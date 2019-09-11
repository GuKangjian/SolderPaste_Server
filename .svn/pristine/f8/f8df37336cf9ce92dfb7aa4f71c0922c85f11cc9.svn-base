using _DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _DAL
{
    public class LineNoConfig
    {

        public DataTable GetAllLineNoConfig()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,LineName from LineNoConfig");
            return DbHelperSql.Query(strSql.ToString()).Tables[0];
        }
    }
}
