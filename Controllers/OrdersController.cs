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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,OrderDate,Client_id,DeliveryType_id,PayType_id")] Orders orders,
                                    List<OrderDetails> details)
        {
            orders.OrderDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.OrderSet.Add(orders);
                db.SaveChanges();
                int id = db.OrderSet.FirstOrDefault(o => o.Id == orders.Id).Id;
                foreach (var detail in details)
                {
                    detail.Order_id = id;
                }
                db.OrderDetailSet.AddRange(details);
                db.SaveChanges();
                // TO do 
                // tu stworzyc Payment z uzyciem procedury
                return RedirectToAction("Index");
            }
            ViewBag.Client_id = new SelectList(db.ClientSet, "Id", "Name", orders.Client_id);
            ViewBag.DeliveryType_id = new SelectList(db.Delivery_typeSet, "Id", "Name", orders.DeliveryType_id);
            ViewBag.PayType_id = new SelectList(db.Payment_typeSet, "Id", "Name", orders.PayType_id);
            return View(orders);
        }

        // For adding product to order
        public ActionResult OrderDetails(int? i)
        {
            ViewBag.Product_id = new SelectList(db.ProductSet, "Id", "Name");
            ViewBag.i = i;
            return PartialView();
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,OrderDate,Client_id,DeliveryType_id,PayType_id")] Orders orders,
                                 List<OrderDetails> details)
        {
            if (ModelState.IsValid)
            {
                Orders ord = db.OrderSet.FirstOrDefault(o => o.Id == orders.Id);
                ord.Client_id = orders.Client_id;
                ord.DeliveryType_id = orders.DeliveryType_id;
                ord.PayType_id = orders.PayType_id;
                db.SaveChanges();
                if(details != null)
                foreach (var detail in details)
                {
                    OrderDetails det = db.OrderDetailSet.FirstOrDefault(o => o.OrderDetail_id == detail.OrderDetail_id);
                    det.Product_id = detail.Product_id;
                    det.Quantity = detail.Quantity;
                    db.SaveChanges();
                }
                ViewBag.Client_id = new SelectList(db.ClientSet, "Id", "Name", orders.Client_id);
                ViewBag.DeliveryType_id = new SelectList(db.Delivery_typeSet, "Id", "Name", orders.DeliveryType_id);
                ViewBag.PayType_id = new SelectList(db.Payment_typeSet, "Id", "Name", orders.PayType_id);
                return View(ord);
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

        public ActionResult DeleteDetail(int[] details)
        {
            if (details != null)
            {
                foreach (var ordDetailID in details)
                {
                    var detail = db.OrderDetailSet.Where(o => o.OrderDetail_id == ordDetailID).FirstOrDefault();
                    db.OrderDetailSet.Remove(detail);
                }
                db.SaveChanges();
            }
            return Json("OK");
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
