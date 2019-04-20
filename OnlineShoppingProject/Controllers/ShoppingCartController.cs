using OnlineShoppingProject.DAO;
using OnlineShoppingProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PagedList;
using System.Web.Security;

namespace OnlineShoppingProject.Controllers
{
    public class ShoppingCartController : Controller
    {
        
        private DbShoppingContext db = new DbShoppingContext();

        public List<CartItem> LayGioHang()
        {
            List<CartItem> lstGioHang = Session["cart"] as List<CartItem>;
            if (lstGioHang == null)
            {
                lstGioHang = new List<CartItem>();
                Session["cart"] = lstGioHang;
            }
            return lstGioHang;
        }

        public int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<CartItem> lstGioHang = Session["cart"] as List<CartItem>;
            if (lstGioHang != null)
            {
                iTongSoLuong = lstGioHang.Sum(p => p.quantity);

            }
            return iTongSoLuong;
        }

        public double TongTien()
        {
            double tongtien = 0;
            List<CartItem> cart = (List<CartItem>)Session["cart"];
            if (cart != null)
            {
                tongtien = cart.Sum(n => n.totalprice);
            }
            return tongtien;
        }


        private int isExisting(int id)
        {
            List<CartItem> cart = (List<CartItem>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].product.id == id)
                {
                    return i;
                }
            }
            return -1;
        }

        

        public ActionResult Orders()
        {
            int? tongSL = TongSoLuong();
            ViewBag.Tongsoluong = tongSL;

            ViewBag.TongTien = TongTien();
            return View((List<CartItem>)Session["cart"]);
        }


        public ActionResult ClearCart()
        {
            List<CartItem> cart = (List<CartItem>)Session["cart"];
            cart.Clear();
            return RedirectToAction("Orders", "ShoppingCart");
        }

        public ActionResult Delete(int id)
        {
            List<CartItem> cart = (List<CartItem>)Session["cart"];
            CartItem sanpham = cart.Single(n => n.product.id == id);
            if (sanpham != null)
            {
                cart.RemoveAll(n => n.product.id == id);

            }
            return RedirectToAction("Orders"/*, new { id = id, quantity = -2 }*/);
        }

        //public void AddToCart(int id, int quantity)
        //{
        //    if (Session["cart"] == null)
        //    {
        //        // tại 1 list cartItem mới, đặt tên là cart
        //        List<CartItem> cart = new List<CartItem>();

        //        // thêm 1 sản phẩn mới vào cart
        //        cart.Add(new CartItem(db.Products.Find(id), 1));

        //        // gán list cart vừa thêm 1 sản phẩm cho session
        //        Session["cart"] = cart;
        //    }
        //    else
        //    {
        //        List<CartItem> cart = (List<CartItem>)Session["cart"];
        //        int index = isExisting(id);
        //        if (index == -1)
        //        {
        //            cart.Add(new CartItem(db.Products.Find(id), 1));
        //        }
        //    }
        //}

        //Phương thức cộng/trừ sản phẩm
        public ActionResult ValueChanged(int id, int quantity, int? flag, int? Page_No, int? cateId)
        {
            
            // nếu session giỏ hàng rỗng
            if (Session["cart"] == null)
            {
                // tại 1 list cartItem mới, đặt tên là cart
                List<CartItem> cart = new List<CartItem>();

                // thêm 1 sản phẩn mới vào cart
                cart.Add(new CartItem(db.Products.Find(id), 1));
                
                // gán list cart vừa thêm 1 sản phẩm cho session
                Session["cart"] = cart;
            }
            else
            {
                List<CartItem> cart = (List<CartItem>)Session["cart"];
                int index = isExisting(id);
                if (index == -1)
                {
                    cart.Add(new CartItem(db.Products.Find(id), 1));
                }
                else
                {
                    cart[index].quantity = cart[index].quantity + quantity;
                    if(cart[index].quantity == 0)
                    {
                        cart.Remove(cart[index]);
                    }
                }
                Session["cart"] = cart;
            }

            

            if (flag == 1)
            {
                if(cateId != null)
                {
                    return RedirectToAction("IndexCate", "Home", new { cateId, Page_No});
                }
                return RedirectToAction("Index", "Home", new { Page_No });
            }
            return RedirectToAction("Orders", "ShoppingCart");
        }

        [HttpGet]
        [Authorize(Roles="MEMBER")]
        public ActionResult Dathang()
        {
            int? tongSL = TongSoLuong();
            ViewBag.Tongsoluong = tongSL;

            ViewBag.HoTen = User.Identity.GetHoTen();
            ViewBag.DiaChi = User.Identity.GetDiaChi();
            ViewBag.SDT = User.Identity.GetPhoneNumber();
            ViewBag.Email = User.Identity.GetUserName();
            


            //ViewBag.Tongsoluong = TongSoLuong();
            return View();
        }

        //[HttpPost]
        //public ActionResult DatHang(FormCollection collection)
        //{
        //    Order ddh = new Order();
        //    customer kh = (customer)Session["Taikhoan"];
        //    List<GioHang> gh = LayGioHang();
        //    ddh.MaKH = kh.MaKH;
        //    ddh.NgayDat = DateTime.Now;
        //    var ngaygiao = String.Format("{0:MM/dd/yyyy}", collection["Ngaygiao"]);
        //    ddh.NgayGiao = DateTime.Parse(ngaygiao);
        //    ddh.TinhTrangGiaoHang = false;
        //    ddh.DaThanhToan = false;
        //    data.orders.InsertOnSubmit(ddh);
        //    data.SubmitChanges();

        //    foreach (var item in gh)
        //    {
        //        ordersDetail ctgh = new ordersDetail();
        //        ctgh.MaDonHang = ddh.id;
        //        ctgh.MaSP = item.iId;
        //        ctgh.SoLuong = item.iQuantity;
        //        ctgh.DonGia = (decimal)item.sPrice;
        //        data.ordersDetails.InsertOnSubmit(ctgh);
        //    }
        //    data.SubmitChanges();
        //    Session["Giohang"] = null;
        //    return RedirectToAction("Xacnhandonhang", "GioHang");
        //}

        public ActionResult PlaceOrder()
        {
            List<CartItem> cart = (List<CartItem>)Session["cart"];
            cart.Clear();
            Session["cart"] = null;
            return View();
        }
    }
}