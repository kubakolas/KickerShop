using KickerShop.Models;
using MongoDB.Driver;

using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KickerShop.Controllers
{
    public class HomeController : Controller
    {
        private KickerShopEntities db = new KickerShopEntities();




        public ActionResult Index()
        {

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "All information about Customers and our Shop";

          

            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }


        public ActionResult Query1()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult Query1Answear(string date)
        {

            if (ModelState.IsValid)
            {
                string g = date;

                DateTime myDate = DateTime.ParseExact(date + "-01-01 00:00:00", "yyyy-MM-dd HH:mm:ss", null);

                var query1 =
                (from order in db.OrderSet
                 join orderDetails in db.OrderDetailSet on order.Id equals orderDetails.Order_id
                 join product in db.ProductSet on orderDetails.Product_id equals product.Id
                 where order.OrderDate >= myDate
                 select new { order.OrderDate, order.Id, orderDetails.OrderDetail_id, product.Name, orderDetails.Quantity }).ToList().ToList();



                List<Query1> temp = new List<Query1>();
                
                foreach(var a in query1)
                {
                    Query1 t = new Query1(a.OrderDate, a.Id, a.OrderDetail_id, a.Name, a.Quantity);
                    temp.Add(t);
                }


                return View(temp);

            }

            return HttpNotFound();
        }


        public ActionResult Query2()
        {
            var query2 = db.DiscountValue1();


            List<DiscountValue1_Result> temp = new List<DiscountValue1_Result>();

            foreach (var a in query2)
            {
                DiscountValue1_Result t = new DiscountValue1_Result();
                t.Distinction = a.Distinction;
                t.Procent = a.Procent;
                t.Total_Disconted_Value = a.Total_Disconted_Value;
                t.Total_value = a.Total_value;

                double? b = t.Procent; 
                temp.Add(t);
            }


            return View(temp);

        }


        public ActionResult Query3()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Query3Answear(string number)
        {


            if (ModelState.IsValid)
            {
                int n = Convert.ToInt32(number);

                var query3 = db.MostProfitableProducts(n);

                List<MostProfitableProducts_Result> temp = new List<MostProfitableProducts_Result>();

                foreach (var a in query3)
                {
                    MostProfitableProducts_Result t = new MostProfitableProducts_Result();
                    t.Product_id = a.Product_id;
                    t.Name = a.Name;
                    t.Total = a.Total;
                  
                    temp.Add(t);
                }


                return View(temp);
            }

            return HttpNotFound();
        }

        public ActionResult Query4()
        {

            var query4 = ( 
                from payments in db.PaymentSet
                where payments.Delivery_cost == 0.00
                select payments.Payment_id).Count();

            ViewBag.Count = query4;

            return View();
        }

        public ActionResult Query5()
        {
            var query5 =
                (from report in db.Report
                 select report);

            List<Report> temp = new List<Report>();

            foreach (var a in db.Report)
            {
                Report t = new Report();
                t.Jare = a.Jare;
                t.Order_Count = a.Order_Count;
                t.Total = a.Total;
                t.Avg_Per_Order = a.Avg_Per_Order;

                int? s = t.Jare;

                temp.Add(t);
            }

            return View(temp);
        }


        public ActionResult UserChoice()
        {
            ViewBag.Id = new SelectList(db.ClientSet, "Id", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserChoice([Bind(Include = "Id,Name,Street,Email,City,Zip")] Clients clients)
        {
            if (ModelState.IsValid)
            {
                KickerShop.Global.GlobalVariables.CurrentUserId = clients.Id;
                Clients cl = db.ClientSet.Find(clients.Id);
                KickerShop.Global.GlobalVariables.CurrentUserName = cl.Name;
            }
            ViewBag.Id = new SelectList(db.ClientSet, "Id", "Name");
            return View();
        }

        public ActionResult OrderTimeStats()
        {
            // MongoDb
            var mongoClient = new MongoClient("mongodb://kubakolas:Bazydanych12@kickershopcluster-shard-00-00-grsrl.mongodb.net:27017,kickershopcluster-shard-00-01-grsrl.mongodb.net:27017,kickershopcluster-shard-00-02-grsrl.mongodb.net:27017/test?ssl=true&replicaSet=KickerShopCluster-shard-0&authSource=admin&retryWrites=true");
            var database = mongoClient.GetDatabase("KickerUserTimes");
            var collection = database.GetCollection<OrderTime>("collection1").Find(_ => true).ToList();
            return View(collection);
        }

    }

   



}