using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Data.Entities;
using NHibernate.Criterion;

namespace Core.Data.Repository
{
    public class UserRepository
    {
        public IList<User> GetUsers()
        {
            // create our NHibernate session factory
            var sessionFactory = NHibernateHelper.OpenSession();

            using (var session = sessionFactory)
            {
                // retreive all stores and display them
                using (session.BeginTransaction())
                {
                    return session.CreateCriteria<User>().List<User>();
                }
            }          
        }

        public User GetUser(int id)
        {
            // create our NHibernate session factory
            var sessionFactory = NHibernateHelper.OpenSession();
         
            using (var session = sessionFactory)
            {
                // retreive all stores and display them
                using (session.BeginTransaction())
                {              
                    return session.CreateCriteria<Store>().Add(Restrictions.Eq("Id", id)).UniqueResult<User>();
                }
            }
           
        }

        public User SaveUser(User user)
        {
            // create our NHibernate session factory
            var sessionFactory = NHibernateHelper.OpenSession();

            using (var session = sessionFactory)
            {
                using (var transaction = session.BeginTransaction())
                {
                   session.SaveOrUpdate(user);
                    transaction.Commit();
                }
            }
            return user;
        }
       
        public User SaveUser(string userName, string email)
        {
            // create our NHibernate session factory
            var sessionFactory = NHibernateHelper.OpenSession();
            User user = new User() { UserName = userName, Email = email };
            using (var session = sessionFactory)
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.SaveOrUpdate(user);
                    transaction.Commit();
                }
            }
            return user;
        }

        public void DeleteUser(User user)
        {
            // create our NHibernate session factory
            var sessionFactory = NHibernateHelper.OpenSession();

            using (var session = sessionFactory)
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Delete(user);
                    transaction.Commit();
                }
            }
        }
    }
}
