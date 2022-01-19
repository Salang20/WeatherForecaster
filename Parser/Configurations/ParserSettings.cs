using AngleSharp;
using AngleSharp.Dom;
using Parser.Interfaces;
using System.Threading.Tasks;

namespace Parser.Configurations
{
    public class ParserSettings : IParserSettings
    {
        public async Task<IDocument> GetConfiguration(string href)
        {
            var config = Configuration.Default.WithDefaultLoader();

            return await BrowsingContext.New(config).OpenAsync(href);
        }
    }
}
