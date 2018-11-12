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
            var order = db.OrderSet.Include(o => o.Client).Include(o => o.Delivery_type).Include(o => o.Payment).Include(o => o.Product);
            return View(order.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.OrderSet.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.Cli_id = new SelectList(db.ClientSet, "Id", "Name");
            ViewBag.Del_id = new SelectList(db.Delivery_typeSet, "Id", "Name");
            ViewBag.Pay_id = new SelectList(db.PaymentSet, "Id", "Name");
            ViewBag.Pro_id = new SelectList(db.ProductSet, "Id", "Name");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Cli_id,Del_id,Pay_id,Pro_id")] Order order)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.OrderSet.Add(order);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    string msg = null;
                    if (e.InnerException == null)
                        msg = "Invalid order data";
                    else
                        msg = e.InnerException.InnerException.Message;
                    ViewBag.Error = msg;
                    ViewBag.Cli_id = new SelectList(db.ClientSet, "Id", "Name", order.Cli_id);
                    ViewBag.Del_id = new SelectList(db.Delivery_typeSet, "Id", "Name", order.Del_id);
                    ViewBag.Pay_id = new SelectList(db.PaymentSet, "Id", "Name", order.Pay_id);
                    ViewBag.Pro_id = new SelectList(db.ProductSet, "Id", "Name", order.Pro_id);
                    return View(order);
                }
            }
            ViewBag.Cli_id = new SelectList(db.ClientSet, "Id", "Name", order.Cli_id);
            ViewBag.Del_id = new SelectList(db.Delivery_typeSet, "Id", "Name", order.Del_id);
            ViewBag.Pay_id = new SelectList(db.PaymentSet, "Id", "Name", order.Pay_id);
            ViewBag.Pro_id = new SelectList(db.ProductSet, "Id", "Name", order.Pro_id);
            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.OrderSet.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cli_id = new SelectList(db.ClientSet, "Id", "Name", order.Cli_id);
            ViewBag.Del_id = new SelectList(db.Delivery_typeSet, "Id", "Name", order.Del_id);
            ViewBag.Pay_id = new SelectList(db.PaymentSet, "Id", "Name", order.Pay_id);
            ViewBag.Pro_id = new SelectList(db.ProductSet, "Id", "Name", order.Pro_id);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Cli_id,Del_id,Pay_id,Pro_id")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Cli_id = new SelectList(db.ClientSet, "Id", "Name", order.Cli_id);
            ViewBag.Del_id = new SelectList(db.Delivery_typeSet, "Id", "Name", order.Del_id);
            ViewBag.Pay_id = new SelectList(db.PaymentSet, "Id", "Name", order.Pay_id);
            ViewBag.Pro_id = new SelectList(db.ProductSet, "Id", "Name", order.Pro_id);
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.OrderSet.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.OrderSet.Find(id);
            db.OrderSet.Remove(order);
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
