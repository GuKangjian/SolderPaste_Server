using _Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Solder_Response_WinFrm
{
    public class ResponseBackUp
    {
        public string LineNo { get; set; }

        public string Path { get; set; }

        public ResponseBackUp(string _lineNo, string _path)
        {
            LineNo = _lineNo;
            Path = _path;
        }

        public void ExeBackUp()
        {
            DirectoryInfo dic = new DirectoryInfo(Path);
            FileInfo[] arrFi = dic.GetFiles("*.*");//获取指定目录下所有文件
            if (arrFi.Length > 0)
            {
                for (int i = 0; i < arrFi.Length; i++)
                {
                    string newFile = Path + "\\" + arrFi[i];
                    if (arrFi[i].Name.Contains("Response"))//解析
                    {
                        string fileTime = arrFi[i].LastWriteTime.ToString();//时间                                                                      //解析内容
                        ReadXMLToDB.ReadResponseXMLToDB(newFile, fileTime, LineNo);
                    }
                }
            }
        }
        /// <summary>
        /// 执行读取已经可以访问的地址下面的最新一笔报文信息
        /// </summary>
        internal void ExeLastestXML()
        {
            DirectoryInfo dic = new DirectoryInfo(Path);
            FileInfo[] arrFi = dic.GetFiles("*.*");//获取指定目录下所有文件
            SortAsFileLastWriteTime(ref arrFi);//所有文件通过创建时间排序
            string fileTime = string.Empty;
            string newFile = string.Empty;
            if (arrFi.Length > 0)
            {
                for (int i = 0; i < arrFi.Length; i++)
                {
                    newFile = Path + "\\" + arrFi[i];
                    if (arrFi[i].Name.IndexOf("Response") > -1)
                    {
                        fileTime = arrFi[i].LastWriteTime.ToString();
                       
                        break;
                    }
                }
            }
            ReadXMLToDB.ReadResponseXMLToDB(newFile, fileTime, LineNo);
        }

        /// <summary>
        /// Sort By LastWriteTime
        /// </summary>
        /// <param name="arrFi"></param>
        private static void SortAsFileLastWriteTime(ref FileInfo[] arrFi)
        {
            Array.Sort(arrFi, delegate (FileInfo x, FileInfo y) { return x.LastWriteTime.CompareTo(y.LastWriteTime); });
        }
    }
}
