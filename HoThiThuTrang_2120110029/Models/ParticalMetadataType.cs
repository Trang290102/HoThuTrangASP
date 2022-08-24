using System;
using System.Collections.Generic;
using System.Linq;
using HoThiThuTrang_2120110029.Context;
using HoThiThuTrang_2120110029.Models;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HoThiThuTrang_2120110029.Context
{
    [MetadataType(typeof(UserMasterData))]
    public partial class Users
    {

    }

    [MetadataType(typeof(ProductMasterData))]
    public partial class Product
    {
        [NotMapped]
        public System.Web.HttpPostedFileBase ImageUpload { get; set; }
    }

    [MetadataType(typeof(CategoryMasterData))]
    public partial class Category
    {
        [NotMapped]
        public System.Web.HttpPostedFileBase ImageUpload { get; set; }
    }
    [MetadataType(typeof(BrandMasterData))]
    public partial class Brand
    {
        [NotMapped]
        public System.Web.HttpPostedFileBase ImageUpload { get; set; }
    }

}