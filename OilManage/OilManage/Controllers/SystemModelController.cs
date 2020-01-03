using OilManage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilManage.Controllers
{
    public class SystemModelController : Controller
    {
        private Model1 db = new Model1();
        // GET: SystemModel
        public ActionResult SystemModelIndex(bool Choice = false,string RId = null)
        {
            ViewBag.Choice = Choice ? "yes" : "no";
            ViewBag.RId = RId == null ? null : RId;
            return View();
        }

        /// <summary>
        ///查询系统模块资源 根据角色查看所拥有的权限
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult SystemModelRead(string Id)
        {
            int page = Convert.ToInt32(Request["page"]);
            int limit = Convert.ToInt32(Request["limit"]);
            Guid id;
            if (Id == null)
            {
                id = Guid.NewGuid();
            }else
            {
                id = new Guid(Id);
            }
            var list = from r in db.SystemResourceModule
                       join s in db.RoleResourceModule.Where(u => u.RoleId == id)
                       on r.Id equals s.ResourceModuleId
                       into a from d in a.DefaultIfEmpty()
                       select new { r.Id,
                           Name= r.Name,
                           Code= r.Code,
                           r.Url,
                           r.Type,
                           Pid = r.ParentId == null ? new Guid("{e10ce31c-124c-4398-b118-1d5bf6dd39f3}") : r.ParentId,
                           lay_is_checked =d.ResourceModuleId!=null?true:false,
                          };
            
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
        /// <summary>
        /// 根据选中与取消选中判断权限
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        public ActionResult SystemModelChoice(string Id,string RoleId)
        {
            //string id = Request["Id"].ToString();
            Guid Sid = new Guid(RoleId);
            string dSQL = "delete from RoleResourceModule Where RoleId='" + RoleId + "'";
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
                        sql += string.Format("insert into RoleResourceModule values('{0}','{1}')\r", RoleId, a);
                    }
                }
                db.Database.ExecuteSqlCommand(sql);
            }

            int i = 1;
            return Json(i > 0, JsonRequestBehavior.AllowGet);

        }
    }
}