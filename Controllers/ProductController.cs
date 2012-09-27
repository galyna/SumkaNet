using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Data.Entities;
using SumkaWeb.Models;
using Core.Data.Repository.Interfaces;
using Core.Data.Repository;

namespace SumkaWeb.Controllers
{
    [ValidateInput(false)]
    public class ProductController : Controller
    {
        private readonly IRepository<Store> StoreRepository;
        private readonly IRepository<WebTemplate> WebTemplateRepository;
        private readonly IRepository<Product> ProductsRepository;

        public ProductController()
        {
            StoreRepository = new Repository<Store>();
            WebTemplateRepository = new Repository<WebTemplate>();
            ProductsRepository = new Repository<Product>();
        }

        public ActionResult Index()
        {
            IList<Product> products = ProductsRepository.GetAll().ToList();
            return View(products);
        }

        //
        // GET: /Product/Details/5

        public ActionResult Details(int id)
        {
            Product store = ProductsRepository.Get(s => s.Id.Equals(id)).SingleOrDefault();

            return View(store);
        }



        //
        // GET: /Product/Create/5

        public ActionResult Create(int id)
        {
            ProductCreateModel productCreateModel = new ProductCreateModel()
            {
                Product = new Product(),
                ProductTemplates = WebTemplateRepository.GetAll().ToList(),
                StoreID = id
            };

            return View(productCreateModel);
        }

        //k
        // POST: /Product/Create

        [HttpPost]
        public ActionResult Create(ProductCreateModel model)
        {
            try
            {
                var name = model.Product.Name;
                var htmlBanner = Server.HtmlEncode(model.Product.HtmlBanner);

                Store store = StoreRepository.Get(s => s.Id.Equals(model.StoreID)).SingleOrDefault();
                store.AddProduct( new Product() { Name = name, HtmlBanner = htmlBanner });
                StoreRepository.SaveOrUpdate(store);
                WebTemplateRepository.SaveOrUpdate(new WebTemplate() { Name = name + "ProductWebTemplate", Html = htmlBanner });
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Product/Edit/5

        public ActionResult Edit(int id)
        {
            Product product = ProductsRepository.Get(s => s.Id.Equals(id)).SingleOrDefault();
            IList<WebTemplate> productTemplates = WebTemplateRepository.GetAll().ToList();

            ProductEditModel productEditModel = new ProductEditModel() { Product = product, ProductTemplates = productTemplates };

            return View(productEditModel);
        }

        //
        // POST: /Product/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                Product product = ProductsRepository.Get(s => s.Id.Equals(id)).SingleOrDefault();
                product.Name = collection["Product.Name"];
                product.HtmlBanner = Server.HtmlEncode(collection["Product.HtmlBanner"]);

                ProductsRepository.SaveOrUpdate(product);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        // Adds any products that we pass in to the store that we pass in
        public static void AddProductsToStore(Store store, params Product[] products)
        {
            foreach (var product in products)
            {
                store.AddProduct(product);
            }
        }

    }
}

