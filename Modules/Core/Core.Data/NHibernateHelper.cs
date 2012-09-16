using System;
using System.IO;
using Core.Data.Entities;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using Core.Data;
using System.Reflection;
using System.Collections.Generic;
using NHibernate.Criterion;

namespace Core.Data
{
    public class NHibernateHelper
    {

        private static ISessionFactory _sessionFactory;
        private static ISessionFactory SessionFactory
        {
            get
            {

                if (_sessionFactory == null)

                    InitializeSessionFactory();


                return _sessionFactory;

            }

        }

        private static void InitializeSessionFactory()
        {

            _sessionFactory = Fluently.Configure()
                  .Database(MsSqlConfiguration.MsSql2008
                  .ConnectionString(
                                 @"Data Source=BEST\SQLEXPRESS;Initial Catalog=testdb;Integrated Security=True")
                         .ShowSql()
                         )

                  .Mappings(m =>
                      m.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()).ExportTo(@"C:\"))

                  .ExposeConfiguration(cfg => new SchemaExport(cfg).Create(true, true))
                  .BuildSessionFactory();
          

        }
    
        public static ISession OpenSession()
        {

            return SessionFactory.OpenSession();

        }

    }


    public class NHibernateFiller
    {


        public void FillDB()
        {
            // create our NHibernate session factory
            var sessionFactory = NHibernateHelper.OpenSession();

            using (var session = sessionFactory)
            {
                // populate the database
                using (var transaction = session.BeginTransaction())
                {
                    // create a couple of Stores each with some Products and Employees
                    var barginBasin = new Store { Name = "Bargin Basin" ,HtmlBanner=""};
                    var superMart = new Store { Name = "SuperMart",HtmlBanner="" };

                    var potatoes = new Product { Name = "Potatoes", Price = 3.60 };
                    var fish = new Product { Name = "Fish", Price = 4.49 };
                    var milk = new Product { Name = "Milk", Price = 0.79 };
                    var bread = new Product { Name = "Bread", Price = 1.29 };
                    var cheese = new Product { Name = "Cheese", Price = 2.10 };
                    var waffles = new Product { Name = "Waffles", Price = 2.41 };

                    var daisy = new Employee { FirstName = "Daisy", LastName = "Harrison" };
                    var jack = new Employee { FirstName = "Jack", LastName = "Torrance" };
                    var sue = new Employee { FirstName = "Sue", LastName = "Walkters" };
                    var bill = new Employee { FirstName = "Bill", LastName = "Taft" };
                    var joan = new Employee { FirstName = "Joan", LastName = "Pope" };

                    // add products to the stores, there's some crossover in the products in each
                    // store, because the store-product relationship is many-to-many
                    AddProductsToStore(barginBasin, potatoes, fish, milk, bread, cheese);
                    AddProductsToStore(superMart, bread, cheese, waffles);

                    // add employees to the stores, this relationship is a one-to-many, so one
                    // employee can only work at one store at a time
                    AddEmployeesToStore(barginBasin, daisy, jack, sue);
                    AddEmployeesToStore(superMart, bill, joan);

                    // save both stores, this saves everything else via cascading
                    session.SaveOrUpdate(barginBasin);
                    session.SaveOrUpdate(superMart);

                    transaction.Commit();
                }
            }
            sessionFactory = NHibernateHelper.OpenSession();
            using (var session = sessionFactory)
            {
                // retreive all stores and display them
                using (session.BeginTransaction())
                {
                    var stores = session.CreateCriteria(typeof(Store))
                        .List<Store>();

                    foreach (var store in stores)
                    {
                        WriteStorePretty(store);
                    }
                }
            }


        }


        public IList<Store> GetStores()
        {
            // create our NHibernate session factory
            var sessionFactory = NHibernateHelper.OpenSession();
            IList<Store> storesList = new List<Store>();
            using (var session = sessionFactory)
            {
                // retreive all stores and display them
                using (session.BeginTransaction())
                {
                    var stores = session.CreateCriteria<Store>().List<Store>();
                        
                    storesList = stores;
                   
                }
            }
            return storesList;
        }

        public IList<WebTemplate> GetWebTemplates()
        {
            // create our NHibernate session factory
            var sessionFactory = NHibernateHelper.OpenSession();
            IList<WebTemplate> storesList = new List<WebTemplate>();
            using (var session = sessionFactory)
            {
                // retreive all stores and display them
                using (session.BeginTransaction())
                {
                    var stores = session.CreateCriteria<WebTemplate>().List<WebTemplate>();

                    storesList = stores;

                }
            }
            return storesList;
        }

        public Store GetStore(int id)
        {
            // create our NHibernate session factory
            var sessionFactory = NHibernateHelper.OpenSession();
           
            using (var session = sessionFactory)
            {
                // retreive all stores and display them
                using (session.BeginTransaction())
                {
                    var store = session.CreateCriteria<Store>().Add(Restrictions.Eq("Id", id)).UniqueResult<Store>();
                    return store;

                }
            }
            return null;
        }
        public WebTemplate GetWebTemplate(int id)
        {
            // create our NHibernate session factory
            var sessionFactory = NHibernateHelper.OpenSession();

            using (var session = sessionFactory)
            {
                // retreive all stores and display them
                using (session.BeginTransaction())
                {
                    var store = session.CreateCriteria<WebTemplate>().Add(Restrictions.Eq("Id", id)).UniqueResult<WebTemplate>();
                    return store;

                }
            }
            return null;
        }
        public Store SaveStore(Store store)
        {
            // create our NHibernate session factory
            var sessionFactory = NHibernateHelper.OpenSession();

            using (var session = sessionFactory)
            {
                using (var transaction = session.BeginTransaction())
                {

                    var newStore = store;
                    
                   
                    session.SaveOrUpdate(store);
                   

                    transaction.Commit();
                }
            }
            return store;
        }
        private static void WriteStorePretty(Store store)
        {
            Console.WriteLine(store.Name);
            Console.WriteLine("  Products:");

            foreach (var product in store.Products)
            {
                Console.WriteLine("    " + product.Name);
            }

            Console.WriteLine("  Staff:");

            foreach (var employee in store.Staff)
            {
                Console.WriteLine("    " + employee.FirstName + " " + employee.LastName);
            }

            Console.WriteLine();
        }

        public static void AddProductsToStore(Store store, params Product[] products)
        {
            foreach (var product in products)
            {
                store.AddProduct(product);
            }
        }

        public static void AddEmployeesToStore(Store store, params Employee[] employees)
        {
            foreach (var employee in employees)
            {
                store.AddEmployee(employee);
            }
        }
    }

}

