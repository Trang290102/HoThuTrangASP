using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HoThiThuTrang_2120110029.Models
{
    public class OrderMasterData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Display(Name = "Id người mua")]
        public Nullable<int> UserId { get; set; }
        [Display(Name = "Trạng thái")]
        public Nullable<int> Status { get; set; }
        [Display(Name = "Thời gian tạo đơn hàng")]

        public Nullable<System.DateTime> CreatedOnUtc { get; set; }

    }
}