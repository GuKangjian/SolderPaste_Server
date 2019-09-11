// Decompiled with JetBrains decompiler
// Type: Solder_Request_WinFrm.FileWatcher
// Assembly: Solder_Request_WinFrm, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CBE9C9AB-C15B-4AEF-9311-0C4F9B822A80
// Assembly location: C:\Users\92450\Desktop\Solder_Request_WinFrm.exe

using _Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Timers;

namespace Solder_Request_WinFrm
{
    public class FileWatcher
    {
        public FileSystemWatcher watcher = (FileSystemWatcher)null;
        private Queue<LineNoAndPath> ListQueue = new Queue<LineNoAndPath>();

        public string _realRequestPath { get; set; }

        public string _LineNo { get; set; }

        public string _filter { get; set; }

        public List<LineConfig> _listConfig { get; set; }

        public FileWatcher(string realRequestPath, string filter, string LineNo, List<LineConfig> listConfig)
        {
            this._listConfig = listConfig;
            this._realRequestPath = realRequestPath;
            this._filter = filter;
            this._LineNo = LineNo;
            System.Timers.Timer timer = new System.Timers.Timer(5000.0);
            timer.Elapsed += new ElapsedEventHandler(this.T_Elapsed);
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        private void T_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (this.ListQueue.Count <= 0)
                return;
            try
            {
                this.ScanQueue();
            }
            catch
            {
                throw;
            }
        }

        public void WatcherStart()
        {
            try
            {
                if (!Directory.Exists(this._realRequestPath))
                    return;
                this.watcher = new FileSystemWatcher()
                {
                    Path = this._realRequestPath,
                    Filter = this._filter
                };
                this.watcher.Created += new FileSystemEventHandler(this.OnProcess);
                this.watcher.EnableRaisingEvents = true;
                this.watcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.Attributes | NotifyFilters.Size | NotifyFilters.LastWrite | NotifyFilters.LastAccess | NotifyFilters.CreationTime | NotifyFilters.Security;
                this.watcher.IncludeSubdirectories = true;
                Thread.Sleep(178000);
                this.watcher.EnableRaisingEvents = false;
            }
            catch
            {
                throw;
            }
        }

        private void OnProcess(object source, FileSystemEventArgs e)
        {
            if (e.ChangeType != WatcherChangeTypes.Created)
                return;
            this.OnCreated(source, e);
        }

        private void OnCreated(object source, FileSystemEventArgs e)
        {
            try
            {
                string path = this.watcher.Path;
                int lineNo = ((IEnumerable<LineConfig>)_listConfig).FirstOrDefault(p => p.FilePathRequest.Replace("*", DateTime.Now.Year.ToString() + "\\" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "\\" + DateTime.Now.Day.ToString().PadLeft(2, '0')) == path).LineNo;
                Queue<LineNoAndPath> listQueue = this.ListQueue;
                LineNoAndPath lineNoAndPath = new LineNoAndPath();
                lineNoAndPath.LineNo=lineNo.ToString();
                lineNoAndPath.Path=path;
                listQueue.Enqueue(lineNoAndPath);
            }
            catch
            {
                throw;
            }
        }

        private void ScanQueue()
        {
            while (ListQueue.Count > 0)
            {
                try
                {
                    LineNoAndPath lineNoAndPath = this.ListQueue.Dequeue();
                    new ReadXMLToDB(lineNoAndPath.Path, lineNoAndPath.LineNo).ReadProgramFileDta();
                }
                catch
                {
                    throw;
                }
            }
        }
    }
}
