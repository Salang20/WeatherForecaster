using System;


namespace Common.DAL.Entity
{
    public class Weather
    {
        public virtual int ElementId { get; set; }

        public virtual DateTime Date { get; set; }

        public virtual string Sky { get; set; }

        public virtual string Temperature { get; set; }

        public virtual string WindSpeed { get; set; }

        public virtual string Precipetation { get; set; }

        public virtual string CityName { get; set; }

        public virtual string Hour { get; set; }

        public override bool Equals(object value)
        {
            var weather = value as Weather;

            return weather != null
                && ElementId == weather.ElementId
                && Date == weather.Date
                && Sky == weather.Sky
                && Temperature == weather.Temperature
                && WindSpeed == weather.WindSpeed
                && Precipetation == weather.Precipetation
                && CityName == weather.CityName
                && Hour == weather.Hour;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ElementId, Date, Sky, Temperature, WindSpeed,
                Precipetation, CityName, Hour);
        }
    }
}
