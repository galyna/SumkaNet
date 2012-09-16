using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Data;
using Core.Data.Entities;

namespace SumkaWeb.Controllers
{
      [ValidateInput(false)]
    public class StoreController : Controller
    {
        //
        // GET: /Storage/

        public ActionResult Index()
        {
            var db = new NHibernateFiller();
           IList<Store> stores= db.GetStores();
           return View(stores);
        }

        //
        // GET: /Storage/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Storage/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Storage/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /Storage/Edit/5
 
        public ActionResult Edit(int id)
        {
            var db = new NHibernateFiller();
            Store store = db.GetStore(id);
          return View(store);
        }

        //
        // POST: /Storage/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                var db = new NHibernateFiller();
                Store store = db.GetStore(id);
                foreach (var key in collection.AllKeys)
                {
                    var value = collection[key];
                    if (key == "Name")
                    {
                        store.Name = value;
                    }
                    if (key == "HtmlBanner")
                    {
                        store.HtmlBanner = value;
                    }
                   
                }
                // TODO: Add update logic here
                db.SaveStore(store);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Storage/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Storage/Delete/5

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
