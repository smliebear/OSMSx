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
    public class StaffController : Controller
    {
        private Model1 db = new Model1();
        // GET: Staff
        public ActionResult Index(bool Choice = false)
        {
            //判断choice的值来确定用于本页面还是其他页面
            ViewBag.Choice = Choice ? "no" : "yes";
            //判断下拉框
            ViewBag.Staff = db.Staff.ToList();
            var aa = new ViewModel.StaffViewModel { jobs = db.Job, organizationStructures = db.OrganizationStructure };
            return View(aa);
        }

        /// <summary>
        /// 查询，模糊查询
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public ActionResult getdata(string LikeName = "")
        {
            int page = Convert.ToInt32(Request["page"]);
            int limit = Convert.ToInt32(Request["limit"]);
            var list = from u in db.Staff
                       join j in db.Job on u.JobId equals j.Id
                       join os in db.OrganizationStructure on u.OrgID equals os.Id
                       where ((LikeName == "") || (u.Name.Contains(LikeName)))
                       select new { u.Id, u.Name, u.No, u.Sex, u.BirthDay, u.NativePlace, u.Address, u.Email, u.Tel, u.CreateTime, u.UpdateTime, u.Password,j_Id=j.Id, j_Name = j.Name,os_ID=os.Id, o_Name = os.Name };
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
        /// 添加数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddStaff()
        {
            Staff s = new Staff();
            s.Id = Guid.NewGuid();
            s.Name = Request["Name"].ToString();
            s.No = "1";
            if (Request["Sex"].ToString() == "0")
            {
                s.Sex = true;
            }
            else
            {
                s.Sex = false;
            }
            s.BirthDay = Convert.ToDateTime(Request["BirthDay"]);
            s.NativePlace = Request["NativePlace"].ToString();
            s.Address = Request["Address"].ToString();
            s.Password = Request["Password"].ToString();
            s.Email = Request["Email"].ToString();
            s.Tel = Request["Tel"].ToString();
            s.CreateTime = Convert.ToDateTime(Request["CreateTime"]);
            s.UpdateTime = Convert.ToDateTime(Request["UpdateTime"]);
            s.JobId = new Guid(Request["JobId"]);
            s.OrgID = new Guid(Request["OrgID"]);
            s.IsHSEGroup = null;
            db.Staff.Add(s);
            int i = db.SaveChanges();
            return Json(i > 0, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 删除员工基本信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DelStaff(string id)
        {
            //string id = Request["Id"].ToString();
            Guid Id = new Guid(id);
            Staff s = db.Staff.Where(r => r.Id == Id).FirstOrDefault();
            //如果查询有参数
            if (s != null)
            {
                db.Staff.Remove(s);
                int i = db.SaveChanges();
                return Json(i > 0, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }

        }

        /// <summary>
        /// 修改员工基本信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateStaff()
        {
            Staff s = new Staff();
            s.Id = new Guid(Request["Id"]);
            s.Name = Request["Name"].ToString();
            if (Request["Sex"].ToString() == "0")
            {
                s.Sex = true;
            }
            else
            {
                s.Sex = false;
            }
            s.Status = "1";
            s.BirthDay = Convert.ToDateTime(Request["BirthDay"]);
            s.NativePlace = Request["NativePlace"].ToString();
            s.Address = Request["Address"].ToString();
            s.Password = Request["Password"].ToString();
            s.Email = Request["Email"].ToString();
            s.Tel = Request["Tel"].ToString();
            s.CreateTime = Convert.ToDateTime(Request["CreateTime"]);
            s.UpdateTime = Convert.ToDateTime(Request["UpdateTime"]);
            s.JobId = new Guid(Request["JobId"]);
            s.OrgID = new Guid(Request["OrgID"]);
            s.IsHSEGroup = true;
            //1.标识为修改
            db.Entry(s).State = EntityState.Modified;
            //2.保存到数据库
            int i = db.SaveChanges();
            return Json(i > 0, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 角色权限表
        /// </summary>
        /// <returns></returns>
        public ActionResult StaffRole()
        {
            return View();
        }

        /// <summary>
        /// 用户角色组
        /// </summary>
        /// <param name="No"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        public ActionResult StaffRoleRead(string No, string Name)
        {
            int page = Convert.ToInt32(Request["page"]);
            int limit = Convert.ToInt32(Request["limit"]);
            var list = from u in db.Staff
                       join j in db.Job on u.JobId equals j.Id
                       join os in db.OrganizationStructure on u.OrgID equals os.Id
                       where (((No == null) || u.No.Contains(No))&& ((Name == null) || 
                      u.Name.Contains(Name)))
                       select new { u.Id, u.Name, u.No, u.Sex, u.BirthDay, u.NativePlace, u.Address, u.Email, u.Tel, u.CreateTime, u.UpdateTime, u.Password, j_Id = j.Id, j_Name = j.Name, os_ID = os.Id, o_Name = os.Name };
            if (list != null)//当集合的成员大于0时候，说明登录成功
            {
                return Json(new { code = 0, msg = "", count = list.Count(), data = list.OrderBy(u => u.Id).Skip((page - 1) * limit).Take(limit).ToList() }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { code = 0, msg = "", count = list.Count(), data = list.OrderBy(u => u.Id).Skip((limit - 1) * page).Take(limit).ToList() }, JsonRequestBehavior.AllowGet);
            }
        }
    }

}