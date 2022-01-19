using Common.DAL.Entity;
using FluentNHibernate.Mapping;

namespace Common.DAL.Mappings
{
    public class WeatherMap : ClassMap<Weather>
    {

        public WeatherMap()
        {
            Schema("ForecastWeather");
            Table("WeatherData");
            CompositeId()
            .KeyProperty(x => x.ElementId)
            .KeyProperty(x => x.CityName)
            .KeyProperty(x => x.Date);
            Map(m => m.Hour);
            Map(m => m.Precipetation);
            Map(m => m.Sky);
            Map(m => m.Temperature);
            Map(m => m.WindSpeed);
        }
    }
}
