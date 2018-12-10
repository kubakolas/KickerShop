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
    public class ClientsController : Controller
    {
        private KickerShopEntities db = new KickerShopEntities();

        // GET: Clients
        public ActionResult Index()
        {
            return View(db.ClientSet.ToList());
        }

        // GET: Clients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clients clients = db.ClientSet.Find(id);
            if (clients == null)
            {
                return HttpNotFound();
            }
            return View(clients);
        }

        // GET: Clients/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Street,Email,City,Zip")] Clients clients)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.ClientSet.Add(clients);
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
                    return View(clients);
                }

            }
            return View(clients);
        }

        // GET: Clients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clients clients = db.ClientSet.Find(id);
            if (clients == null)
            {
                return HttpNotFound();
            }
            return View(clients);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Street,Email,City,Zip")] Clients clients)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(clients).State = EntityState.Modified;
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
                    return View(clients);
                }
            }
            return View(clients);
        }

        // GET: Clients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clients clients = db.ClientSet.Find(id);
            if (clients == null)
            {
                return HttpNotFound();
            }
            return View(clients);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Clients clients = db.ClientSet.Find(id);
            db.ClientSet.Remove(clients);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Orders(int id)
        {
            ClientOrdersViewModel model = new ClientOrdersViewModel();
            model.client = db.ClientSet.Find(id);
            var clientOrders =
                (from client in db.ClientSet
                 join order in db.OrderSet on client.Id equals order.Client_id
                 where client.Id == id
                 select order).ToList().ToList();
            List<OrderPayment> orderPayment = new List<OrderPayment>();
            foreach (var ord in clientOrders)
            {
                OrderPayment orPay = new OrderPayment
                {
                    order = ord
                };
                var payQuery =
                    (from order in db.OrderSet
                     join payment in db.PaymentSet on order.Id equals payment.Ord_id
                     where order.Id == ord.Id
                     select payment).ToList().ToList();
                if (payQuery.Count > 0)
                    orPay.payment = payQuery[0];
                else orPay.payment = new Payments();
                orderPayment.Add(orPay);
            }
            model.orderPayment = orderPayment;
            return View(model);
        }

        public ActionResult OrderDetails(int id)
        {
            var query =
                (from order in db.OrderSet
                 join orderDetails in db.OrderDetailSet on order.Id equals orderDetails.Order_id
                 where order.Id == id
                 select orderDetails).ToList();
            return View(query.ToList());
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
