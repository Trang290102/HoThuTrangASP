using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HoThiThuTrang_2120110029.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Address()
        {
            return View();
        }
        public ActionResult Order()
        {
            return View();
        }
        public ActionResult Seller()
        {
            return View();
        }
        public ActionResult Setting()
        {
            return View();
        }
        public ActionResult Wishlist()
        {
            return View();
        }
    }
}