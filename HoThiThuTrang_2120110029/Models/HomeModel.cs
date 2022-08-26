using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HoThiThuTrang_2120110029.Context;
namespace HoThiThuTrang_2120110029.Models
{
    public class HomeModel
    {
        public List<Product> ListProduct { get; set; }
        public List<Category> ListCategory { get; set; }
        public List<Brand> ListBrand { get; set; }

    }
}