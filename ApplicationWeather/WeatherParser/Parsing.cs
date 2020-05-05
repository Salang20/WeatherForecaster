using AngleSharp;
using AngleSharp.Html.Dom;
using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherParser
{
    public static class Parsing
    {
       public static async System.Threading.Tasks.Task WeatherParsAsync()
        {
            //Create a new context for evaluating webpages with the given config
            var context = BrowsingContext.New(Configuration.Default.WithDefaultLoader());

            //Create a virtual request to specify the document to load (here from our fixed string)
            var document = await context.OpenAsync("https://gismeteo.ru");

            Console.WriteLine($"{document.Context}");
        }
    }
}
