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

namespace OnlineShoppingProject.Areas.AdministratorCP.Controllers
{
    [Authorize(Roles = "MANAGER, ADMIN")]
    public class ProductsController : Controller
    {
        private DbShoppingContext db = new DbShoppingContext();

        // GET: Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.category);
            return View(products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Include(a => a.category).Where(a => a.id == id).FirstOrDefault();
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.cateId = new SelectList(db.Categories, "id", "name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,price,amount,description,thumbnail,valid,cateId")] Product product)
        {
            if (ModelState.IsValid)
            {
                var f = Request.Files["hinhanh"];

                if (f != null && f.ContentLength > 0)
                {
                    var path = Server.MapPath("~/ImageUpload/" + f.FileName);
                    f.SaveAs(path);
                    product.thumbnail = "/ImageUpload/" + f.FileName;
                }

                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cateId = new SelectList(db.Categories, "id", "name", product.cateId);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.cateId = new SelectList(db.Categories, "id", "name", product.cateId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,price,amount,description,thumbnail,valid,cateId")] Product product)
        {
            if (ModelState.IsValid)
            {
                var f = Request.Files["hinhanh"];

                if (f != null && f.ContentLength > 0)
                {
                    var path = Server.MapPath("~/ImageUpload/" + f.FileName);
                    f.SaveAs(path);
                    product.thumbnail = "/ImageUpload/" + f.FileName;
                }
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cateId = new SelectList(db.Categories, "id", "name", product.cateId);
            return View(product);
        }

        [Authorize(Roles = "ADMIN")]
        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Include(a => a.category).Where(a => a.id == id).FirstOrDefault();
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
