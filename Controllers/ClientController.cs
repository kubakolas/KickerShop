using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KickerShop.Models;
using System.Data.Entity;

namespace KickerShop.Controllers
{
    public class ClientController : Controller
    {

        private KickerShopEntities db = new KickerShopEntities();

        // GET: Client
        public ActionResult Index()
        {
            return View(db.ClientSet.ToList());
        }

        // GET: Client/Details/5
        public ActionResult Details(int id)
        {
            return View(db.ClientSet.Find(id));
        }

        // GET: Clinet/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Client/Create
        [HttpPost]
        public ActionResult Create(Client client)
        {
            if (ModelState.IsValid)
            {
                // LINQ
                db.ClientSet.Add(client);
                try
                {
                    db.SaveChanges();						
                }
                catch (Exception e)
                {
                    string msg = null;
                    if (e.InnerException == null)
                        msg = "Niepoprawne dane klienta";
                    else
                        msg = e.InnerException.InnerException.Message;
                    ViewBag.Exception = msg; 
                    return View(client);
                }
                return RedirectToAction("Index");
            }
            return View(client);
        }

        // GET: Client/Edit/5
        public ActionResult Edit(int id)
        {
            return View(db.ClientSet.Find(id));
        }

        // POST: Client/Edit/5   
        [HttpPost]
        public ActionResult Edit(Client client)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.ClientSet.Attach(client);
                    db.Entry(client).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch // dorobic obsluge bledow
                {
                    return View();
                }
            }
            return View(client);
        }

        // GET: Client/Delete/5
        public ActionResult Delete(int id)
        {
            return View(db.ClientSet.Find(id));
        }

        // POST: Client/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                View(db.ClientSet.Remove(db.ClientSet.Find(id)));
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch // dorobic obsluge bledow
            {
                return View();
            }
        }

        public ActionResult Orders(int id)
        {
            var query =
               (from client in db.ClientSet
                join order in db.OrderSet on client.Id equals order.Id
                where client.Id == id
                select order).ToList();
            return View(query.ToList());
        }
    }
}
