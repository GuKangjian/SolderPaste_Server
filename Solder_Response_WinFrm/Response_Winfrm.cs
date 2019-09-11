using _Common;
using Solder_Response_WinFrm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Solder_Response_WinFrm
{
    public partial class Response_Winfrm : Form
    {
        public Response_Winfrm()
        {
            InitializeComponent();
        }
        #region Init
        private List<_Model.LineConfig> listConfig = ReadXMLContent.GetConfigFile(ConfigurationSettings.AppSettings["FileConfigPath"].ToString() + "FilePathConfig.XML");

        private Thread mainThread = null;
        #endregion     

        #region Load
        private void Response_Winfrm_Load(object sender, EventArgs e)
        {
            //重启的时候读取当天文件夹下面的所有内容
            ReStartBackUp();//重启备份
            mainThread = new Thread(ThreadRead);
            mainThread.Start();//主线程开始跑
            timer_service.Interval = 1000 * 60 * 3;//每隔分钟重启(防止当前未生产)
            timer_service.Elapsed += Timer_service_Elapsed;
            timer_service.AutoReset = true;
            timer_service.Enabled = true;
        }
        #endregion

        #region BackUp 
        private void ReStartBackUp()
        {
            for (int i = 0; i < listConfig.Count; i++)
            {
                string newPath = listConfig[i].FilePathResponse.Replace("*", DateTime.Now.Year.ToString() + "\\" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "\\" + DateTime.Now.Day.ToString().PadLeft(2, '0'));
                if (!Directory.Exists(newPath))
                {
                    continue;//不存在的地址跳过
                }
                //存在 对其地址下面的Response文件进行解析
                new ResponseBackUp(listConfig[i].LineNo.ToString(), newPath).ExeBackUp();
            }
        }
        #endregion

        #region  定时
        /// <summary>
        /// 定时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_service_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                mainThread.Abort();
                mainThread = new Thread(ThreadRead);
                mainThread.Start();//主线程重新开始跑
            }
            catch (Exception ex)
            {
                using (FileStream stream = new FileStream(ConfigurationSettings.AppSettings["LogPath"].ToString(), FileMode.Append))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.WriteLine("ResponseTimerElapsed Error:" + ex.ToString() + DateTime.Now);
                }
            }
        }
        #endregion

        #region ThreadRead
        private void ThreadRead()
        {
            try
            {
                string noExistNo = string.Empty;
                for (int i = 0; i < listConfig.Count; i++)
                {
                    string newPath = listConfig[i].FilePathResponse.Replace("*", DateTime.Now.Year.ToString() + "\\" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "\\" + DateTime.Now.Day.ToString().PadLeft(2, '0'));
                    if (!Directory.Exists(newPath))
                    {
                        noExistNo += listConfig[i].LineNo.ToString() + "--";//地址不存在的线号
                        continue;//不存在的地址跳过
                    }
                    //执行已经存在的地址里面最新的那一条的信息读取
                    ExeReadNewestXMLToDB(newPath, listConfig[i].LineNo.ToString());
                    //创建监听
                    FileWatcher watcher = new FileWatcher(newPath, "*.*", listConfig[i].LineNo.ToString(), listConfig);
                    Thread th = new Thread(watcher.WatcherStart);
                    th.Start();
                }
                using (FileStream stream = new FileStream(ConfigurationSettings.AppSettings["LogPath"].ToString(), FileMode.Append))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.WriteLine("Response NoExistLineNo:" + noExistNo + ";" + DateTime.Now);
                }
            }
            catch (Exception ex)
            {
                using (FileStream stream = new FileStream(ConfigurationSettings.AppSettings["LogPath"].ToString(), FileMode.Append))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.WriteLine("Response Error:" + ex.Message + ";" + DateTime.Now);
                }
            }
        }

        private void ExeReadNewestXMLToDB(string newPath, string v)
        {
            new ResponseBackUp(v, newPath).ExeLastestXML();
        }
        #endregion
    }
}
