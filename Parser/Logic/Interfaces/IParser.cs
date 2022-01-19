using Parser.Models;
using System.Collections.Generic;

namespace Parser.Logic.Interfaces
{
    interface IParser
    {
        List<string> GetPopularCities();

        List<WeatherInfo> ParsWeatherInPopularCity(string url);

        void Executor();
    }
}
