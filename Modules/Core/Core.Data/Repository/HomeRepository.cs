using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Data.Entities;
using Core.Data.Repository.Interfaces;

namespace Core.Data.Repository
{
    public class HomeRepository : IHomeRepository
    {
        public IList<Store> GetStores()
        {
            // create our NHibernate session factory
            var sessionFactory = NHibernateHelper.OpenSession();

            using (var session = sessionFactory)
            {
                // retreive all stores and display them
                using (session.BeginTransaction())
                {
                    return session.CreateCriteria<Store>().List<Store>();

                }
            }

        }
    }
}
