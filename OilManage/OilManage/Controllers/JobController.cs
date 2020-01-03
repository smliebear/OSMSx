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
    public class JobController : Controller
    {
        private Model1 db = new Model1();
        // GET: Job
        public ActionResult JobIndex(bool Choice=false)
        {
            ViewBag.Choice = Choice ? "no" : "yes";
       
            ViewBag.Job = db.Job.ToList();
            return View();
          
        }

        /// <summary>
        /// 查询/模糊查询
        /// </summary>
        /// <param name="JobName"></param>
        /// <returns></returns>
        public ActionResult JobRead(string JobName="") {
            int page = Convert.ToInt32(Request["page"]);
            int limit = Convert.ToInt32(Request["limit"]);
            var list = from u in db.Job
                       where ((JobName == "") || (u.Name.Contains(JobName)))
                       select new { u.Id, u.Name, u.Code, u.UpdateTime, u.CreateTime };
            if (list != null)//当集合的成员大于0时候，说明登录成功
            {
                return Json(new { code = 0, msg = "", count = list.Count(), data = list.OrderBy(u => u.Id).Skip((page - 1) * limit).Take(limit).ToList() }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Job"></param>
        /// <param name="CreateTime"></param>
        /// <param name="UpdateTime"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult JobAdd(string Name,string Job,string CreateTime,string UpdateTime)
        {
            Job j = new Job();
            j.Id = Guid.NewGuid();
            j.Name = Name;
            j.Code= Job;
            j.CreateTime =Convert.ToDateTime(CreateTime) ;
            j.UpdateTime = Convert.ToDateTime(UpdateTime);
            j.IsDel = true;
            db.Job.Add(j);
            int i = db.SaveChanges();
            return Json(i > 0, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult JobDel(string id) {
            Guid Id = new Guid(id);
            Job j = db.Job.Where(r => r.Id == Id).FirstOrDefault();
            //如果查询有参数
            if (j != null)
            {
                db.Job.Remove(j);
                int i = db.SaveChanges();
                return Json(i > 0, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Name"></param>
        /// <param name="Code"></param>
        /// <param name="CreateTime"></param>
        /// <param name="UpdateTime"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult JobUpdate(string Id,string Name,string Code,string CreateTime,string UpdateTime) { 
            Job j = new Job();
            j.Id = new Guid(Id);
            j.Name = Name;
            j.Code = Code;
            j.CreateTime = Convert.ToDateTime(CreateTime);
            j.UpdateTime = Convert.ToDateTime(UpdateTime);

            //1.标识为修改
            db.Entry(j).State = EntityState.Modified;
            //2.保存到数据库
            int i = db.SaveChanges();
            return Json(i > 0, JsonRequestBehavior.AllowGet);
        }
    }
}