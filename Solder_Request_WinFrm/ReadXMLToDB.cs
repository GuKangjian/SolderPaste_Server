using _Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Xml;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;


namespace Solder_Request_WinFrm
{
    public class ReadXMLToDB
    {
        static string _realRequestPath = string.Empty;
        static string _LineNo = string.Empty;

        private static _BLL.RequestInfo _bllRequest = new _BLL.RequestInfo();
        private static _BLL.ResponseInfo _bllResponse = new _BLL.ResponseInfo();
        private static _BLL.SolderPasteDifferRecord _bllSolderPasteDifferRecord = new _BLL.SolderPasteDifferRecord();
        private static _BLL.ResponseDic _bllResponseDic = new _BLL.ResponseDic();

        public ReadXMLToDB(string filePathBefore, string LineNo)
        {
            _realRequestPath = filePathBefore;
            _LineNo = LineNo;
        }

        public void ReadProgramFileDta()
        {
            try
            {
                if (Directory.Exists(_realRequestPath))
                {
                    DirectoryInfo dic = new DirectoryInfo(_realRequestPath);
                    FileInfo[] arrFi = dic.GetFiles("*.*");//获取指定目录下所有文件
                    SortAsFileLastWriteTime(ref arrFi);//所有文件通过创建时间排序
                    string newFile = string.Empty;
                    string fileTime = string.Empty;
                    if (arrFi.Length > 0)
                    {
                        for (int i = arrFi.Length - 1; i > 0; i--)
                        {
                            newFile = _realRequestPath + "\\" + arrFi[i];
                            if (arrFi[i].Name.IndexOf("Request") > -1)
                            {
                                fileTime = arrFi[i].LastWriteTime.ToString();
                                break;
                            }
                        }
                    }
                    ReadRequestXMLToDB(newFile, fileTime, _LineNo);
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Sort By LastWriteTime
        /// </summary>
        /// <param name="arrFi"></param>
        private static void SortAsFileLastWriteTime(ref FileInfo[] arrFi)
        {
            Array.Sort(arrFi, delegate (FileInfo x, FileInfo y) { return x.LastWriteTime.CompareTo(y.LastWriteTime); });
        }

        /// <summary>
        /// Read RequestXML To DB
        /// </summary>
        /// <param name="file"></param>
        internal static void ReadRequestXMLToDB(string file, string fileTime, string lineNo)
        {
            try
            {
                XmlDocument xdoc = new XmlDocument();
                xdoc.Load(file);
                XmlNode nodes = xdoc.SelectSingleNode("root");
                string typeNo = string.Empty;
                string timeStamp = string.Empty;
                //获取当前Request xml文件的TypeNo and timeStamp
                if (nodes != null)
                {
                    foreach (XmlNode node in nodes)
                    {
                        if (node.Name == "event")
                        {
                            XmlNodeList xmlFeeds = ReadXML_MV.GetChildNodes(node);
                            foreach (XmlNode nodeFeed in xmlFeeds)
                            {
                                typeNo = ReadXML_MV.ReadAttrValue(nodeFeed, "typeNo") ?? string.Empty;
                                if (!string.IsNullOrEmpty(typeNo))
                                {
                                    break;
                                }
                            }
                        }
                        if (node.Name == "header")
                        {
                            foreach (System.Xml.XmlAttribute item in node.Attributes)
                            {
                                if (item.Name == "timeStamp")
                                {
                                    timeStamp = item.Value;
                                    break;
                                }
                            }
                        }
                    }
                    //插入数据库
                    InsertToRequestInfoAndCompare(typeNo, timeStamp, fileTime, lineNo);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// InsertRequestXMLInfo To DB   and Copmpare No
        /// </summary>
        /// <param name="typeNo"></param>
        /// <param name="timeStamp"></param>
        public static void InsertToRequestInfoAndCompare(string typeNo, string timeStamp, string fileTime, string lineNo)
        {
            //插入数据库（Request）
            if (!string.IsNullOrEmpty(typeNo) && !string.IsNullOrEmpty(timeStamp))
            {
                //插入数据库前判断是否存在此次Request信息 如果存在说明重复说明同一次生成两个文件触发监听 
                var flag = _bllRequest.IsExist(typeNo, timeStamp, fileTime, lineNo);
                if (!flag)
                {
                    _bllRequest.Add(new _Model.RequestInfo
                    {
                        TypeNo = typeNo,
                        TimeStamp = timeStamp,
                        FileTime = fileTime,
                        LineName = lineNo,
                        CreateTime = Convert.ToDateTime(Convert.ToDateTime(fileTime).ToString("yyyy-MM-dd HH:mm:ss"))
                    });
                }
                else return;
            }
            else return;

            //去ResponseInfo 表中寻找是否存在对应的TypeNo 
            //存在则获取料号 然后与最新一笔的ResponseInfo
            //信息对比(相同[2:change锡膏型号一致，可以转移锡膏]不相同[3:change锡膏型号不一致，不可以转移锡膏]) 不存在(1:change前锡膏型号比对数据不完全，转移锡膏前请谨慎确认)

            string nextMatID = string.Empty;//临时变量保存TypeNo 对应的料号
            var nextResponseDic = _bllResponseDic.GetResponseDic(string.Format("ProductPartNo='{0}' and LineName='{1}'", typeNo, lineNo));
            if (nextResponseDic != null && nextResponseDic.Count > 0)
            {
                foreach (var i in nextResponseDic)
                {
                    if (!string.IsNullOrEmpty(i.MaterialsPartNo) && !string.IsNullOrEmpty(i.ProductPartNo))
                    {
                        nextMatID = i.MaterialsPartNo;
                        break;
                    }
                }
            }
            //获取Request 倒数第二笔信息
            var requestInfo = _bllRequest.GetRequestInfo(lineNo);//Request 表中的第二笔最新(为了获取上一笔Request 信息)
            string lastFileTime = string.Empty;
            string lastTypeNo = string.Empty;
            string lastMatID = string.Empty;

            if (requestInfo != null && requestInfo.Count > 0)
            {
                //2:change锡膏型号一致，可以转移锡膏
                lastFileTime = requestInfo[0].FileTime;
                lastTypeNo = requestInfo[0].TypeNo;

                //通过lastTypeNo获取对应的MatId
                var lastRequestDic = _bllResponseDic.GetResponseDic(string.Format("ProductPartNo='{0}' and LineName='{1}'", lastTypeNo, lineNo));
                if (lastRequestDic != null && lastRequestDic.Count > 0)
                {
                    foreach (var i in lastRequestDic)
                    {
                        if (!string.IsNullOrEmpty(i.MaterialsPartNo) && !string.IsNullOrEmpty(i.ProductPartNo))
                        {
                            lastMatID = i.MaterialsPartNo;
                            break;
                        }
                    }
                }
            }

            //仍然为空 则表示数据库不存在对应的料号Id
            if (string.IsNullOrEmpty(nextMatID) || string.IsNullOrEmpty(lastMatID))
            {
                //不存在(1:change前锡膏型号比对数据不完全，转移锡膏前请谨慎确认)
                InsertToCompareTable(lastFileTime, fileTime, lastTypeNo, typeNo, lastMatID, nextMatID, Convert.ToBase64String(UTF8Encoding.Unicode.GetBytes("plcChangeOverStarted锡膏型号比对数据不完全，转移锡膏前请谨慎确认")), lineNo);
            }
            else
            {
                //相同[2:change锡膏型号一致，可以转移锡膏]不相同[3:change锡膏型号不一致，不可以转移锡膏])
                if (nextMatID == lastMatID)
                {//2 change相同
                    InsertToCompareTable(lastFileTime, fileTime, lastTypeNo, typeNo, lastMatID, nextMatID, Convert.ToBase64String(UTF8Encoding.Unicode.GetBytes("plcChangeOverStarted锡膏型号一致,可以转移锡膏")), lineNo);
                }
                else
                { // 3:change锡膏型号不一致，不可以转移锡膏
                    InsertToCompareTable(lastFileTime, fileTime, lastTypeNo, typeNo, lastMatID, nextMatID, Convert.ToBase64String(UTF8Encoding.Unicode.GetBytes("plcChangeOverStarted锡膏型号不一致，不可以转移锡膏")), lineNo);
                }
            }
        }
        public static void InsertToCompareTable(string lastFileTime, string nextFileTime, string lastTypeNo, string nextTypeNo, string lastMaterialsNo, string nextMaterialsNo, string status, string lineName)
        {
            _Model.SolderPasteDifferRecord model = new _Model.SolderPasteDifferRecord
            {
                LastTypeNo = lastTypeNo,
                NextTypeNo = nextTypeNo,
                LastMaterialsNo = lastMaterialsNo,
                NextMaterialsNo = nextMaterialsNo,
                LastFileTime = lastFileTime,
                NextFileTime = nextFileTime,
                Status = status,
                LineName = lineName,
                DifferSign = "plcChangeOverStarted",
                AduitStatus = Convert.ToBase64String(UTF8Encoding.Unicode.GetBytes("待确认")),
                CreateTime = Convert.ToDateTime(Convert.ToDateTime(nextFileTime).ToString("yyyy-MM-dd HH:mm:ss"))
            };
            _bllSolderPasteDifferRecord.Add(model);//添加对比结果进对比表中
        }
    }
}


// Decompiled with JetBrains decompiler
// Type: Solder_Request_WinFrm.ReadXMLToDB
// Assembly: Solder_Request_WinFrm, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CBE9C9AB-C15B-4AEF-9311-0C4F9B822A80
// Assembly location: C:\Users\92450\Desktop\Solder_Request_WinFrm.exe



//namespace Solder_Request_WinFrm
//{
//    public class ReadXMLToDB
//    {
//        static string _realRequestPath = string.Empty;
//        static string _LineNo = string.Empty;

//        private static _BLL.RequestInfo _bllRequest = new _BLL.RequestInfo();
//        private static _BLL.ResponseInfo _bllResponse = new _BLL.ResponseInfo();
//        private static _BLL.SolderPasteDifferRecord _bllSolderPasteDifferRecord = new _BLL.SolderPasteDifferRecord();
//        private static _BLL.ResponseDic _bllResponseDic = new _BLL.ResponseDic();

//        public ReadXMLToDB(string filePathBefore, string LineNo)
//        {
//            _realRequestPath = filePathBefore;
//            _LineNo = LineNo;
//        }

//        public void ReadProgramFileDta()
//        {
//            try
//            {
//                if (!Directory.Exists(_realRequestPath))
//                    return;
//                FileInfo[] files = new DirectoryInfo(_realRequestPath).GetFiles("*.*");
//                SortAsFileLastWriteTime(ref files);
//                string file = string.Empty;
//                string empty = string.Empty;
//                if ((uint)files.Length > 0U)
//                {
//                    for (int index = files.Length - 1; index > 0; --index)
//                    {
//                        file = ReadXMLToDB._realRequestPath + "\\" + (object)files[index];
//                        if (files[index].Name.IndexOf("Request") > -1)
//                        {
//                            empty = files[index].LastWriteTime.ToString();
//                            break;
//                        }
//                    }
//                }
//                ReadXMLToDB.ReadRequestXMLToDB(file, empty, ReadXMLToDB._LineNo);
//            }
//            catch
//            {
//                throw;
//            }
//        }

//        private static void SortAsFileLastWriteTime(ref FileInfo[] arrFi)
//        {
//            Array.Sort(arrFi, (x, y) => x.LastWriteTime.CompareTo(y.LastWriteTime));
//        }

//        internal static void ReadRequestXMLToDB(string file, string fileTime, string lineNo)
//        {
//            try
//            {
//                XmlDocument xmlDocument = new XmlDocument();
//                xmlDocument.Load(file);
//                XmlNode xmlNode1 = xmlDocument.SelectSingleNode("root");
//                string typeNo = string.Empty;
//                string empty = string.Empty;
//                if (xmlNode1 == null)
//                    return;
//                foreach (XmlNode xmlNode2 in xmlNode1)
//                {
//                    if (xmlNode2.Name == "event")
//                    {
//                        foreach (XmlNode childNode in ReadXML_MV.GetChildNodes(xmlNode2))
//                        {
//                            typeNo = ReadXML_MV.ReadAttrValue(childNode, "typeNo") ?? string.Empty;
//                            if (!string.IsNullOrEmpty(typeNo))
//                                break;
//                        }
//                    }
//                    if (xmlNode2.Name == "header")
//                    {
//                        foreach (XmlAttribute attribute in (XmlNamedNodeMap)xmlNode2.Attributes)
//                        {
//                            if (attribute.Name == "timeStamp")
//                            {
//                                empty = attribute.Value;
//                                break;
//                            }
//                        }
//                    }
//                }
//                ReadXMLToDB.InsertToRequestInfoAndCompare(typeNo, empty, fileTime, lineNo);
//            }
//            catch (Exception ex)
//            {
//                throw new Exception(ex.Message);
//            }
//        }

//        //public static void InsertToRequestInfoAndCompare(string typeNo, string timeStamp, string fileTime, string lineNo)
//        //{
//        //    if (string.IsNullOrEmpty(typeNo) || string.IsNullOrEmpty(timeStamp) || ReadXMLToDB._bllRequest.IsExist(typeNo, timeStamp, fileTime, lineNo))
//        //        return;
//        //    RequestInfo bllRequest = ReadXMLToDB._bllRequest;
//        //    RequestInfo requestInfo1 = new RequestInfo();
//        //    requestInfo1.set_TypeNo(typeNo);
//        //    requestInfo1.set_TimeStamp(timeStamp);
//        //    requestInfo1.set_FileTime(fileTime);
//        //    requestInfo1.set_LineName(lineNo);
//        //    requestInfo1.set_CreateTime(Convert.ToDateTime(Convert.ToDateTime(fileTime).ToString("yyyy-MM-dd HH:mm:ss")));
//        //    bllRequest.Add(requestInfo1);
//        //    string nextMaterialsNo = string.Empty;
//        //    IList<ResponseDic> responseDic = ReadXMLToDB._bllResponseDic.GetResponseDic(string.Format("ProductPartNo='{0}' and LineName='{1}'", (object)typeNo, (object)lineNo));
//        //    if (responseDic != null && ((ICollection<ResponseDic>)responseDic).Count > 0)
//        //    {
//        //        using (IEnumerator<ResponseDic> enumerator = ((IEnumerable<ResponseDic>)responseDic).GetEnumerator())
//        //        {
//        //            while (((IEnumerator)enumerator).MoveNext())
//        //            {
//        //                ResponseDic current = enumerator.Current;
//        //                if (!string.IsNullOrEmpty(current.get_MaterialsPartNo()) && !string.IsNullOrEmpty(current.get_ProductPartNo()))
//        //                {
//        //                    nextMaterialsNo = current.get_MaterialsPartNo();
//        //                    break;
//        //                }
//        //            }
//        //        }
//        //    }
//        //    IList<RequestInfo> requestInfo2 = ReadXMLToDB._bllRequest.GetRequestInfo(lineNo);
//        //    string lastFileTime = string.Empty;
//        //    string lastTypeNo = string.Empty;
//        //    string lastMaterialsNo = string.Empty;
//        //    if (requestInfo2 != null && ((ICollection<RequestInfo>)requestInfo2).Count > 0)
//        //    {
//        //        lastFileTime = requestInfo2[0].get_FileTime();
//        //        lastTypeNo = requestInfo2[0].get_TypeNo();
//        //        IList<SolderPasteDifferRecord> newestRecord = ReadXMLToDB._bllSolderPasteDifferRecord.GetNewestRecord(string.Format("NextTypeNo='{0}' and LineName='{1}' and DifferSign='{2}' order by CreateTime desc", (object)lastTypeNo, (object)lineNo, (object)"plcChangeOverStarted"));
//        //        if (newestRecord != null && ((ICollection<SolderPasteDifferRecord>)newestRecord).Count > 0)
//        //        {
//        //            using (IEnumerator<SolderPasteDifferRecord> enumerator = ((IEnumerable<SolderPasteDifferRecord>)newestRecord).GetEnumerator())
//        //            {
//        //                while (((IEnumerator)enumerator).MoveNext())
//        //                {
//        //                    SolderPasteDifferRecord current = enumerator.Current;
//        //                    if (!string.IsNullOrEmpty(current.get_NextMaterialsNo()))
//        //                    {
//        //                        lastMaterialsNo = current.get_NextMaterialsNo();
//        //                        break;
//        //                    }
//        //                }
//        //            }
//        //        }
//        //    }
//        //    if (string.IsNullOrEmpty(nextMaterialsNo) || string.IsNullOrEmpty(lastMaterialsNo))
//        //        ReadXMLToDB.InsertToCompareTable(lastFileTime, fileTime, lastTypeNo, typeNo, lastMaterialsNo, nextMaterialsNo, Convert.ToBase64String(Encoding.Unicode.GetBytes("plcChangeOverStarted锡膏型号比对数据不完全，转移锡膏前请谨慎确认")), lineNo);
//        //    else if (nextMaterialsNo == lastMaterialsNo)
//        //        ReadXMLToDB.InsertToCompareTable(lastFileTime, fileTime, lastTypeNo, typeNo, lastMaterialsNo, nextMaterialsNo, Convert.ToBase64String(Encoding.Unicode.GetBytes("plcChangeOverStarted锡膏型号一致,可以转移锡膏")), lineNo);
//        //    else
//        //        ReadXMLToDB.InsertToCompareTable(lastFileTime, fileTime, lastTypeNo, typeNo, lastMaterialsNo, nextMaterialsNo, Convert.ToBase64String(Encoding.Unicode.GetBytes("plcChangeOverStarted锡膏型号不一致，不可以转移锡膏")), lineNo);
//        //}

//        //public static void InsertToCompareTable(string lastFileTime, string nextFileTime, string lastTypeNo, string nextTypeNo, string lastMaterialsNo, string nextMaterialsNo, string status, string lineName)
//        //{
//        //    SolderPasteDifferRecord pasteDifferRecord1 = new SolderPasteDifferRecord();
//        //    pasteDifferRecord1.set_LastTypeNo(lastTypeNo);
//        //    pasteDifferRecord1.set_NextTypeNo(nextTypeNo);
//        //    pasteDifferRecord1.set_LastMaterialsNo(lastMaterialsNo);
//        //    pasteDifferRecord1.set_NextMaterialsNo(nextMaterialsNo);
//        //    pasteDifferRecord1.set_LastFileTime(lastFileTime);
//        //    pasteDifferRecord1.set_NextFileTime(nextFileTime);
//        //    pasteDifferRecord1.set_Status(status);
//        //    pasteDifferRecord1.set_LineName(lineName);
//        //    pasteDifferRecord1.set_DifferSign("plcChangeOverStarted");
//        //    pasteDifferRecord1.set_AduitStatus(Convert.ToBase64String(Encoding.Unicode.GetBytes("待确认")));
//        //    pasteDifferRecord1.set_CreateTime(Convert.ToDateTime(Convert.ToDateTime(nextFileTime).ToString("yyyy-MM-dd HH:mm:ss")));
//        //    SolderPasteDifferRecord pasteDifferRecord2 = pasteDifferRecord1;
//        //    ReadXMLToDB._bllSolderPasteDifferRecord.Add(pasteDifferRecord2);
//        //}
//    }
//}

