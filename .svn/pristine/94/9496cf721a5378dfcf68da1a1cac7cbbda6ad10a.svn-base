
using _Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _BLL
{
    public class UserTbl
    {
        static _DAL.UserTbl _dal = new _DAL.UserTbl();

        public int GetUserInfoByUserName(string userName, string userPwd, out _Model.UserTbl user)
        {
            int flag = -1;
            try
            {
                var userInfo = _dal.GetUserInfoByUserName(userName);
                if (userInfo != null)
                {
                    var result = ModelConvertHelper<_Model.UserTbl>.ConvertToModel(userInfo.Tables[0]);
                    int returnId = -1;
                    string returnUserName = string.Empty;
                    string returnUserPwd = string.Empty;
                    int returnUserRole = -1;
                    if (result != null && result.Count > 0)
                    {
                        foreach (var i in result)
                        {
                            if (!string.IsNullOrEmpty(i.UserName) && !string.IsNullOrEmpty(i.UserPwd) && !string.IsNullOrEmpty(i.UserRole.ToString()))
                            {
                               
                                returnId = i.Id;
                                returnUserName = i.UserName;
                                returnUserPwd = i.UserPwd;
                                returnUserRole = i.UserRole;
                                break;
                            }
                        }
                    }
                    if (returnUserName == userName)
                    {
                        //用户名相等
                        if (returnUserPwd == userPwd)
                        {
                            user = new _Model.UserTbl { Id = returnId, UserName = returnUserName, UserPwd = userPwd, UserRole = returnUserRole };
                            flag = 0;

                            //用户名密码相同
                        }
                        else
                        {
                            flag = 1;
                            user = null;
                            //密码错误
                        }
                    }
                    else
                    {
                        user = null;
                        flag = 2;
                        //用户名错误
                    }
                }
                else
                {
                    user = null;
                }
            }
            catch 
            {
                user = null;
            }
            return flag;
        }
    }
}
