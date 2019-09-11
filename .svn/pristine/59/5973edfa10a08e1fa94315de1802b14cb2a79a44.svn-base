
using _Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace Solder_Response_WinFrm
{
    public class ReadXMLToDB
    {
        string _realResponsePath = string.Empty;
        static string LineNo = string.Empty;
        private static _BLL.RequestInfo _bllRequest = new _BLL.RequestInfo();
        private static _BLL.ResponseInfo _bllResponse = new _BLL.ResponseInfo();
        private static _BLL.ResponseDic _bllResponseDic = new _BLL.ResponseDic();
        private static _BLL.SolderPasteDifferRecord _bllSolderPasteDifferRecord = new _BLL.SolderPasteDifferRecord();
        public ReadXMLToDB(string filePathBefore, string _LineNo)
        {
            _realResponsePath = filePathBefore;
            LineNo = _LineNo;
        }

        public void ReadProgramFileDta()
        {
            try
            {
                string fileTime = string.Empty;
                if (Directory.Exists(_realResponsePath))
                {
                    DirectoryInfo dic = new DirectoryInfo(_realResponsePath);
                    FileInfo[] arrFi = dic.GetFiles("*.*");//获取指定目录下所有文件
                    SortAsFileLastWriteTime(ref arrFi);//所有文件通过创建时间排序
                    string newFile = string.Empty;
                    if (arrFi.Length > 0)
                    {
                        for (int i = arrFi.Length - 1; i > 0; i--)
                        {
                            newFile = _realResponsePath + "\\" + arrFi[i];
                            if (arrFi[i].Name.IndexOf("Response") > -1)
                            {
                                fileTime = arrFi[i].LastWriteTime.ToString();
                                break;
                            }
                        }
                    }
                    ReadResponseXMLToDB(newFile, fileTime, LineNo);
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
        internal static void ReadResponseXMLToDB(string file, string fileTime, string lineNo)
        {
            try
            {
                XmlDocument xdoc = new XmlDocument();
                xdoc.Load(file);
                XmlNode nodes = xdoc.SelectSingleNode("root/body/structArrays/array");
                string matID = string.Empty;
                string partNo = string.Empty;
                string timeStamp = string.Empty;
                //获取当前Request xml文件的TypeNo and timeStamp
                if (nodes != null)
                {
                    //21号线Response报文不同
                    if (lineNo == "21" || lineNo == "32" || lineNo == "26")
                    {
                        foreach (XmlNode node in nodes)
                        {
                            if (node.Name == "values")
                            {
                                XmlNodeList xmlFeeds = ReadXML_MV.GetChildNodes(node);
                                foreach (XmlNode nodeFeed in xmlFeeds)
                                {
                                    partNo = ReadXML_MV.ReadAttrValue(nodeFeed, "partNo");
                                    matID = ReadXML_MV.ReadAttrValue(nodeFeed, "matID");
                                    if (!string.IsNullOrEmpty(partNo) && !string.IsNullOrEmpty(matID))
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (XmlNode node in nodes)
                        {
                            if (node.Name == "values")
                            {
                                XmlNodeList xmlFeeds = ReadXML_MV.GetChildNodes(node);
                                foreach (XmlNode nodeFeed in xmlFeeds)
                                {
                                    partNo = ReadXML_MV.ReadAttrValue(nodeFeed, "typeNo");
                                    matID = ReadXML_MV.ReadAttrValue(nodeFeed, "matID");
                                    if (!string.IsNullOrEmpty(partNo) && !string.IsNullOrEmpty(matID))
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    XmlNode nodes_head = xdoc.SelectSingleNode("root");
                    //获取当前Request xml文件的TypeNo and timeStamp
                    foreach (XmlNode node in nodes_head)
                    {
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
                    InsertToResponseInfoAndCompare(partNo, matID, timeStamp, fileTime, lineNo);
                }
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// InsertResponseXMLInfo To DB   and Copmpare No
        /// </summary>
        /// <param name="typeNo"></param>
        /// <param name="timeStamp"></param>
        public static void InsertToResponseInfoAndCompare(string partNo, string matId, string timeStamp, string fileTime, string lineNo)
        {
            //插入数据库（先把当前独到的xml  插入数据库 ResponseInfo  再将其插入到RequestDic表 有则不插入 或更新 无则插入）
            //先判断存在 如果存在一样 说明是其他文件变化
            //插入数据库（Response）
            if (!string.IsNullOrEmpty(partNo) && !string.IsNullOrEmpty(timeStamp) && !string.IsNullOrEmpty(matId))
            {
                //插入数据库前判断是否存在此次Request信息 如果存在说明重复说明同一次生成两个文件触发监听 
                var flag = _bllResponse.IsExist(partNo, timeStamp, lineNo, matId);
                if (!flag)
                {
                    InsertResponseToDB(new _Model.ResponseInfo
                    {
                        PartNo = partNo,
                        MatID = matId,
                        TimeStamp = timeStamp,
                        LineName = lineNo,
                        CreateTime = Convert.ToDateTime(Convert.ToDateTime(fileTime).ToString("yyyy-MM-dd HH:mm:ss"))
                    });
                }
                else return;
            }
            else return;

            //获取此号线最新一笔Request的TypeNo 
            var newRequest = _bllRequest.GetNewRequest(lineNo);//Request获取此号线最新一笔Request的TypeNo 
            var typeNo = string.Empty;
            if (newRequest != null && newRequest.Count > 0)
            {
                typeNo = newRequest[0].TypeNo;
                InsertOrUpdateResponseDic(new _Model.ResponseDic
                {
                    MaterialsPartNo = partNo,
                    ProductPartNo = typeNo,
                    LineName = lineNo
                });//字典表插入或者更新
            }
            //开始比较 获取RequestInfo 表中最新倒数第二笔
            var requestInfo = _bllRequest.GetRequestInfo(lineNo);//Request 表中的第二笔最新(为了获取上一笔Request 信息)
            string lastFileTime = string.Empty;
            string lastTypeNo = string.Empty;
            string lastLineNo = string.Empty;
            string lastMaterialsPartNo = string.Empty;
            if (requestInfo != null && requestInfo.Count > 0)
            {
                //2:change锡膏型号一致，可以转移锡膏
                lastFileTime = requestInfo[0].FileTime;
                lastTypeNo = requestInfo[0].TypeNo;
                lastLineNo = requestInfo[0].LineName;
                //通过lastTypeNo获取对应的MatId
                var ResponseDic = _bllResponseDic.GetResponseDic(string.Format("ProductPartNo='{0}' and LineName='{1}'", lastTypeNo, lastLineNo));

                if (ResponseDic != null && ResponseDic.Count > 0)
                {
                    foreach (var i in ResponseDic)
                    {
                        if (!string.IsNullOrEmpty(i.MaterialsPartNo))
                        {
                            lastMaterialsPartNo = i.MaterialsPartNo;
                            break;
                        }
                    }
                }
            }

            if (!string.IsNullOrEmpty(partNo) && !string.IsNullOrEmpty(lastMaterialsPartNo))
            {
                if (partNo == lastMaterialsPartNo)
                {
                    //2:锡膏型号一致，可以转移锡膏]
                    InsertToCompareTable(lastFileTime, fileTime, lastTypeNo, typeNo, lastMaterialsPartNo, partNo, Convert.ToBase64String(UTF8Encoding.Unicode.GetBytes("plcMaterialChangeStarted锡膏型号一致，可以转移锡膏")), lineNo);
                }
                else
                {
                    InsertToCompareTable(lastFileTime, fileTime, lastTypeNo, typeNo, lastMaterialsPartNo, partNo, Convert.ToBase64String(UTF8Encoding.Unicode.GetBytes("plcMaterialChangeStarted锡膏型号不一致，不可以转移锡膏")), lineNo);
                    //[3:Response锡膏型号不一致，不可以转移锡膏])
                }
            }
            else
            {
                InsertToCompareTable(lastFileTime, fileTime, lastTypeNo, typeNo, lastMaterialsPartNo, partNo, Convert.ToBase64String(UTF8Encoding.Unicode.GetBytes("plcMaterialChangeStarted锡膏型号比对数据不完全，转移锡膏前请谨慎确认")), lineNo);
                //不存在(1:前锡膏型号比对数据不完全，转移锡膏前请谨慎确认)
            }
        }



        private static void InsertResponseToDB(_Model.ResponseInfo model)
        {
            _bllResponse.Add(model);
        }

        /// <summary>
        /// 插入或者更新值 Request 字典表
        /// </summary>
        /// <param name="model"></param>
        private static void InsertOrUpdateResponseDic(_Model.ResponseDic model)
        {

            var RequestDic = _bllResponseDic.GetResponseDic(string.Format("LineName='{0}' and ProductPartNo='{1}'", model.LineName, model.ProductPartNo));
            if (RequestDic != null && RequestDic.Count > 0)
            {
                //存在此线的ResponseDic的ProductPartNo 
                //1.更新
                _bllResponseDic.UpdateMaterialsToResponseDicByPartNo(model.ProductPartNo, model.LineName, model.MaterialsPartNo);
            }
            else
            {
                //字典表中不存在 插入新的
                _bllResponseDic.Add(model);
            }
        }



        public static void InsertToCompareTable(string lastFileTime, string nextFileTime, string lastTypeNo, string nextTypeNo, string lastMatNo, string nextMatNo, string status, string lineName)
        {
            _Model.SolderPasteDifferRecord model = new _Model.SolderPasteDifferRecord
            {
                LastTypeNo = lastTypeNo,
                NextTypeNo = nextTypeNo,
                LastMaterialsNo = lastMatNo,
                NextMaterialsNo = nextMatNo,
                LastFileTime = lastFileTime,
                NextFileTime = nextFileTime,
                Status = status,
                LineName = lineName,
                DifferSign = "plcMaterialChangeStarted",
                AduitStatus = Convert.ToBase64String(UTF8Encoding.Unicode.GetBytes("待确认")),
                CreateTime = Convert.ToDateTime(Convert.ToDateTime(nextFileTime).ToString("yyyy-MM-dd HH:mm:ss"))
            };
            _bllSolderPasteDifferRecord.Add(model);//添加对比结果进对比表中
        }
    }
}
