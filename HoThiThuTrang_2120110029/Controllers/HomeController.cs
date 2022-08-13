using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HoThiThuTrang_2120110029.Context;
using HoThiThuTrang_2120110029.Models;

namespace HoThiThuTrang_2120110029.Controllers
{
    public class HomeController : Controller
    {
        QuanLyBanHangEntities3 objquanLyBanHangEntities3 = new QuanLyBanHangEntities3();
        public ActionResult Index()
        {
            HomeModel objHomeModel = new HomeModel();
            objHomeModel.ListCategory = objquanLyBanHangEntities3.Categories.ToList();

            objHomeModel.ListProduct = objquanLyBanHangEntities3.Products.ToList();

            return View(objHomeModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}