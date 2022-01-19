using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi.Base
{
    public interface IWeatherService
    {
        List<string> GetAllCities();
    }
}
