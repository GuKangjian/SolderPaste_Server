using _DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _DAL
{
    public class SolderPasteDifferRecord
    {



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(_Model.SolderPasteDifferRecord model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SolderPasteDifferRecord(");
            strSql.Append("LastMaterialsNo,NextMaterialsNo,LastTypeNo,NextTypeNo,LastFileTime,NextFileTime,Status,AduitStatus,LineName,DifferSign,CreateTime");
            strSql.Append(") values (");
            strSql.Append("'" + model.LastMaterialsNo + "','" + model.NextMaterialsNo + "','" + model.LastTypeNo + "','" + model.NextTypeNo + "','" + model.LastFileTime + "','" + model.NextFileTime + "','" + model.Status + "','" + model.AduitStatus + "','" + model.LineName + "','" + model.DifferSign + "','" + model.CreateTime + "'");
            strSql.Append(") ");
            DbHelperSql.ExecuteSql(strSql.ToString());
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append("FROM SolderPasteDifferRecord ");
            if (strWhere.Trim() != "")
            {
                strSql.Append("where " + strWhere);
            }
            return DbHelperSql.Query(strSql.ToString());
        }

        /// <summary>
        /// Request
        /// </summary>
        /// <param name="lineName"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public DataSet GetAllSolderPasteplcChangeOverStarted(string lineName, string startTime, string endTime, int startIndex, int endIndex)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from ");
            strSql.Append("(select s.Id,s.LastMaterialsNo,s.NextMaterialsNo,s.AduitStatus,s.CreateTime,s.DifferSign, s.LastFileTime, s.LastTypeNo, s.LineName, s.NextFileTime, s.Status, s.NextTypeNo, row_number() over (order by s.CreateTime desc) as myRow from SolderPasteDifferRecord s where LineName = '" + lineName + "' and DifferSign = 'plcChangeOverStarted' and CreateTime> '" + startTime + "' and CreateTime<'" + endTime + "') vv ");
            strSql.Append("where  vv.myRow between '" + startIndex + "' and '" + endIndex + "'");
            return DbHelperSql.Query(strSql.ToString());
        }



        public int GetAllSolderPasteplcChangeOverStarted(string lineName, string startTime, string endTime)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select  * from SolderPasteDifferRecord s where LineName = '" + lineName + "' and DifferSign = 'plcChangeOverStarted' and CreateTime> '" + startTime + "' and CreateTime<'" + endTime + "'");

            var ds = DbHelperSql.Query(strSql.ToString());
            if (ds != null && ds.Tables[0] != null)
                return ds.Tables[0].Rows.Count;
            else
                return 0;
        }

        /// <summary>
        /// Response
        /// </summary>
        /// <param name="lineName"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public DataSet GetAllSolderPasteplcMaterialChangeStarted(string lineName, string startTime, string endTime, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from ");
            strSql.Append("(select s.Id,s.AduitStatus,s.LastMaterialsNo,s.NextMaterialsNo,s.CreateTime, s.DifferSign, s.LastFileTime, s.LastTypeNo, s.LineName, s.NextFileTime, s.Status, s.NextTypeNo, row_number() over (order by s.CreateTime desc) as myRow from SolderPasteDifferRecord s where LineName = '" + lineName + "' and DifferSign = 'plcMaterialChangeStarted' and CreateTime> '" + startTime + "' and CreateTime<'" + endTime + "') vv ");
            strSql.Append("where  vv.myRow between '" + startIndex + "' and '" + endIndex + "'");
            return DbHelperSql.Query(strSql.ToString());
        }
        public int GetAllSolderPasteplcMaterialChangeStarted(string lineName, string startTime, string endTime)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select * from SolderPasteDifferRecord s where LineName = '" + lineName + "' and DifferSign = 'plcMaterialChangeStarted' and CreateTime> '" + startTime + "' and CreateTime<'" + endTime + "'");
            var ds = DbHelperSql.Query(strSql.ToString());
            if (ds != null && ds.Tables[0] != null)
                return ds.Tables[0].Rows.Count;
            else
                return 0;
        }

        public int UpdateAduitStatus(string id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE SolderPasteDifferRecord  SET AduitStatus = '" + Convert.ToBase64String(UTF8Encoding.Unicode.GetBytes("已确认")) + "' WHERE Id = '" + id + "'");
            return DbHelperSql.ExecuteSql(strSql.ToString());
        }

        public DataSet GetLastestData(string lineNo, string sign)
        {
            StringBuilder strSql = new StringBuilder();
            if (sign == "1")
            {
                strSql.Append("select top 1 * from SolderPasteDifferRecord s  ");
                strSql.Append("where s.LineName = '" + lineNo + "' and DifferSign = 'plcChangeOverStarted'  order by createtime desc ");
            }
            else
            {
                strSql.Append("select top 1 * from SolderPasteDifferRecord s  ");
                strSql.Append("where s.LineName = '" + lineNo + "' and DifferSign = 'plcMaterialChangeStarted'  order by createtime desc ");
            }
            return DbHelperSql.Query(strSql.ToString());

        }

        
    }


}
