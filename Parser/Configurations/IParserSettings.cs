using AngleSharp.Dom;
using System.Threading.Tasks;

namespace Parser.Interfaces
{
    public interface IParserSettings
    {
        Task<IDocument> GetConfiguration(string href);
    }
}
