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
    public class PaymentTypesController : Controller
    {
        private KickerShopEntities db = new KickerShopEntities();

        // GET: PaymentTypes
        public ActionResult Index()
        {
            return View(db.Payment_typeSet.ToList());
        }

        // GET: PaymentTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment_types payment_types = db.Payment_typeSet.Find(id);
            if (payment_types == null)
            {
                return HttpNotFound();
            }
            return View(payment_types);
        }

        // GET: PaymentTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PaymentTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Unit_price")] Payment_types payment_types)
        {
            if (ModelState.IsValid)
            {
                db.Payment_typeSet.Add(payment_types);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(payment_types);
        }

        // GET: PaymentTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment_types payment_types = db.Payment_typeSet.Find(id);
            if (payment_types == null)
            {
                return HttpNotFound();
            }
            return View(payment_types);
        }

        // POST: PaymentTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Unit_price")] Payment_types payment_types)
        {
            if (ModelState.IsValid)
            {
                db.Entry(payment_types).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(payment_types);
        }

        // GET: PaymentTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment_types payment_types = db.Payment_typeSet.Find(id);
            if (payment_types == null)
            {
                return HttpNotFound();
            }
            return View(payment_types);
        }

        // POST: PaymentTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Payment_types payment_types = db.Payment_typeSet.Find(id);
            db.Payment_typeSet.Remove(payment_types);
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
