using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Data.Entities;
using NHibernate.Criterion;

namespace Core.Data.Repository
{
    public class WebTemplateRepository
    {
        public IList<WebTemplate> GetWebTemplates()
        {
            // create our NHibernate session factory
            var sessionFactory = NHibernateHelper.OpenSession();

            using (var session = sessionFactory)
            {
                // retreive all webtemplates and display them
                using (session.BeginTransaction())
                {
                    return session.CreateCriteria<WebTemplate>().List<WebTemplate>();
                }
            }
           
        }

        public WebTemplate GetWebTemplate(int id)
        {
            // create our NHibernate session factory
            var sessionFactory = NHibernateHelper.OpenSession();

            using (var session = sessionFactory)
            {
                // retreive WebTemplate by id and display them
                using (session.BeginTransaction())
                {
                    return session.CreateCriteria<WebTemplate>().Add(Restrictions.Eq("Id", id)).UniqueResult<WebTemplate>();
                }
            }
       
        }

        public WebTemplate SaveWebTemplate(WebTemplate webTemplate)
        {
            // create our NHibernate session factory
            var sessionFactory = NHibernateHelper.OpenSession();

            using (var session = sessionFactory)
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.SaveOrUpdate(webTemplate);
                    transaction.Commit();
                }
            }
            return webTemplate;
        }

        public void DeleteWebTemplate(WebTemplate webTemplate)
        {
            // create our NHibernate session factory
            var sessionFactory = NHibernateHelper.OpenSession();

            using (var session = sessionFactory)
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Delete(webTemplate);
                    transaction.Commit();
                }
            }

        }
    }
}
