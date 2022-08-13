using System.Web;
using System.Web.Mvc;

namespace HoThiThuTrang_2120110029
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
