using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HoThiThuTrang_2120110029.Context;
namespace HoThiThuTrang_2120110029.Models
{
    public class CartModel
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}