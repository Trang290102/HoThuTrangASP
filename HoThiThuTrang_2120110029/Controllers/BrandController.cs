using HoThiThuTrang_2120110029.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HoThiThuTrang_2120110029.Controllers
{
    public class BrandController : Controller
    {
        QuanLyBanHangEntities3 objquanLyBanHangEntities3 = new QuanLyBanHangEntities3();

        // GET: Brand
        public ActionResult Index()
        {
            //danh sách loại sản phẩm
            var lstBrand = objquanLyBanHangEntities3.Brands.ToList();
            return View(lstBrand);
        }
    }

}