using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Data.Entities;
using NHibernate.Criterion;

namespace Core.Data.Repository
{
   public class ProductsRepository
    {

       public IList<Product> GetProducts()
        {
            // create our NHibernate session factory
            var sessionFactory = NHibernateHelper.OpenSession();

            using (var session = sessionFactory)
            {
                // retreive all products and display them
                using (session.BeginTransaction())
                {
                     return session.CreateCriteria<Product>().List<Product>(); ;
                }
            }
     
        }

       public Product GetProduct(int id)
        {
            // create our NHibernate session factory
            var sessionFactory = NHibernateHelper.OpenSession();

            using (var session = sessionFactory)
            {
                // retreive product by id and display them
                using (session.BeginTransaction())
                {
                    return session.CreateCriteria<Product>().Add(Restrictions.Eq("Id", id)).UniqueResult<Product>(); ;

                }
            }
          
        }

       public Product SaveStore(Product product)
        {
            // create our NHibernate session factory
            var sessionFactory = NHibernateHelper.OpenSession();

            using (var session = sessionFactory)
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.SaveOrUpdate(product);
                    transaction.Commit();
                }
            }
            return product;
        }

       public void DeleteStore(Product product)
        {
            // create our NHibernate session factory
            var sessionFactory = NHibernateHelper.OpenSession();

            using (var session = sessionFactory)
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Delete(product);
                    transaction.Commit();
                }
            }

        }
    }
}
