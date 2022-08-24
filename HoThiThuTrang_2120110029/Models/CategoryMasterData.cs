using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HoThiThuTrang_2120110029.Models
{
    public partial class CategoryMasterData
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên danh mục!")]
        [Display(Name = "Tên danh mục")]
        public string Name { get; set; }
        [Display(Name = "Hình ảnh hiển thị")]
        public string Avatar { get; set; }
        public string Slug { get; set; }
        public Nullable<bool> ShowOnHomePage { get; set; }
        [Display(Name = "Thứ tự hiển thị")]
        public Nullable<int> DisplayOrder { get; set; }
        public Nullable<bool> Deleted { get; set; }
        public Nullable<System.DateTime> CreatedOnUtc { get; set; }
        public Nullable<System.DateTime> UpdatedOnUtc { get; set; }


    }
}