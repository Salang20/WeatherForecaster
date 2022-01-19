using Common.DAL;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using RestApi.Base;
using SimpleInjector;

namespace RestApi
{
    public class Program
    {

        static readonly Container container;

        static Program()
        {
            container = new Container();
            container.Register<IDbExecutor, DbExecutor>();
            container.Register<IWeatherService, WeatherService>();
            container.Verify();
        }

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
