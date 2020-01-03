using OilManage.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilManage.Controllers
{
    public class RoleController : Controller
    {
        private Model1 db = new Model1();
        // GET: Role
        public ActionResult RoleIndex(bool Choice = false)
        {
            //判断choice的值来确定用于本页面还是其他页面
            ViewBag.Choice = Choice==false ? "no" : "yes";
            return View();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Code"></param>
        /// <returns></returns>
        public ActionResult RoleRead(string Name,string Code,string Id)
        {
            int page = Convert.ToInt32(Request["page"]);
            int limit = Convert.ToInt32(Request["limit"]);
            Guid id;
            if (Id == null)
            {
               id = new Guid();
            }
            else
            {
                id = new Guid(Id);
            }
            var list = from r in db.Role
                       join s in db.StaffRole.Where(u => u.StaffId == id)
                       on r.Id equals s.RoleId
                       into a from d in a.DefaultIfEmpty()
                       where ((((Name == null) || r.Name.Contains(Name)))&&((Code== null) ||r.Code.Contains(Code)))
                       select new
                       {
                           r.Id,
                           Name = r.Name,
                           Code = r.Code,
                           LAY_CHECKED = d.StaffId != null ? true : false,
                       };
            if (list != null)
            {
                return Json(new { code = 0, msg = "", count = list.Count(), data = list.OrderBy(u => u.Id).Skip((page - 1) * limit).Take(limit).ToList() }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { code = 0, msg = "", count = list.Count(), data = list.OrderBy(u => u.Id).Skip((limit - 1) * page).Take(limit).ToList() }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        public ActionResult RoleAdd(string Name, string Code)
        {
            Role r = new Role();
            r.Id = Guid.NewGuid(); ;
            r.Name = Name;
            r.Code = Code;
            db.Role.Add(r);
            int i = db.SaveChanges();
            return Json(i > 0, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Name"></param>
        /// <param name="Code"></param>
        /// <returns></returns>
        public ActionResult RoleUpdate(string Id, string Name, string Code)
        { 
            Role r = new Role();
            r.Id =new Guid(Id);
            r.Name = Name;
            r.Code = Code;
            //1.标识为修改
            db.Entry(r).State = EntityState.Modified;
            //2.保存到数据库
            int i = db.SaveChanges();
            return Json(i > 0, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult RoleDel(string Id)
        {
            //string id = Request["Id"].ToString();
            Guid id = new Guid(Id);
            Role s = db.Role.Where(r => r.Id == id).FirstOrDefault();
            //如果查询有参数
            if (s != null)
            {
                db.Role.Remove(s);
                int i = db.SaveChanges();
                return Json(i > 0, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }

        }


      [HttpPost]
      public ActionResult RoleChoice(String Id,string StaffId)
        {
            //string id = Request["Id"].ToString();
            Guid Sid = new Guid(StaffId);
            string dSQL = "delete from StaffRole Where StaffId='" + StaffId + "'";
            db.Database.ExecuteSqlCommand(dSQL);
            var sArray = Id.Split(',');
            StaffRole r = new StaffRole();
            if (Id != null)
            {
                string sql = "";
                foreach (string a in sArray)  //取出一个添加一个，最后一起保存
                {
                    if (a != "")
                    {
                        sql += string.Format("insert into StaffRole values('{0}','{1}')\r", StaffId, a);
                    }
                }
                db.Database.ExecuteSqlCommand(sql);
            }
           
            int i = 1;
            return Json(i > 0, JsonRequestBehavior.AllowGet);

        }
    }
}