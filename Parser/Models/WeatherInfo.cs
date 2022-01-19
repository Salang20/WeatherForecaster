using System.Collections.Generic;

namespace Parser.Models
{
    public class WeatherInfo
    {
        public SortedDictionary<int, WeatherModel> WeatherWithinDay { get; set; }
    }
}
