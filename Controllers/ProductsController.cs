using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KickerShop.Models;

namespace KickerShop.Controllers
{
    public class ProductsController : Controller
    {
        private KickerShopEntities db = new KickerShopEntities();

        // GET: Products
        public ActionResult Index()
        {
            var product = db.ProductSet.Include(p => p.Producer).Include(p => p.Warehouse);
            return View(product.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.ProductSet.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.Producer_id = new SelectList(db.ProducerSet, "Id", "Name");
            ViewBag.War_id = new SelectList(db.WarehouseSet, "Id", "Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Unit_price,Quantity,War_id,Producer_id")] Product product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.ProductSet.Add(product);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    string msg = null;
                    if (e.InnerException == null)
                        msg = "Invalid product data";
                    else
                        msg = e.InnerException.InnerException.Message;
                    ViewBag.Error = msg;
                    ViewBag.Producer_id = new SelectList(db.ProducerSet, "Id", "Name", product.Producer_id);
                    ViewBag.War_id = new SelectList(db.WarehouseSet, "Id", "Name", product.War_id);
                    return View(product);
                }
            }
            ViewBag.Producer_id = new SelectList(db.ProducerSet, "Id", "Name", product.Producer_id);
            ViewBag.War_id = new SelectList(db.WarehouseSet, "Id", "Name", product.War_id);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.ProductSet.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.Producer_id = new SelectList(db.ProducerSet, "Id", "Name", product.Producer_id);
            ViewBag.War_id = new SelectList(db.WarehouseSet, "Id", "Name", product.War_id);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Unit_price,Quantity,War_id,Producer_id")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Producer_id = new SelectList(db.ProducerSet, "Id", "Name", product.Producer_id);
            ViewBag.War_id = new SelectList(db.WarehouseSet, "Id", "Name", product.War_id);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.ProductSet.Find(id);
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
            Product product = db.ProductSet.Find(id);
            db.ProductSet.Remove(product);
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
