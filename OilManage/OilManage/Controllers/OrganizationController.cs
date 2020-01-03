using OilManage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilManage.Controllers
{
    public class OrganizationController : Controller
    {
        private Model1 db = new Model1();
        // GET: Organization
        public ActionResult OrganizationIndex()
        {
            return View();
        }

        public ActionResult OrganizationRead()
        {
            int page = Convert.ToInt32(Request["page"]);
            int limit = Convert.ToInt32(Request["limit"]);
            var list = from u in db.OrganizationStructure
                       select new { u.Id,u.Code, u.Name,u.Leve, Pid =u.ParentId == null ? new Guid("{e10ce31c-124c-4398-b118-1d5bf6dd39f3}") : u.ParentId };
            if (list != null)//当集合的成员大于0时候，说明登录成功
            {
                return Json(new { code = 0, msg = "", count = list.Count(), data = list.ToList() }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult OrganizationAdd(string Pid, string Code,string Name)
        {
            OrganizationStructure or = new OrganizationStructure();
            or.Id = Guid.NewGuid();
            or.ParentId = new Guid(Pid);
            or.Code = Code;
            or.Name = Name;
            or.CreateTime = DateTime.Now;
            or.UpdateTime = null;

            db.OrganizationStructure.Add(or);
            int i = db.SaveChanges();
            return Json(i > 0, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult OrganizationDel(string id)
        {
            Guid Id = new Guid(id);
            OrganizationStructure or = db.OrganizationStructure.Where(r => r.Id == Id).FirstOrDefault();
            //如果查询有参数
            if (or != null)
            {
                db.OrganizationStructure.Remove(or);
                int i = db.SaveChanges();
                return Json(i > 0, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
    }
}