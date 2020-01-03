using OilManage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OilManage.AppCode;


namespace OilManage.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        private Model1 db = new Model1();
        [LoginFilter(IsCheck = false)]
        public ActionResult Index()
        {
            return View();
        }

        [LoginFilter(IsCheck = false)]
        [HttpPost]
        public ActionResult GetData()
        {
            string name = Request["name"].ToString();
            string password = Request["pwd"].ToString();
            string flag = "";
            var list = db.Staff.Where(u => u.Name == name && u.Password == password).ToList();
            Models.Staff s = list.FirstOrDefault();//将泛型集合转成实体对象
            
            if (list.Count > 0)//当集合的成员大于0时候，说明登录成功
            {
                Session["user"] = s;
                //return RedirectToAction("Index", "Users");//跳转到主页面
                return Json(s.Name, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(flag, JsonRequestBehavior.AllowGet);
            }
        }
    }
}