using _DBUtility;
using _Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _DAL
{
    public class ResponseInfo
    {

        public bool Exists(string partNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ResponseInfo");
            strSql.Append(" where ");
            strSql.Append(" PartNo ='" + partNo + "'  ");
            return DbHelperSql.Exists(strSql.ToString());
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(_Model.ResponseInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ResponseInfo(");
            strSql.Append("PartNo,MatID,LineName,TimeStamp,CreateTime");
            strSql.Append(") values (");
            strSql.Append("'" + model.PartNo + "','" + model.MatID + "','" + model.LineName + "','" + model.TimeStamp + "','" + DateTime.Now + "'");
            strSql.Append(") ");

            Console.WriteLine(strSql.ToString());
            int count = DbHelperSql.ExecuteSql(strSql.ToString());
            Console.WriteLine(count.ToString());
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * ");
            strSql.Append("FROM ResponseInfo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append("where " + strWhere);
            }
            return DbHelperSql.Query(strSql.ToString());
        }

        public void UpdateMatIDByPartNo(string partNo, string matId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE ResponseInfo  SET MatID = '" + matId + "' WHERE PartNo = '" + partNo + "'");
            DbHelperSql.ExecuteSql(strSql.ToString());
        }

        public DataTable GetResponse(string partNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from dbo.ResponseInfo t where t.PartNo='" + partNo + "'");
            return DbHelperSql.Query(strSql.ToString()).Tables[0];
        }

        public DataTable GetResponseInfo()
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 * from dbo.ResponseInfo t order by t.CreateTime desc");
            return DbHelperSql.Query(strSql.ToString()).Tables[0];
        }


        public DataSet Exist(string partNo, string timeStamp, string LineNo, string matId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append("FROM ResponseInfo ");
            strSql.Append("where PartNo='" + partNo + "' and MatID='" + matId + "' and timeStamp='" + timeStamp + "'  and LineName='" + LineNo + "'");
            return DbHelperSql.Query(strSql.ToString());
        }
    }
}
