using HoThiThuTrang_2120110029.Context;
using HoThiThuTrang_2120110029.Models;
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
    public class CategoryController : Controller
    {
        QuanLyBanHangEntities3 objquanLyBanHangEntities3 = new QuanLyBanHangEntities3();

        // GET: Admin/Category
        //chức năng tìm kiếm sản phẩm Admin
        public ActionResult Index(string currentFilter, string SearchString, int? page)
        {
            var lstCategory = new List<Category>();
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
                lstCategory = objquanLyBanHangEntities3.Categories.Where(n => n.Name.Contains(SearchString)).ToList();
            }
            else
            {
                lstCategory = objquanLyBanHangEntities3.Categories.ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            lstCategory = lstCategory.OrderByDescending(n => n.Id).ToList();
            return View(lstCategory.ToPagedList(pageNumber, pageSize));
        }


        [HttpGet]
        public ActionResult Create()//tao danh muc
        {
            this.LoadData();
            return View();
        }


        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(Category objCategory)//tao danh muc
        {
            this.LoadData();
            if (ModelState.IsValid)
            {
                try
                {
                    if (objCategory.ImageUpload != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(objCategory.ImageUpload.FileName);
                        string extension = Path.GetExtension(objCategory.ImageUpload.FileName);
                        fileName = fileName + extension;
                        objCategory.Avatar = fileName;
                        objCategory.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/category"), fileName));
                    }
                    objCategory.CreatedOnUtc = DateTime.Now;
                    objquanLyBanHangEntities3.Categories.Add(objCategory);
                    objquanLyBanHangEntities3.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View(objCategory);
                }
            }
            return View(objCategory);
        }

        [HttpGet]
        public ActionResult Details(int id)//xem chi tiet danh muc(admin)
        {
            var objCategory = objquanLyBanHangEntities3.Categories.Where(n => n.Id == id).FirstOrDefault();
            return View(objCategory);
        }

        [HttpGet]
        public ActionResult Delete(int id)//xoa danh muc(admin)
        {
            var objCategory = objquanLyBanHangEntities3.Categories.Where(n => n.Id == id).FirstOrDefault();
            return View(objCategory);
        }

        [HttpPost]
        public ActionResult Delete(Category objCat)//xoa danh muc(admin)
        {
            var objCategory = objquanLyBanHangEntities3.Categories.Where(n => n.Id == objCat.Id).FirstOrDefault();
            objquanLyBanHangEntities3.Categories.Remove(objCategory);
            objquanLyBanHangEntities3.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            this.LoadData();
            var objCategory = objquanLyBanHangEntities3.Categories.Where(n => n.Id == id).FirstOrDefault();
            return View(objCategory);
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category objCategory, FormCollection form)
        {
            //this.LoadData();
            if (objCategory.ImageUpload != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(objCategory.ImageUpload.FileName);
                //tenhinh
                string extension = Path.GetExtension(objCategory.ImageUpload.FileName);
                //png
                fileName = fileName + extension;
                //tenhinh.png
                objCategory.Avatar = fileName;
                objCategory.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/category"), fileName));

            }
            else
            {
                objCategory.Avatar = form["oldimage"];
                objquanLyBanHangEntities3.Entry(objCategory).State = EntityState.Modified;
                objquanLyBanHangEntities3.SaveChanges();
                return RedirectToAction("Index");
            }
            objquanLyBanHangEntities3.Entry(objCategory).State = EntityState.Modified;
            objquanLyBanHangEntities3.SaveChanges();
            return RedirectToAction("Index");
        }
        void LoadData()
        {
            Common objCommon = new Common();

            var lstCat = objquanLyBanHangEntities3.Categories.ToList();

            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dtCategory = converter.ToDataTable(lstCat);
            ViewBag.ListCategory = objCommon.ToSelectList(dtCategory, "Id", "Name");

            List<CategoryType> lstCategoryType = new List<CategoryType>();
            CategoryType objCategoryType = new CategoryType();
            objCategoryType.Id = 1;
            objCategoryType.Name = "Danh mục phổ biến";
            lstCategoryType.Add(objCategoryType);



            DataTable dtCategoryType = converter.ToDataTable(lstCategoryType);
            ViewBag.CategoryType = objCommon.ToSelectList(dtCategoryType, "Id", "Name");
        }
    }
}