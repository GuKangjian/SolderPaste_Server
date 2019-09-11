using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SolderPasteShow_Web.Controllers
{

    public class LoginController : Controller
    {
        _BLL.UserTbl _dal = new _BLL.UserTbl();
        _Model.UserTbl model = null;
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public int CheckUser()
        {
            string us = Request["us"].ToString();
            string pw = Request["pw"].ToString();

            int flag = _dal.GetUserInfoByUserName(us, pw, out model);

            if (model != null)
            {
                Session["S_username"] = model.UserName.TrimEnd();
                Session["S_ID"] = model.Id;
                Session["S_Role"] = model.UserRole.ToString();
            }
            return flag;
        }
    }

}