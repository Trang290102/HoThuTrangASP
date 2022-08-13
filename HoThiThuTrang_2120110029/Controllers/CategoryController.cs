using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HoThiThuTrang_2120110029.Context;


namespace HoThiThuTrang_2120110029.Controllers
{

    public class CategoryController : Controller
    {
        QuanLyBanHangEntities3 objquanLyBanHangEntities3 = new QuanLyBanHangEntities3();

        // GET: Category
        public ActionResult Index()
        {
            var lstCategory = objquanLyBanHangEntities3.Categories.ToList();
            return View(lstCategory);
        }
    }
}