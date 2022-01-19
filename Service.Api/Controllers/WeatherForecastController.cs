using Common.DAL;
using Common.DAL.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Api.Controllers
{
    [ApiController]
    [Route("api/WeatherForecast/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IDbExecutor _dbExecutor;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IDbExecutor dbExecutor)
        {
            _logger = logger;
            _dbExecutor = dbExecutor;
        }

        [Route("GetAllCities")]
        [HttpGet]
        public List<string> GetAllCities()
        {
            return _dbExecutor.SelectAll<Weather>().Select(x => x.CityName).Distinct().ToList();
        }

        [Route("GetForecastForTomorow/{cityName}")]
        [HttpGet]
        public List<Weather> GetForecastForTomorow(string cityName)
        {
            return _dbExecutor.SelectForCurrentDate(DateTime.Today.AddDays(1), cityName);
        }
    }
}
