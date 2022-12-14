using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HoThiThuTrang_2120110029.Context;
using PagedList;
using static HoThiThuTrang_2120110029.Common;

namespace HoThiThuTrang_2120110029.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        QuanLyBanHangEntities3 objquanLyBanHangEntities3 = new QuanLyBanHangEntities3();

        // GET: Admin/Product
        //chức năng tìm kiếm sản phẩm Admin
        public ActionResult Index(string currentFilter, string SearchString, int? page)
        {
            var lstProduct = new List<Product>();
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
                lstProduct = objquanLyBanHangEntities3.Products.Where(n => n.Name.Contains(SearchString)).ToList();
            }
            else
            {
                lstProduct = objquanLyBanHangEntities3.Products.ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            int pageSize = 4; 
            int pageNumber = (page ?? 1);
            lstProduct = lstProduct.OrderByDescending(n => n.Id).ToList();
            return View(lstProduct.ToPagedList(pageNumber, pageSize));
        }
        //private dynamic ToSelectList(DataTable dtCategory, string v1, string v2)
        //{
        //    throw new NotImplementedException();
        //}


        [HttpGet]
        public ActionResult Create()//tao san pham
        {
            this.LoadData();
            return View();
        }


        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(Product objProduct)//tao san pham
        {
            this.LoadData();
            if (ModelState.IsValid)
            {
                try
                {
                    if (objProduct.ImageUpload != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(objProduct.ImageUpload.FileName);
                        string extension = Path.GetExtension(objProduct.ImageUpload.FileName);
                        fileName = fileName + extension;
                        objProduct.Avatar = fileName;
                        objProduct.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items"), fileName));
                    }

                    objProduct.CreatedOnUtc = DateTime.Now;
                    objquanLyBanHangEntities3.Products.Add(objProduct);
                    objquanLyBanHangEntities3.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View(objProduct);
                }
            }
            return View(objProduct);
        }

        [HttpGet]
        public ActionResult Details(int id)//xem chi tiet san pham(admin)
        {
            var objProduct = objquanLyBanHangEntities3.Products.Where(n => n.Id == id).FirstOrDefault();
            return View(objProduct);
        }

        [HttpGet]
        public ActionResult Delete(int id)//xoa san pham(admin)
        {
            var objProduct = objquanLyBanHangEntities3.Products.Where(n => n.Id == id).FirstOrDefault();
            return View(objProduct);
        }

        [HttpPost]
        public ActionResult Delete(Product objPro)//xoa san pham(admin)
        {
            var objProduct = objquanLyBanHangEntities3.Products.Where(n => n.Id == objPro.Id).FirstOrDefault();
            objquanLyBanHangEntities3.Products.Remove(objProduct);
            objquanLyBanHangEntities3.SaveChanges();

            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            this.LoadData();
            var objProduct = objquanLyBanHangEntities3.Products.Where(n => n.Id == id).FirstOrDefault();
            return View(objProduct);
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product objProduct, FormCollection form)
        {
            //this.LoadData();
            if (objProduct.ImageUpload != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(objProduct.ImageUpload.FileName);
                //tenhinh
                string extension = Path.GetExtension(objProduct.ImageUpload.FileName);
                //mo rong
                fileName = fileName + extension;
                //tenhinh.jpg
                objProduct.Avatar = fileName;
                objProduct.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items"), fileName));

            }
            else
            {
                objProduct.Avatar = form["oldimage"];
                objquanLyBanHangEntities3.Entry(objProduct).State = EntityState.Modified;
                objquanLyBanHangEntities3.SaveChanges();
                return RedirectToAction("Index");
            }
            objquanLyBanHangEntities3.Entry(objProduct).State = EntityState.Modified;
            objquanLyBanHangEntities3.SaveChanges();
            return RedirectToAction("Index");
        }
        void LoadData()
        {
            Common objCommon = new Common();
            // Láy dữ liệu danh mục dưới DB
            var lstCat = objquanLyBanHangEntities3.Categories.ToList();
            // Convert sang select list dạng value , text
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dtCategory = converter.ToDataTable(lstCat);
            ViewBag.ListCategory = objCommon.ToSelectList(dtCategory, "Id", "Name");

            //Lấy dữ liệu thương hiệu dưới  DB
            var lstBrand = objquanLyBanHangEntities3.Brands.ToList();
            DataTable dtBrand = converter.ToDataTable(lstBrand);
            //convert sang select list  dạng value , text 
            ViewBag.ListBrand = objCommon.ToSelectList(dtBrand, "Id", "Name");

            //Loai san pham
            List<ProductType> lstProductType = new List<ProductType>();
            ProductType objProductType = new ProductType();
            objProductType.Id = 01;
            objProductType.Name = "Giảm giá sốc";
            lstProductType.Add(objProductType);

            objProductType = new ProductType();
            objProductType.Id = 02;
            objProductType.Name = "Đề xuất";
            lstProductType.Add(objProductType);

            DataTable dtProductType = converter.ToDataTable(lstProductType);
            //convert sang select list dang value, text
            ViewBag.ProductType = objCommon.ToSelectList(dtProductType, "Id", "Name");
        }
    }
}