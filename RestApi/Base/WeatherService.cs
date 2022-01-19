using Common.DAL;
using System.Collections.Generic;
using System.Linq;

namespace RestApi.Base
{
    public class WeatherService : IWeatherService
    {
        private readonly IDbExecutor _dbExecutor;
        public WeatherService(IDbExecutor dbExecutor)
        {
            _dbExecutor = dbExecutor;
        }

        public List<string> GetAllCities()
        {
            var repository = _dbExecutor.SelectAll<Common.DAL.Entity.Weather>();
            return repository.GroupBy(c => c.CityName).Select(c => c.Key).ToList();
        }
    }
}
