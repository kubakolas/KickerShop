using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KickerShop.Models;

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
        public ActionResult Create([Bind(Exclude = "Id")] Client client)
        {
            ViewBag.Exception = null;
            string msg = null;
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


        // Reszta TO DO

        // POST: Client/Edit/5   
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic heres

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Client/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Client/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
