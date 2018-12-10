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
    public class OrderDetailsController : Controller
    {
        private KickerShopEntities db = new KickerShopEntities();

        // GET: OrderDetails
        public ActionResult Index()
        {
            var orderDetails = db.OrderDetailSet.Include(o => o.Orders).Include(o => o.Products);
            return View(orderDetails.ToList());
        }

        // GET: OrderDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetails orderDetails = db.OrderDetailSet.Find(id);
            if (orderDetails == null)
            {
                return HttpNotFound();
            }
            return View(orderDetails);
        }

        // GET: OrderDetails/Create
        public ActionResult Create()
        {
            ViewBag.Order_id = new SelectList(db.OrderSet, "Id", "Id");
            ViewBag.Product_id = new SelectList(db.ProductSet, "Id", "Name");
            return View();
        }

        // POST: OrderDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderDetail_id,Order_id,Product_id,Quantity")] OrderDetails orderDetails)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.OrderDetailSet.Add(orderDetails);
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

                    ViewBag.Order_id = new SelectList(db.OrderSet, "Id", "Id", orderDetails.Order_id);
                    ViewBag.Product_id = new SelectList(db.ProductSet, "Id", "Name", orderDetails.Product_id);
                    ViewBag.Error = msg;
                    return View(orderDetails);
                }
            }

            ViewBag.Order_id = new SelectList(db.OrderSet, "Id", "Id", orderDetails.Order_id);
            ViewBag.Product_id = new SelectList(db.ProductSet, "Id", "Name", orderDetails.Product_id);
            return View(orderDetails);
        }

        // GET: OrderDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetails orderDetails = db.OrderDetailSet.Find(id);
            if (orderDetails == null)
            {
                return HttpNotFound();
            }
            ViewBag.Order_id = new SelectList(db.OrderSet, "Id", "Id", orderDetails.Order_id);
            ViewBag.Product_id = new SelectList(db.ProductSet, "Id", "Name", orderDetails.Product_id);
            return View(orderDetails);
        }

        // POST: OrderDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderDetail_id,Order_id,Product_id,Quantity")] OrderDetails orderDetails)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderDetails).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Order_id = new SelectList(db.OrderSet, "Id", "Id", orderDetails.Order_id);
            ViewBag.Product_id = new SelectList(db.ProductSet, "Id", "Name", orderDetails.Product_id);
            return View(orderDetails);
        }

        // GET: OrderDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetails orderDetails = db.OrderDetailSet.Find(id);
            if (orderDetails == null)
            {
                return HttpNotFound();
            }
            return View(orderDetails);
        }

        // POST: OrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderDetails orderDetails = db.OrderDetailSet.Find(id);
            db.OrderDetailSet.Remove(orderDetails);
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
