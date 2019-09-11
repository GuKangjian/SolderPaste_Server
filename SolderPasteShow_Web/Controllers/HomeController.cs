using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SolderPasteShow_Web.Controllers
{
    public class HomeController : Controller
    {

        _BLL.LineNoConfig bllLineNo = new _BLL.LineNoConfig();
        _BLL.SolderPasteDifferRecord bllSolderPasteDifferRecord = new _BLL.SolderPasteDifferRecord();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Record()
        {
            return View();
        }

        public ActionResult OldIndex()
        {
            return View();
        }
        /// <summary>
        /// 获取线号
        /// </summary>
        /// <returns></returns>
        public JsonResult LineNo()
        {
            List<_Model.LineNoConfig> result = bllLineNo.GetAllLineNoConfig() as List<_Model.LineNoConfig>;
            JsonSerializerSettings setting = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            var ret = JsonConvert.SerializeObject(result, setting);
            return Json(ret, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 用户通过日期查看信息
        /// </summary>
        /// <returns></returns>
        public string GetDifferDataByDate()
        {
            string lineNo = Request["LineNo"].ToString();
            string currentPage = Request["currentPage"].ToString();
            string size = Request["size"].ToString();
            string sign = Request["sign"].ToString();
            string startTime = Request["startTime"].ToString();
            string endTime = Request["endTime"].ToString();
            //返回两个json对象
            var result = bllSolderPasteDifferRecord.GetAllSolderPasteDifferRecordByDate(lineNo, Convert.ToInt32(currentPage), Convert.ToInt32(size), sign, startTime, endTime);
            return result;
        }

        /// <summary>
        /// 不同的标记获得两次换型的数据信息
        /// </summary>
        /// <returns></returns>
        public string GetDifferData()
        {
            string lineNo = Request["LineNo"].ToString();
            string currentPage = Request["currentPage"].ToString();
            string size = Request["size"].ToString();
            string sign = Request["sign"].ToString();
            //返回两个json对象
            var result = bllSolderPasteDifferRecord.GetAllSolderPasteDifferRecord(lineNo, Convert.ToInt32(currentPage), Convert.ToInt32(size), sign);
            return result;
        }

        /// <summary>
        /// 获得最新的一笔数据
        /// </summary>
        /// <returns></returns>
        public string GetLastestData()
        {
            string lineNo = Request["LineNo"].ToString();
            string sign = Request["sign"].ToString();
            //返回两个json对象
            var result = bllSolderPasteDifferRecord.GetLastestData(lineNo, sign);
            return result;
        }

        
        /// <summary>
        /// 更新确认状态
        /// </summary>
        /// <returns></returns>
        public string UpdateAduitStatus()
        {
            string id = Request["ID"].ToString();
            //返回两个json对象
            return bllSolderPasteDifferRecord.UpdateAduitStatus(id);
        }
    }
}