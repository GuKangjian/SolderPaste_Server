

using System;
using System.IO;

namespace Solder_Request_WinFrm
{
    public class RequestBackUp
    {
        public string LineNo { get; set; }

        public string Path { get; set; }

        public RequestBackUp(string _lineNo, string _path)
        {
            this.LineNo = _lineNo;
            this.Path = _path;
        }

        public void ExeBackUp()
        {
            FileInfo[] files = new DirectoryInfo(this.Path).GetFiles("*.*");
            if ((uint)files.Length <= 0U)
                return;
            for (int index = 0; index < files.Length; ++index)
            {
                string file = this.Path + "\\" + (object)files[index];
                if (files[index].Name.Contains("Request"))
                {
                    string fileTime = files[index].LastWriteTime.ToString();
                    ReadXMLToDB.ReadRequestXMLToDB(file, fileTime, this.LineNo);
                }
            }
        }

        public void ExeLastestXML()
        {
            FileInfo[] files = new DirectoryInfo(this.Path).GetFiles("*.*");
            RequestBackUp.SortAsFileLastWriteTime(ref files);
            string empty = string.Empty;
            string file = string.Empty;
            if ((uint)files.Length > 0U)
            {
                for (int index = 0; index < files.Length; ++index)
                {
                    file = this.Path + "\\" + (object)files[index];
                    if (files[index].Name.IndexOf("Request") > -1)
                    {
                        empty = files[index].LastWriteTime.ToString();
                        break;
                    }
                }
            }
            ReadXMLToDB.ReadRequestXMLToDB(file, empty, this.LineNo);
        }

        private static void SortAsFileLastWriteTime(ref FileInfo[] arrFi)
        {
            Array.Sort<FileInfo>(arrFi, (Comparison<FileInfo>)((x, y) => x.LastWriteTime.CompareTo(y.LastWriteTime)));
        }
    }
}
