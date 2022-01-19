using Common.DAL.Entity;
using FluentNHibernate.Cfg;
using NHibernate;
using NHibernate.Cfg;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.DAL
{
    public class DbExecutor : IDbExecutor
    {
        public ISessionFactory SessionFactory { get; set; }
        public Configuration MyConfiguration { get; set; }
        public ISession Session { get; set; }

        private void SetConnect()
        {
            MyConfiguration = new Configuration().Configure();

            SessionFactory = Fluently.Configure(MyConfiguration).Mappings(m =>
                m.FluentMappings.AddFromAssemblyOf<Weather>()).BuildConfiguration()
            .BuildSessionFactory();
            Session = SessionFactory.OpenSession();
        }

        public void DeleteAll(string query)
        {
            SetConnect();
            using (Session.BeginTransaction())
            {
                Session.CreateQuery(query).ExecuteUpdate();
                Session.GetCurrentTransaction()?.Commit();
            }
        }

        public void Insert<T>(T entity)
        {
            SetConnect();

            using (Session.BeginTransaction())
            {
                Session.Save(entity);
                Session.GetCurrentTransaction()?.Commit();
            }
        }

        public List<T> SelectAll<T>()
        {
            SetConnect();

            return Session.Query<T>().ToList();
        }

        public List<Weather> SelectForCurrentDate(DateTime date, string cityName)
        {
            SetConnect();

            return Session.Query<Weather>().Where(i => i.Date == date && i.CityName == cityName).ToList();
        }
    }
}
