using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestApi.Base;

namespace RestApi.Controllers
{
    [ApiController]
    [Route("api/WeatherForecast/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherService _weatherService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherService weatherService)
        {
            _logger = logger;
            _weatherService = weatherService;
        }

        [Route("GetAllCities")]
        [HttpGet]
        public List<string> GetAllCities()
        {
            return _weatherService.GetAllCities();   
        }
    }
}
