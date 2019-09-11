using _Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace Solder_Response_WinFrm
{
    public class FileWatcher
    {
        public string _realResponsePath { get; set; }
        public string _LineNo { get; set; }
        public string _filter { get; set; }

        public FileSystemWatcher watcher = null;
        public List<_Model.LineConfig> _listConfig { get; set; }

        private Queue<LineNoAndPath> ListQueue = new Queue<LineNoAndPath>();
        public FileWatcher(string realResponsePath, string filter, string LineNo, List<_Model.LineConfig> listConfig)
        {
            _listConfig = listConfig;
            _realResponsePath = realResponsePath;
            _filter = filter;
            _LineNo = LineNo;
            System.Timers.Timer t = new System.Timers.Timer(5000);
            t.Elapsed += T_Elapsed;
            t.AutoReset = true;
            t.Enabled = true;
        }

        private void T_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (ListQueue.Count > 0)
            {
                try
                {
                    ScanQueue();
                }
                catch 
                {
                    throw;
                }
            }
        }

        public void WatcherStart()
        {
            try
            {
                if (Directory.Exists(_realResponsePath))
                {
                    watcher = new FileSystemWatcher
                    {
                        Path = _realResponsePath,
                        Filter = _filter
                    };
                    watcher.Created += new FileSystemEventHandler(OnProcess);
                    watcher.EnableRaisingEvents = true;
                    watcher.NotifyFilter = NotifyFilters.Attributes | NotifyFilters.CreationTime | NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.LastAccess
                                           | NotifyFilters.LastWrite | NotifyFilters.Security | NotifyFilters.Size;
                    watcher.IncludeSubdirectories = true;
                    Thread.Sleep(178000);
                    watcher.EnableRaisingEvents = false;
                }
            }
            catch 
            {
                throw;
            }
        }

        private void OnProcess(object source, FileSystemEventArgs e)
        {
            if (e.ChangeType == WatcherChangeTypes.Created)
            {
                OnCreated(source, e);
            }
        }

        private void OnCreated(object source, FileSystemEventArgs e)
        {
            //有新的报文产生 就去解析 相应的数据
            try
            {
                var path = watcher.Path;
                var lineNo = _listConfig
                    .FirstOrDefault(p => p.FilePathResponse
                    .Replace("*", DateTime.Now.Year.ToString() + "\\" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "\\" + DateTime.Now.Day.ToString().PadLeft(2, '0')) == path).LineNo;
                //有新的报文产生 就去解析 相应的数据      
                ListQueue.Enqueue(new LineNoAndPath { LineNo = lineNo.ToString(), Path = path });//加到队列中  
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
                    //从队列中取出
                    LineNoAndPath queueinfo = ListQueue.Dequeue();
                    new ReadXMLToDB(queueinfo.Path, queueinfo.LineNo).ReadProgramFileDta();
                }
                catch
                {
                    throw;
                }
            }
        }
    }
}
