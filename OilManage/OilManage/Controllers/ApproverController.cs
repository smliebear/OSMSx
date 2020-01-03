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
    public class ApproverController : Controller
    {
        private Model1 db = new Model1();
        // GET: Approver
        public ActionResult ApproverIndex()
        {
            return View(db.ProcessItem);
        }

        /// <summary>
        /// 流程设置查询
        /// </summary>
        /// <returns></returns>
        public ActionResult ApproverRead(string AreaLeve, string JobCode = "", string ProcessItemId = "7BB5A65C-C31C-4993-9CE2-12D8BABFADB3")
        {
            Guid guid = new Guid(ProcessItemId);
            int page = Convert.ToInt32(Request["page"]);
            int limit = Convert.ToInt32(Request["limit"]);
            int arealeve =Convert.ToInt32(AreaLeve);
            var list = from a in db.Approver
                       join p in db.ProcessItem on a.ProcessItemId equals p.Id into pt
                       from p in pt.DefaultIfEmpty()
                       where ((a.ProcessItemId == guid) && ((JobCode == "") || a.JobCode.Contains(JobCode)))
                       select new { a.Id, a.JobCode, a.AreaLeve, a.Discrible, a.Order, pDiscrible=p.Discrible,a.ExecuteMethod };
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
        /// 流程设置添加
        /// </summary>
        /// <param name="JobCode"></param>
        /// <param name="AreaLeve"></param>
        /// <param name="Discrible"></param>
        /// <param name="ExecuteMethod"></param>
        /// <returns></returns>
        public ActionResult ApproverAdd(string JobCode,string AreaLeve,string Discrible,string ExecuteMethod)
        {
            Approver a = new Approver();
            a.Id = Guid.NewGuid();
            a.JobCode = JobCode;
            a.AreaLeve =Convert.ToInt32(AreaLeve);
            a.Discrible = Discrible;
            a.ExecuteMethod = ExecuteMethod;
            db.Approver.Add(a);
            int i = db.SaveChanges();
            return Json(i > 0, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 流程设置删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ApproverDel(string id)
        {
            Guid Id = new Guid(id);
            Approver a = db.Approver.Where(r => r.Id == Id).FirstOrDefault();
            //如果查询有参数
            if (a != null)
            {
                db.Approver.Remove(a);
                int i = db.SaveChanges();
                return Json(i > 0, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }

        }

        /// <summary>
        /// 流程设置修改
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="JobCode"></param>
        /// <param name="AreaLeve"></param>
        /// <param name="Discrible"></param>
        /// <param name="ExecuteMethod"></param>
        /// <returns></returns>
        public ActionResult ApproverUpdate(string Id, string JobCode, string AreaLeve, string Discrible, string ExecuteMethod)
        {
            Approver a = new Approver();
            a.Id = new Guid(Id);
            a.JobCode = JobCode;
            a.AreaLeve = Convert.ToInt32(AreaLeve);
            a.Discrible = Discrible;
            a.ExecuteMethod = ExecuteMethod;
            db.Entry(a).State = EntityState.Modified;
            int i = db.SaveChanges();
            return Json(i > 0, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 向上移动
        /// </summary>
        /// <param name="Order"></param>
        /// <param name="Orders"></param>
        /// <returns></returns>
        public ActionResult ApproverMove(string Id, string Orders,string ProcessItemId) {
            Guid Aid = new Guid(Id);
            //根据该字段的id找到order
            Approver a = new Approver();
           a = (from aps in db.Approver
                 where (aps.Id == new Guid(Id))
                 select aps).Single();

            //第二个表的查询
            int Orde = Convert.ToInt32(Orders);
            Approver b = new Approver();
            b = (from aps in db.Approver
                        where (aps.Order == Orde && aps.ProcessItemId == new Guid(ProcessItemId)) select aps).Single();

            a.Order = a.Order -  1;
            db.Entry(a).State = EntityState.Modified;
            b.Order = b.Order + 1;
            db.Entry(b).State = EntityState.Modified;
            //2.保存到数据库
            int i = db.SaveChanges();
            return Json(i, JsonRequestBehavior.AllowGet);
        }
    }
}