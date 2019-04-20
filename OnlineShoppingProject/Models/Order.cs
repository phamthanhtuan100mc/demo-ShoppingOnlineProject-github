using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineShoppingProject.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Tình trạng giao hàng")]
        public bool TinhTrangGiaoHang { get; set; }

        [DisplayName("Đã thanh toán")]
        public bool DaThanhToan { get; set; }

        [DisplayName("Ngày đặt")]
        public DateTime NgayDat { get; set; }

        [DisplayName("Ngày giao")]
        public DateTime NgayGiao { get; set; }


        [DisplayName("Khách hàng")]
        public string MaKH { get; set; }

        [ForeignKey("OrderId")]
        ICollection<OrdersDetail> OrdersDetails { get; set; }

    }
}