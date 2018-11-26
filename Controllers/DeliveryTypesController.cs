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
    public class DeliveryTypesController : Controller
    {
        private KickerShopEntities db = new KickerShopEntities();

        // GET: DeliveryTypes
        public ActionResult Index()
        {
            return View(db.Delivery_typeSet.ToList());
        }

        // GET: DeliveryTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Delivery_types delivery_types = db.Delivery_typeSet.Find(id);
            if (delivery_types == null)
            {
                return HttpNotFound();
            }
            return View(delivery_types);
        }

        // GET: DeliveryTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DeliveryTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Unit_price")] Delivery_types delivery_types)
        {
            if (ModelState.IsValid)
            {
                db.Delivery_typeSet.Add(delivery_types);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(delivery_types);
        }

        // GET: DeliveryTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Delivery_types delivery_types = db.Delivery_typeSet.Find(id);
            if (delivery_types == null)
            {
                return HttpNotFound();
            }
            return View(delivery_types);
        }

        // POST: DeliveryTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Unit_price")] Delivery_types delivery_types)
        {
            if (ModelState.IsValid)
            {
                db.Entry(delivery_types).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(delivery_types);
        }

        // GET: DeliveryTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Delivery_types delivery_types = db.Delivery_typeSet.Find(id);
            if (delivery_types == null)
            {
                return HttpNotFound();
            }
            return View(delivery_types);
        }

        // POST: DeliveryTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Delivery_types delivery_types = db.Delivery_typeSet.Find(id);
            db.Delivery_typeSet.Remove(delivery_types);
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
