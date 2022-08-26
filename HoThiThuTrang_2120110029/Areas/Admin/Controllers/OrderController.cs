using HoThiThuTrang_2120110029.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HoThiThuTrang_2120110029.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        QuanLyBanHangEntities3 objquanLyBanHangEntities3 = new QuanLyBanHangEntities3();

        // GET: Admin/Order
        public ActionResult Index()
        {
            var listOrder = objquanLyBanHangEntities3.Orders.ToList();
            return View(listOrder);
        }
    }
}