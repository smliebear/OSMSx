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
    public class LeaveApproveController : Controller
    {
        // GET: LeaveApprove
        private Model1 db = new Model1();
        public ActionResult LeaveApproveIndex()
        {
            return View();
        }

        /// <summary>
        /// 读取数据
        /// </summary>
        /// <returns></returns>
        public ActionResult LeaveApproveRead()
        {
            int page = Convert.ToInt32(Request["page"]);
            int limit = Convert.ToInt32(Request["limit"]);
            var list = from u in db.LeaveOffice
                       join s in db.Job on u.JobId equals s.Id
                       select new { u.Id,u.No,u.StaffName,u.JobId,JobName=s.Name,u.LeaveType,u.ApplyPersonId, u.Reason,u.CreateTime,u.HandoverSatffId, u.ExplanationHandover };
            if (list != null)//当集合的成员大于0时候，说明登录成功
            {
                return Json(new { code = 0, msg = "", count = list.Count(), data = list.OrderBy(u => u.Id).Skip((page - 1) * limit).Take(limit).ToList() }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { code = 0, msg = "", count = list.Count(), data = list.OrderBy(u => u.Id).Skip((limit - 1) * page).Take(limit).ToList() }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        ///得到修改的取值
        /// </summary>
        /// <param name="HandoverSatffName"></param>
        /// <returns></returns>
        public ActionResult LeaveApproveapartRead(string HandoverSatffName)
        {
            Guid hid = new Guid(HandoverSatffName);
            var list = from u in db.LeaveOffice
                       join s in db.Staff on u.HandoverSatffId equals s.Id
                       where s.Id== hid
                       select new { s.Id,s.Name };
            var lists = list.ToList();
            if (list != null)//当集合的成员大于0时候，说明登录成功
            {
                return Json(lists, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="StaffName"></param>
        /// <param name="JobId"></param>
        /// <param name="LeaveType"></param>
        /// <param name="CreateTime"></param>
        /// <param name="Reason"></param>
        /// <param name="ExplanationHandover"></param>
        /// <param name="HandoverSatffId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult LeaveApproveAdd(string StaffName,string JobId,string LeaveType,string CreateTime,string Reason,string ExplanationHandover,string HandoverSatffId)
        {
            LeaveOffice l = new LeaveOffice();
            l.Id = Guid.NewGuid();
            l.StaffName = StaffName;
            l.JobId =new Guid(JobId);
            l.LeaveType = LeaveType;
            l.CreateTime = Convert.ToDateTime(CreateTime);
            l.Reason = Reason;
            l.ExplanationHandover = ExplanationHandover;
            l.HandoverSatffId = new Guid(HandoverSatffId);
            db.LeaveOffice.Add(l);
            int i = db.SaveChanges();
            return Json(i > 0, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="StaffName"></param>
        /// <param name="JobId"></param>
        /// <param name="LeaveType"></param>
        /// <param name="CreateTime"></param>
        /// <param name="Reason"></param>
        /// <param name="ExplanationHandover"></param>
        /// <param name="HandoverSatffId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult LeaveApproverUpdate(string Id,string StaffName, string JobId, string LeaveType, string CreateTime, string Reason, string ExplanationHandover, string HandoverSatffId)
        {
            LeaveOffice l = new LeaveOffice();
            l = (from aps in db.LeaveOffice
                 where (aps.Id == new Guid(Id))
                 select aps).Single();
            l.StaffName = StaffName;
            l.JobId = new Guid(JobId);
            l.LeaveType = LeaveType;
            l.CreateTime = Convert.ToDateTime(CreateTime);
            l.Reason = Reason;
            l.ExplanationHandover = ExplanationHandover;
            l.HandoverSatffId = new Guid(HandoverSatffId);
            db.Entry(l).State = EntityState.Modified;
            int i = db.SaveChanges();
            return Json(i > 0, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult LeaveApproverDel(string Id)
        {
            Guid id = new Guid(Id);
            LeaveOffice l = db.LeaveOffice.Where(r => r.Id == id).FirstOrDefault();
            //如果查询有参数
            if (l != null)
            {
                db.LeaveOffice.Remove(l);
                int i = db.SaveChanges();
                return Json(i > 0, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult LeaveApproverSubmitAdd(Guid LeaveId,Guid ApplyPersonId,string No)
        {
            ProcessStepRecord p = new ProcessStepRecord();
            p.Id = Guid.NewGuid();
            p.Type = "LeaveOffice";
            p.RecordRemarks = "";
            p.StepOrder =0;
            p.WaitForExecutionStaffId = ApplyPersonId;
            p.CreateTime = DateTime.Now;
            p.UpdateTime = DateTime.Now;
            p.WhetherToExecute = true;
            p.No = No;
            p.RefOrderId = LeaveId;
            p.Result = true;   //默认为0，即通过
            p.ExecuteMethod = "";
            p.Discrible = "";
            db.ProcessStepRecord.Add(p);
            int i = db.SaveChanges();
            return Json(i > 0, JsonRequestBehavior.AllowGet);
        }
    }
}