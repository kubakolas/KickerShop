using KickerShop.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
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
            ViewBag.Message = "In Kicker Shop You can find everything you need to play football.";
            return View();
        }

        public ActionResult Contact()
        {
            return View();
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