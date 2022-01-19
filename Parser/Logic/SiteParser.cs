using AngleSharp.Html.Dom;
using Common.DAL;
using Common.DAL.Entity;
using Parser.Interfaces;
using Parser.Logic.Interfaces;
using Parser.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Parser.Logic
{
    public class SiteParser : IParser
    {
        private static readonly string[] _days = { "tomorrow", "3-day", "4-day", "5-day", "6-day", "7-day", "8-day", "9-day", "10-day" };
        private static readonly string[] _hours = { "0:00", "3:00", "6:00", "9:00", "12:00", "15:00", "18:00", "21:00" };
        private readonly IParserSettings _parserSettings;
        private readonly IDbExecutor _dbExecutor;
        public SiteParser(IParserSettings parserSettings, IDbExecutor dbExecutor)
        {
            _parserSettings = parserSettings;
            _dbExecutor = dbExecutor;
        }

        public void Executor()
        {
            if (_dbExecutor.SelectAll<Weather>().Any())
            {
                _dbExecutor.DeleteAll("delete from Weather");
            }

            var citiesList = GetPopularCities();

            foreach (var city in citiesList)
            {
                var weatherList = ParsWeatherInPopularCity(city);


                foreach (var weather in weatherList)
                {
                    foreach (var element in weather.WeatherWithinDay)
                    {
                        var row = new Weather
                        {
                            ElementId = element.Key,
                            Date = element.Value.Date,
                            Sky = element.Value.Sky,
                            Temperature = element.Value.Temperature,
                            WindSpeed = element.Value.WindSpeed,
                            Precipetation = element.Value.Precipetation,
                            CityName = element.Value.CityName,
                            Hour = element.Value.Hour
                        };
                        _dbExecutor.Insert(row);
                    }
                }
            }
        }

        public List<string> GetPopularCities()
        {
            var document = _parserSettings.GetConfiguration("https://www.gismeteo.ru/").Result;
            var citiesInfo = document.All.Where(i => i.ClassName == "list-item").OfType<IHtmlDivElement>().Select(i => i.Children).ToList();
            var result = new List<string>();
            var a = citiesInfo.First();
            foreach (var cityInfo in citiesInfo)
            {

                result.Add(cityInfo.OfType<IHtmlAnchorElement>().First().Href);
            }

            return result;
        }

        private static DateTimeFormatInfo GetDateTimeFormat()
        {
            var ci = CultureInfo.CreateSpecificCulture("ru-RU");

            var dtf = ci.DateTimeFormat;
            dtf.AbbreviatedMonthNames = new string[] { "янв", "февр", "март",
                                                  "апр", "май", "июнь",
                                                  "июль", "авг", "сент",
                                                  "окт", "нояб", "дек", "" }; ;

            dtf.AbbreviatedMonthGenitiveNames = new string[] { "янв", "февр", "мар",
                                                  "апр", "мая", "июн",
                                                  "июл", "авг", "сент",
                                                  "окт", "нояб", "дек", "" };

            return dtf;
        }

        public List<WeatherInfo> ParsWeatherInPopularCity(string url)
        {
            IList<string> sky = null;
            IList<string> temperature = null;
            IList<string> windSpeed = null;
            IList<string> prec = null;
            var result = new List<WeatherInfo>();

            foreach (var day in _days)
            {
                var weatherByHours = new List<WeatherModel>(Enumerable.Range(0, 8).Select(r => new WeatherModel()));
                var document = _parserSettings.GetConfiguration(url + day).Result;
                sky = document.All.Where(i => i.ClassName == "weather-icon tooltip").OfType<IHtmlDivElement>().Select(i => i.Dataset["text"]).ToList();
                temperature = document.All.Where(i => i.ClassName == "unit unit_temperature_c").Skip(6).Select(i => i.TextContent).ToList();
                windSpeed = document.All.Where(i => i.ClassName == "wind-unit unit unit_wind_m_s").OfType<IHtmlSpanElement>().Take(8).Select(i => i.TextContent).ToList();
                prec = document.All.Where(i => i.ClassName == "item-unit unit-blue" | i.ClassName == "item-unit").OfType<IHtmlDivElement>().Select(i => i.TextContent).ToList();
                var cityName = document.All.Where(i => i.ClassName == "transparent-city js-transparent-city").OfType<IHtmlDivElement>().First().TextContent;
                var date = DateTime.Parse(document.All.FirstOrDefault(i => i.ClassName == "tab-content").Children.First().TextContent.Replace(",", ""), GetDateTimeFormat());


                for (int i = 0; i < 8; i++)
                {
                    weatherByHours[i].Date = date;
                    weatherByHours[i].Sky = sky[i];
                    weatherByHours[i].Temperature = temperature[i];
                    weatherByHours[i].WindSpeed = windSpeed[i].Trim();
                    weatherByHours[i].Precipetation = prec[i];
                    weatherByHours[i].CityName = cityName;
                    weatherByHours[i].Hour = _hours[i];
                    weatherByHours[i].ElementId = i;
                }

                result.Add(new WeatherInfo { WeatherWithinDay = new SortedDictionary<int, WeatherModel>(weatherByHours.ToDictionary(i => i.ElementId)) });
            }

            return result;
        }
    }
}