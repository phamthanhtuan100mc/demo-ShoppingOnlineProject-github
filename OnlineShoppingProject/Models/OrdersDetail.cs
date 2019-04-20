using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineShoppingProject.Models
{
    public class OrdersDetail
    {
        [Key]
        public string MaDonHang { get; set; }
        
        [Required]
        public int SoLuong { get; set; }

        [Required]
        public double DonGia { get; set; }

        [Required]
        [DisplayName("Đơn hàng")]
        public string OrderId { get; set; }
        public Order Order { get; set; }
    }
}