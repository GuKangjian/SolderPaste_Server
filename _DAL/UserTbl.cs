
using _DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _DAL
{
    public class UserTbl
    {


        public DataSet GetUserInfoByUserName(string userName)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select u.Id,u.UserName,u.UserPwd,u.UserRole from UserTbl u where u.UserName='" + userName + "'");
            return DbHelperSql.Query(sql.ToString());
        }
    }
}
