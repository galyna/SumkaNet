using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Data;
using Core.Data.Entities;
using Core.Data.Repository;

namespace SumkaWeb.Controllers
{
      [ValidateInput(false)]
    public class StoreController : Controller
    {
          private StoreRepository _storeRepository;

          public  StoreController()
          {
              _storeRepository = new StoreRepository();
          }

        public ActionResult Index()
        {
            IList<Store> stores = _storeRepository.GetStores();
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
                _storeRepository.SaveStore(new Store() { Name = collection["Name"], HtmlBanner = collection["HtmlBanner"] });

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
            Store store = _storeRepository.GetStore(id);
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
                store.Name=collection["Name"];
                store.HtmlBanner=collection["HtmlBanner"];

                _storeRepository.SaveStore(store);
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
