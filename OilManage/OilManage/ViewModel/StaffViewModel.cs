using OilManage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OilManage.ViewModel
{
    public class StaffViewModel
    {
        public  IQueryable<Job>  jobs { get; set; }
        public IQueryable<OrganizationStructure> organizationStructures { get; set; }
    }
}