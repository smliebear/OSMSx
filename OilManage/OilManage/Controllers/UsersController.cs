using OilManage.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

using System.Web;
using System.Web.Mvc;

namespace OilManage.Controllers
{
    public class UsersController : Controller
    {
        private Model1 db = new Model1();
        // GET: Users
        public ActionResult Index()
        {
            Staff s = Session["user"] as Staff;
            Guid Id = s.Id;
            //var list = from sr in db.StaffRole
            //           join rrm in db.RoleResourceModule on sr.RoleId equals rrm.RoleId into pt
            //           from p in pt.DefaultIfEmpty()
            //           where (sr.StaffId==Id)
            //           select new {p.ResourceModuleId  };
           
            //var Syid = list.SingleOrDefault().ResourceModuleId;
            //staff -staffrole - role -rolemodel -  system  id-staffid-roleid-syid-
            ViewBag.Parent = db.SystemResourceModule.Where(r => r.ParentId == null );
            ViewBag.Chiren = db.SystemResourceModule.Where(r => r.ParentId != null );
            return View();
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Pwd"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Updatepwd(string id, string Pwd)
        {
            Staff s = new Staff() { Id = new Guid(id), Password = Pwd };
            DbEntityEntry<Staff> entry = db.Entry<Staff>(s);
            entry.State = EntityState.Unchanged;
            entry.Property(t => t.Password).IsModified = true; //设置要更新的属性
            int i = db.SaveChanges();
            return Json(i > 0, JsonRequestBehavior.AllowGet);

        }
    }
}