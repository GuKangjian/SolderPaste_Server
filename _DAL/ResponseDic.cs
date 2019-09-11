using _DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _DAL
{
    public class ResponseDic
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(_Model.ResponseDic model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ResponseDic(");
            strSql.Append("LineName,ProductPartNo,MaterialsPartNo");
            strSql.Append(") values (");
            strSql.Append("'" + model.LineName + "','" + model.ProductPartNo + "','" + model.MaterialsPartNo + "'");
            strSql.Append(") ");
            DbHelperSql.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetResponseDic(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * ");
            strSql.Append("FROM ResponseDic ");
            if (strWhere.Trim() != "")
            {
                strSql.Append("where " + strWhere);
            }
            return DbHelperSql.Query(strSql.ToString());
        }

        public void UpdateMatIDToResponseDicByPartNo(string productPartNo, string lineNo, string MaterialsPartNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE ResponseDic SET MaterialsPartNo = '" + MaterialsPartNo + "' WHERE LineName = '" + lineNo + "' and ProductPartNo='" + productPartNo + "'");

            int c = DbHelperSql.ExecuteSql(strSql.ToString());

        }
    }
}
