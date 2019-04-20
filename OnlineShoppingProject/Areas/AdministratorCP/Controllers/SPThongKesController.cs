using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineShoppingProject.Areas.AdministratorCP.Models;
using OnlineShoppingProject.DAO;

namespace OnlineShoppingProject.Areas.AdministratorCP.Controllers
{
    public class SPThongKesController : Controller
    {
        private DbShoppingContext db = new DbShoppingContext();

        // GET: AdministratorCP/SPThongKes
        public ActionResult Index()
        {
            return View(db.SPThongKes.ToList());
        }

        // GET: AdministratorCP/SPThongKes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SPThongKe sPThongKe = db.SPThongKes.Find(id);
            if (sPThongKe == null)
            {
                return HttpNotFound();
            }
            return View(sPThongKe);
        }

        // GET: AdministratorCP/SPThongKes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdministratorCP/SPThongKes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,quantity,price")] SPThongKe sPThongKe)
        {
            if (ModelState.IsValid)
            {
                db.SPThongKes.Add(sPThongKe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sPThongKe);
        }

        // GET: AdministratorCP/SPThongKes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SPThongKe sPThongKe = db.SPThongKes.Find(id);
            if (sPThongKe == null)
            {
                return HttpNotFound();
            }
            return View(sPThongKe);
        }

        // POST: AdministratorCP/SPThongKes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,quantity,price")] SPThongKe sPThongKe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sPThongKe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sPThongKe);
        }

        // GET: AdministratorCP/SPThongKes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SPThongKe sPThongKe = db.SPThongKes.Find(id);
            if (sPThongKe == null)
            {
                return HttpNotFound();
            }
            return View(sPThongKe);
        }

        // POST: AdministratorCP/SPThongKes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SPThongKe sPThongKe = db.SPThongKes.Find(id);
            db.SPThongKes.Remove(sPThongKe);
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

        public ActionResult ThongKeSanPham()
        {
            var products = db.Products.ToList();
            var result = new List<SPThongKe>();
            foreach(var item in products)
            {
                result.Add(new SPThongKe { id = item.id, name = item.name, quantity = item.amount, price = item.price });
            }
            return View(result);
        }
    }
}
