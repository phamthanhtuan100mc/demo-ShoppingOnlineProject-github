using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace OnlineShoppingProject.Models
{
    public class CartItem
    {
        [DisplayName("Sản phẩm")]
        public Product product { get; set; }

        [DisplayName("Số lượng")]
        public int quantity { get; set; }

        public CartItem() { }

        public CartItem(Product product, int quantity)
        {
            this.product = product;
            this.quantity = quantity;
        }

        public double totalprice { get { return product.price * quantity; } }
    }
}