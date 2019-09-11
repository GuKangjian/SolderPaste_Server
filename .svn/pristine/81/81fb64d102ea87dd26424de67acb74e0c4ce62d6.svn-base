using _Common;
using _Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _BLL
{

    public class SolderPasteDifferRecord
    {

        private readonly _DAL.SolderPasteDifferRecord dal = new _DAL.SolderPasteDifferRecord();
        public SolderPasteDifferRecord()
        { }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(_Model.SolderPasteDifferRecord model)
        {
            dal.Add(model);

        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<_Model.SolderPasteDifferRecord> GetSolderPasteDifferRecordList(string strWhere)
        {
            var _dt = dal.GetList(strWhere);
            if (_dt != null && _dt.Tables[0] != null)
                return ModelConvertHelper<_Model.SolderPasteDifferRecord>.ConvertToModel(_dt.Tables[0]);
            else
                return null;
        }

        public string GetAllSolderPasteDifferRecord(string lineName, int currentPage, int pageSize, string sign)
        {
            string strResJson = string.Empty;
            int recordCount = 0;
            try
            {
                int startindex = (currentPage - 1) * pageSize + 1;
                int endindex = currentPage * pageSize;
                string startTime = DateTime.Now.AddHours(-12).ToString();
                string endTime = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "-" + DateTime.Now.Day.ToString().PadLeft(2, '0') + " 23:59:59";
                DataSet ds = null;
                if (sign == "1")
                {
                    recordCount = dal.GetAllSolderPasteplcChangeOverStarted(lineName, startTime, endTime);
                    ds = dal.GetAllSolderPasteplcChangeOverStarted(lineName, startTime, endTime, startindex, endindex);
                }
                else if (sign == "2")
                {
                    recordCount = dal.GetAllSolderPasteplcMaterialChangeStarted(lineName, startTime, endTime);
                    ds = dal.GetAllSolderPasteplcMaterialChangeStarted(lineName, startTime, endTime, startindex, endindex);

                }
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        ds.Tables[0].Rows[i]["Status"] = UTF8Encoding.Unicode.GetString(Convert.FromBase64String(ds.Tables[0].Rows[i]["Status"].ToString()));
                        ds.Tables[0].Rows[i]["AduitStatus"] = UTF8Encoding.Unicode.GetString(Convert.FromBase64String(ds.Tables[0].Rows[i]["AduitStatus"].ToString()));
                    }
                    strResJson = TableToJson.GetResultJson(recordCount, JsonHelper.ToJson(ds.Tables[0]));
                }
                else {
                    strResJson = "{\"total\":" + recordCount + ",\"rows\":[]}";
                }
            }
            catch
            {
                strResJson = "{\"total\":" + recordCount + ",\"rows\":[]}";
            }
            return strResJson;
        }

        public string GetLastestData(string lineNo, string sign)
        {
            string strResJson = string.Empty;
            try
            {
                DataSet ds = null;
                ds = dal.GetLastestData(lineNo, sign);
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        ds.Tables[0].Rows[i]["Status"] = UTF8Encoding.Unicode.GetString(Convert.FromBase64String(ds.Tables[0].Rows[i]["Status"].ToString()));
                        ds.Tables[0].Rows[i]["AduitStatus"] = UTF8Encoding.Unicode.GetString(Convert.FromBase64String(ds.Tables[0].Rows[i]["AduitStatus"].ToString()));
                    }
                    strResJson = JsonHelper.ToJsonSingle(ds.Tables[0]);
                }
                
            }
            catch
            {
               
            }
            return strResJson;
        }

        public string GetAllSolderPasteDifferRecordByDate(string lineName, int currentPage, int pageSize, string sign, string startTime, string endTime)
        {
            string strResJson = string.Empty;
            startTime = startTime + " 00:00:00";
            endTime = endTime + " 23:59:59";

            int recordCount = 0;
            try
            {
                int startindex = (currentPage - 1) * pageSize + 1;
                int endindex = currentPage * pageSize;
                DataSet ds = null;
                if (sign == "1")
                {
                    recordCount = dal.GetAllSolderPasteplcChangeOverStarted(lineName, startTime, endTime);
                    ds = dal.GetAllSolderPasteplcChangeOverStarted(lineName, startTime, endTime, startindex, endindex);
                }
                else if (sign == "2")
                {
                    recordCount = dal.GetAllSolderPasteplcMaterialChangeStarted(lineName, startTime, endTime);
                    ds = dal.GetAllSolderPasteplcMaterialChangeStarted(lineName, startTime, endTime, startindex, endindex);
                }
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        ds.Tables[0].Rows[i]["Status"] = UTF8Encoding.Unicode.GetString(Convert.FromBase64String(ds.Tables[0].Rows[i]["Status"].ToString()));
                        ds.Tables[0].Rows[i]["AduitStatus"] = UTF8Encoding.Unicode.GetString(Convert.FromBase64String(ds.Tables[0].Rows[i]["AduitStatus"].ToString()));
                    }
                    strResJson = TableToJson.GetResultJson(recordCount, JsonHelper.ToJson(ds.Tables[0]));
                }
                else {
                    strResJson = "{\"total\":" + recordCount + ",\"rows\":[]}";
                }
            }
            catch
            {
                strResJson= "{\"total\":" + recordCount + ",\"rows\":[]}";
            }
            return strResJson;
        }

        public string UpdateAduitStatus(string id)
        {
            if (dal.UpdateAduitStatus(id) > 0)
            {
                return "ok";
            }
            else
            {
                return "no";
            }
        }
    }
}
