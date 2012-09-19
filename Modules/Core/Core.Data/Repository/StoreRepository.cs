using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Data.Entities;
using NHibernate.Criterion;

namespace Core.Data.Repository
{
    public class StoreRepository
    {

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

        public Store SaveStore(Store store)
        {
            // create our NHibernate session factory
            var sessionFactory = NHibernateHelper.OpenSession();

            using (var session = sessionFactory)
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.SaveOrUpdate(store);
                    transaction.Commit();
                }
            }
            return store;
        }

        public void DeleteStore(Store store)
        {
            // create our NHibernate session factory
            var sessionFactory = NHibernateHelper.OpenSession();

            using (var session = sessionFactory)
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Delete(store);
                    transaction.Commit();
                }
            }

        }

    }
}
