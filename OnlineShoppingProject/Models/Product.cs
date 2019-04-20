using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineShoppingProject.Models
{
    public class Product
    {
        [Key]
        public int id { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Tên sản phẩm")]
        public string name { get; set; }

        [Range(0, 10000000000)]
        [DisplayName("Giá bán")]
        public double price { get; set; }

        [Range(0, 10000)]
        [DisplayName("Số lượng")]
        public int amount { get; set; }

        [StringLength(500)]
        [DisplayName("Mô tả")]
        public string description { get; set; }

        [StringLength(250)]
        [DisplayName("Hình ảnh")]
        public string thumbnail { get; set; }

        [Required]
        [DisplayName("Trạng thái")]
        public bool valid { get; set; }

        [ForeignKey("category")]
        [DisplayName("Danh mục")]
        public int cateId { get; set; }
        public Category category { get; set; }
        
    }
}