using _DBUtility;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _DAL
{
    public class RequestInfo
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(_Model.RequestInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into RequestInfo(");
            strSql.Append("TypeNo,FileTime,LineName,TimeStamp,CreateTime");
            strSql.Append(") values (");
            strSql.Append("'" + model.TypeNo + "','" + model.FileTime + "','" + model.LineName + "','" + model.TimeStamp + "','" + model.CreateTime + "'");
            strSql.Append(") ");
            DbHelperSql.ExecuteSql(strSql.ToString());
        }

        public DataTable GetRequestInfo(string lineNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from (select*, row_number() over (order by CreateTime desc) as row_num from RequestInfo where LineName='" + lineNo + "') collection where row_num = 2");
            return DbHelperSql.Query(strSql.ToString()).Tables[0];
        }

        public DataTable GetRequest()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 * from RequestInfo order by CreateTime desc");
            return DbHelperSql.Query(strSql.ToString()).Tables[0];
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM RequestInfo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSql.Query(strSql.ToString());
        }



        public DataSet Exist(string typeNo, string timeStamp)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append("FROM RequestInfo ");
            strSql.Append("where TypeNo='" + typeNo + "'  and timeStamp='" + timeStamp + "'");
            return DbHelperSql.Query(strSql.ToString());
        }

        public DataTable GetNewRequest(string lineNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 * from RequestInfo where LineName='" + lineNo + "' order by CreateTime desc");
            return DbHelperSql.Query(strSql.ToString()).Tables[0];
        }

        public DataSet Exist(string typeNo, string timeStamp, string fileTime, string _LineNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append("FROM RequestInfo ");
            strSql.Append("where TypeNo='" + typeNo + "'  and timeStamp='" + timeStamp + "' and FileTime='" + fileTime + "' and LineName='" + _LineNo + "'");
            return DbHelperSql.Query(strSql.ToString());
        }
    }
}
