using HoThiThuTrang_2120110029.Context;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static HoThiThuTrang_2120110029.Common;

namespace HoThiThuTrang_2120110029.Areas.Admin.Controllers
{
    public class BrandController : Controller
    {
        QuanLyBanHangEntities3 objquanLyBanHangEntities3 = new QuanLyBanHangEntities3();

        // GET: Admin/Brand
        //chức năng tìm kiếm sản phẩm Admin
        public ActionResult Index(string currentFilter, string SearchString, int? page)
        {
            var lstBrand = new List<Brand>();
            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = currentFilter;
            }
            if (!string.IsNullOrEmpty(SearchString))
            {
                lstBrand = objquanLyBanHangEntities3.Brands.Where(n => n.Name.Contains(SearchString)).ToList();
            }
            else
            {
                lstBrand = objquanLyBanHangEntities3.Brands.ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            lstBrand = lstBrand.OrderByDescending(n => n.Id).ToList();
            return View(lstBrand.ToPagedList(pageNumber, pageSize));
        }
     


        [HttpGet]
        public ActionResult Create()//tao thuong hieu
        {
            //this.LoadData();
            return View();
        }


        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(Brand objBrand)//tao thuong hieu
        {
            //this.LoadData();
            if (ModelState.IsValid)
            {
                try
                {
                    if (objBrand.ImageUpload != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(objBrand.ImageUpload.FileName);
                        string extension = Path.GetExtension(objBrand.ImageUpload.FileName);
                        fileName = fileName + extension;
                        objBrand.Avatar = fileName;
                        objBrand.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items/brand"), fileName));
                    }
                    objBrand.CreatedOnUtc = DateTime.Now;
                    objquanLyBanHangEntities3.Brands.Add(objBrand);
                    objquanLyBanHangEntities3.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch
                {
                    return RedirectToAction("Index");
                }
            }
            return View(objBrand);
        }

        [HttpGet]
        public ActionResult Details(int id)//xem chi tiet thuong hieu(admin)
        {
            var objBrand = objquanLyBanHangEntities3.Brands.Where(n => n.Id == id).FirstOrDefault();
            return View(objBrand);
        }

        [HttpGet]
        public ActionResult Delete(int id)//xoa thuong hieu(admin)
        {
            var objBrand = objquanLyBanHangEntities3.Brands.Where(n => n.Id == id).FirstOrDefault();
            return View(objBrand);
        }

        [HttpPost]
        public ActionResult Delete(Brand objCat)//xoa thuong hieu(admin)
        {
            var objBrand = objquanLyBanHangEntities3.Brands.Where(n => n.Id == objCat.Id).FirstOrDefault();
            objquanLyBanHangEntities3.Brands.Remove(objBrand);
            objquanLyBanHangEntities3.SaveChanges();

            return RedirectToAction("Index");
        }
        [HttpGet]

        public ActionResult Edit(int id)
        {
            //this.LoadData();
            var objBrand = objquanLyBanHangEntities3.Brands.Where(n => n.Id == id).FirstOrDefault();
            return View(objBrand);
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Brand objBrand, FormCollection form)
        {
            //this.LoadData();
            if (objBrand.ImageUpload != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(objBrand.ImageUpload.FileName);
                //tenhinh
                string extension = Path.GetExtension(objBrand.ImageUpload.FileName);
                //mo rong
                fileName = fileName + extension;
                //tenhinh.jpg
                objBrand.Avatar = fileName;
                objBrand.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items/brand"), fileName));

            }
            else
            {
                objBrand.Avatar = form["oldimage"];
                objquanLyBanHangEntities3.Entry(objBrand).State = EntityState.Modified;
                objquanLyBanHangEntities3.SaveChanges();
                return RedirectToAction("Index");
            }
            objquanLyBanHangEntities3.Entry(objBrand).State = EntityState.Modified;
            objquanLyBanHangEntities3.SaveChanges();
            return RedirectToAction("Index");
        }
        void LoadData()
        {
            Common objCommon = new Common();

            var lstCat = objquanLyBanHangEntities3.Brands.ToList();

            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dtBrand = converter.ToDataTable(lstCat);
            ViewBag.ListBrand = objCommon.ToSelectList(dtBrand, "Id", "Name");

            List<BrandType> lstBrandType = new List<BrandType>();
            BrandType objBrandType = new BrandType();
            objBrandType.Id = 1;
            objBrandType.Name = "Danh mục phổ biến";
            lstBrandType.Add(objBrandType);



            DataTable dtBrandType = converter.ToDataTable(lstBrandType);
            ViewBag.BrandType = objCommon.ToSelectList(dtBrandType, "Id", "Name");
        }
    }
}