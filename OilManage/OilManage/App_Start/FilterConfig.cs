using System.Web;
using System.Web.Mvc;
using OilManage.AppCode;

namespace OilManage
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //将登陆验证添加至全局变量
            filters.Add(new LoginFilter() { IsCheck = true });
        }
    }
}
