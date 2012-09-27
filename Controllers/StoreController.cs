using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Data;
using Core.Data.Entities;
using Core.Data.Repository;
using SumkaWeb.Models;
using Core.Data.Repository.Interfaces;

namespace SumkaWeb.Controllers
{
    [ValidateInput(false)]
    public class StoreController : Controller
    {
        private readonly IRepository<Store> StoreRepository;
        private readonly IRepository<WebTemplate> WebTemplateRepository;
        private readonly IRepository<Product> ProductsRepository;

        public StoreController()
        {
            StoreRepository = new Repository<Store>();
            WebTemplateRepository = new Repository<WebTemplate>();
            ProductsRepository = new Repository<Product>();
        }

        public ActionResult Index()
        {
            IList<Store> stores = StoreRepository.GetAll().ToList();
            return View(stores);
        }

        //
        // GET: /Storage/Details/5

        public ActionResult Details(int id)
        {
            Store store = StoreRepository.Get(s => s.Id.Equals(id)).SingleOrDefault();

            return View(store);
        }

        //
        // GET: /Storage/Create

        public ActionResult Create()
        {
            return View(new Store());
        }

        //
        // POST: /Storage/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                var name = collection["Name"];
                var htmlBanner = Server.HtmlEncode(collection["HtmlBanner"]);
                StoreRepository.SaveOrUpdate(new Store() { Name = name, HtmlBanner = htmlBanner });
                WebTemplateRepository.SaveOrUpdate(new WebTemplate() { Name = name + "_Store", Html = htmlBanner });
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
            Store store = StoreRepository.Get(s => s.Id.Equals(id)).SingleOrDefault();

            StoreEditModel storeEditModel = new StoreEditModel() { Store = store };

            return View(storeEditModel);
        }

        //
        // POST: /Storage/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                Store store = StoreRepository.Get(s => s.Id.Equals(id)).SingleOrDefault();
                store.Name = collection["Store.Name"];
                store.HtmlBanner = Server.HtmlEncode(collection["Store.HtmlBanner"]);// collection["Store.HtmlBanner"];

                StoreRepository.SaveOrUpdate(store);
                WebTemplateRepository.SaveOrUpdate(new WebTemplate() { Name = store.Name + "StoreWebTemplate", Html = store.HtmlBanner });
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
            return View(StoreRepository.Get(s => s.Id.Equals(id)).SingleOrDefault());
        }

        //
        // POST: /Storage/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                StoreRepository.Delete(StoreRepository.Get(s => s.Id.Equals(id)).SingleOrDefault());
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Storage/EditProduct/5

        public ActionResult EditProduct(int id)
        {
            return RedirectToAction("Edit", "Product", new { id = id });
        }

        //
        // GET: /Storage/AddProduct/5

        public ActionResult AddProduct(int id)
        {
            return RedirectToAction("Create", "Product", new { id = id });
        }
    }
}
