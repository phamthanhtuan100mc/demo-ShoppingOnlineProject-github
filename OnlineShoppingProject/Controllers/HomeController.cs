using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineShoppingProject.DAO;
using OnlineShoppingProject.Models;
using PagedList;

namespace OnlineShoppingProject.Controllers
{
    public class HomeController : Controller
    {
        // comaosdjgehg
        private DbShoppingContext db = new DbShoppingContext();

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

        public ActionResult IndexCate(int? cateId, string SearchString, int? Page_No, int Size_Of_Page = 5)
        {
            int? tongSL = TongSoLuong();
            ViewBag.Tongsoluong = tongSL;

            // tính tổng số trang
            int Number_Of_Page = (Page_No ?? 1);
            ViewBag.cateId = cateId;
            var products = db.Products.Include(p => p.category).Where(p=>p.cateId==cateId).OrderBy(p => p.id).ToPagedList(Number_Of_Page, Size_Of_Page);
            return View(products);
        }

        // GET: Home
        public ActionResult Index(string SearchString,  int? Page_No, int Size_Of_Page = 5)
        {
            // số lượng hàng trong giỏ
            int? tongSL = TongSoLuong();
            ViewBag.Tongsoluong = tongSL;

            // tính tổng số trang
            int Number_Of_Page = (Page_No ?? 1);

            // kiểm tra chuỗi tìm kiếm có rõng hay không
            if (String.IsNullOrEmpty(SearchString))
            {
                SearchString = "";
            }
            ViewBag.searchStr = SearchString;
            var products = db.Products.Include(p => p.category).Where(p => p.name.Contains(SearchString) || p.category.name.Contains(SearchString)).OrderBy(p => p.id).ToPagedList(Number_Of_Page, Size_Of_Page);
            return View(products);
        }

        // GET: Home/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Include(p => p.category).Where(p => p.id == id).FirstOrDefault();
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // action này sẽ không được truy cập trực tiếp như những cái kia mà dùng để load thông tin
        [ChildActionOnly]
        public ActionResult Menu()
        {
            var categories = db.Categories.ToList();
            return PartialView(categories);
        }
    }
}
