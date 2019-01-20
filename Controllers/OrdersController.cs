using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KickerShop.Models;
using System.Transactions;
using System.Data.Common;
using MongoDB.Driver;

namespace KickerShop.Controllers
{
    public class OrdersController : Controller
    {
        static Dictionary<int, DateTime> orderTime = new Dictionary<int, DateTime>();
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
            if(Global.GlobalVariables.CurrentUserId < 0)
            {
                return RedirectToAction("UserChoice", "Home");
            }
            ViewBag.Client_id = new SelectList(db.ClientSet, "Id", "Name");
            ViewBag.DeliveryType_id = new SelectList(db.Delivery_typeSet, "Id", "Name");
            ViewBag.PayType_id = new SelectList(db.Payment_typeSet, "Id", "Name");
            orderTime[Global.GlobalVariables.CurrentUserId] = DateTime.Now;
            return View();
        }

        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,OrderDate,Client_id,DeliveryType_id,PayType_id")] Orders order,
                                    List<OrderDetails> details)
        {
            ViewBag.Client_id = new SelectList(db.ClientSet, "Id", "Name", order.Client_id);
            ViewBag.DeliveryType_id = new SelectList(db.Delivery_typeSet, "Id", "Name", order.DeliveryType_id);
            ViewBag.PayType_id = new SelectList(db.Payment_typeSet, "Id", "Name", order.PayType_id);


            if (ModelState.IsValid)
            {
                try
                {
                    Clients client = db.ClientSet.Find(KickerShop.Global.GlobalVariables.CurrentUserId);
                    order.Client_id = client.Id;
                    order.OrderDate = DateTime.Now;
                    db.OrderSet.Add(order);
                    foreach (var detail in details)
                    {
                        detail.Order_id = order.Id;
                        db.OrderDetailSet.Add(detail);
                    }
                    db.SaveChanges();
                    db.InsertPayment(order.Id);
                    db.SaveChanges();

                    // MongoDb
                    var ordTime = DateTime.Now - orderTime[client.Id];
                    var mongoClient = new MongoClient("mongodb://kubakolas:Bazydanych12@kickershopcluster-shard-00-00-grsrl.mongodb.net:27017,kickershopcluster-shard-00-01-grsrl.mongodb.net:27017,kickershopcluster-shard-00-02-grsrl.mongodb.net:27017/test?ssl=true&replicaSet=KickerShopCluster-shard-0&authSource=admin&retryWrites=true");
                    var database = mongoClient.GetDatabase("KickerUserTimes");
                    var collection = database.GetCollection<OrderTime>("collection1");
                    OrderTime ot = new OrderTime
                    {
                        time = ordTime,
                        clientName = client.Name,
                        orderId = order.Id
                    };
                    collection.InsertOne(ot);
                    return RedirectToAction("../Clients/Orders/" + client.Id.ToString());
                }
                catch (Exception e)
                {
                    string msg = null;
                    if (e.InnerException == null)
                        msg = "Invalid order data";
                    else
                        msg = e.InnerException.Message;
                    ViewBag.Error = msg;
                    return View(order);
                }
            }
            return RedirectToAction("Create");
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
            ViewBag.Product_id = new SelectList(db.ProductSet, "Id", "Name");
            return View(orders);
        }

        // POST: Orders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,OrderDate,Client_id,DeliveryType_id,PayType_id")] Orders orders,
                                 List<OrderDetails> details)
        {
            ViewBag.Client_id = new SelectList(db.ClientSet, "Id", "Name", orders.Client_id);
            ViewBag.DeliveryType_id = new SelectList(db.Delivery_typeSet, "Id", "Name", orders.DeliveryType_id);
            ViewBag.PayType_id = new SelectList(db.Payment_typeSet, "Id", "Name", orders.PayType_id);
            ViewBag.Product_id = new SelectList(db.ProductSet, "Id", "Name");

            if (ModelState.IsValid)
            {
                try
                {
                    Orders ord = db.OrderSet.FirstOrDefault(o => o.Id == orders.Id);
                    ord.Client_id = orders.Client_id;
                    ord.DeliveryType_id = orders.DeliveryType_id;
                    ord.PayType_id = orders.PayType_id;
                    db.SaveChanges();
                    if (details != null)
                        foreach (var detail in details)
                        {
                            OrderDetails det = db.OrderDetailSet.FirstOrDefault(o => o.OrderDetail_id == detail.OrderDetail_id);
                            det.Product_id = detail.Product_id;
                            det.Quantity = detail.Quantity;
                            db.SaveChanges();
                        }
                    return View(ord);
                }
                catch (Exception e)
                {
                    string msg = null;
                    if (e.InnerException == null)
                        msg = "Invalid order data";
                    else
                        msg = e.InnerException.InnerException.Message;
                    ViewBag.Error = msg;
                    return View(orders);
                }
            }
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
            db.OrderDetailSet.RemoveRange(orders.OrderDetails);
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
