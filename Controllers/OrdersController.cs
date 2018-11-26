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
    public class OrdersController : Controller
    {
        private KickerShopEntities db = new KickerShopEntities();

        // GET: Orders
        public ActionResult Index()
        {
            var orders = db.OrderSet.Include(o => o.Clients).Include(o => o.Delivery_types).Include(o => o.Payment_types);
            return View(orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders orders = db.OrderSet.Find(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            return View(orders);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.Client_id = new SelectList(db.ClientSet, "Id", "Name");
            ViewBag.DeliveryType_id = new SelectList(db.Delivery_typeSet, "Id", "Name");
            ViewBag.PayType_id = new SelectList(db.Payment_typeSet, "Id", "Name");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,OrderDate,Client_id,DeliveryType_id,PayType_id")] Orders orders)
        {
            if (ModelState.IsValid)
            {
                db.OrderSet.Add(orders);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Client_id = new SelectList(db.ClientSet, "Id", "Name", orders.Client_id);
            ViewBag.DeliveryType_id = new SelectList(db.Delivery_typeSet, "Id", "Name", orders.DeliveryType_id);
            ViewBag.PayType_id = new SelectList(db.Payment_typeSet, "Id", "Name", orders.PayType_id);
            return View(orders);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders orders = db.OrderSet.Find(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            ViewBag.Client_id = new SelectList(db.ClientSet, "Id", "Name", orders.Client_id);
            ViewBag.DeliveryType_id = new SelectList(db.Delivery_typeSet, "Id", "Name", orders.DeliveryType_id);
            ViewBag.PayType_id = new SelectList(db.Payment_typeSet, "Id", "Name", orders.PayType_id);
            return View(orders);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,OrderDate,Client_id,DeliveryType_id,PayType_id")] Orders orders)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orders).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Client_id = new SelectList(db.ClientSet, "Id", "Name", orders.Client_id);
            ViewBag.DeliveryType_id = new SelectList(db.Delivery_typeSet, "Id", "Name", orders.DeliveryType_id);
            ViewBag.PayType_id = new SelectList(db.Payment_typeSet, "Id", "Name", orders.PayType_id);
            return View(orders);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders orders = db.OrderSet.Find(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            return View(orders);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Orders orders = db.OrderSet.Find(id);
            db.OrderSet.Remove(orders);
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
