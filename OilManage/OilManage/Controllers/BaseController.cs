using OilManage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace OilManage.Controllers
{
    public class BaseController : Controller
    {
        private Model1 db = new Model1();
        // GET: Base

        public ActionResult Index()
        {
            return View();
        }

        public List<Models.SystemResourceModule> GetSystemResources(Guid id)
        {
            StaffRole sRData = db.StaffRole.Where(x => x.StaffId == id).FirstOrDefault();//用户是什么角色
            List<Guid> ResourceModuleId = new List<Guid>();
            if (sRData != null)
            {
                List<RoleResourceModule> rrData = db.RoleResourceModule.Where(x => x.RoleId == sRData.RoleId).ToList();//这个角色有什么资源
                foreach (RoleResourceModule item in rrData)
                {
                    ResourceModuleId.Add(item.ResourceModuleId);
                }
            }

            List<SystemResourceModule> srsDate = db.SystemResourceModule.Where(x => ResourceModuleId.Contains(x.Id)).ToList();//查询系统资源库中包含ResourceModuleId的条目
            return srsDate;
        }
    }
}