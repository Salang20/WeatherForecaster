using System;

namespace Parser.Models
{
    public class WeatherModel
    {
        public int ElementId { get; set; }

        public DateTime Date { get; set; }

        public string Sky { get; set; }

        public string Temperature { get; set; }

        public string WindSpeed { get; set; }

        public string Precipetation { get; set; }

        public string CityName { get; set; }

        public string Hour { get; set; }
    }
}
