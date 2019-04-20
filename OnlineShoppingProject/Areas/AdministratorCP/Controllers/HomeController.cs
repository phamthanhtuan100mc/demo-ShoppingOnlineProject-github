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

namespace OnlineShoppingProject.Areas.AdministratorCP.Controllers
{
    public class HomeController : Controller
    {
        private DbShoppingContext db = new DbShoppingContext();

        // GET: Home
        public ActionResult Index(string SearchString,  int? Page_No, int Size_Of_Page = 5)
        {
            // tính tổng số trang
            int Number_Of_Page = (Page_No ?? 1);

            // kiểm tra chuỗi tìm kiếm có rõng hay không
            if (String.IsNullOrEmpty(SearchString))
            {
                SearchString = "";
            }

            var products = db.Products.Include(p => p.category).Where(p => p.name.Contains(SearchString)).OrderBy(p => p.id).ToPagedList(Number_Of_Page, Size_Of_Page);
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
