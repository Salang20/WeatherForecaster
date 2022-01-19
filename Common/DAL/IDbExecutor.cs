using Common.DAL.Entity;
using System;
using System.Collections.Generic;

namespace Common.DAL
{
    public interface IDbExecutor
    {
        void Insert<T>(T entity);

        List<T> SelectAll<T>();

        void DeleteAll(string query);

        public List<Weather> SelectForCurrentDate(DateTime date, string cityName);


    }
}
