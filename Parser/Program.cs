using Common.DAL;
using Parser.Configurations;
using Parser.Interfaces;
using Parser.Logic;
using SimpleInjector;

namespace Parser
{
    static class Program
    {
        static readonly Container container;

        static Program()
        {
            container = new Container();
            container.Register<IParserSettings, ParserSettings>();
            container.Register<IDbExecutor, DbExecutor>();
            container.Register<SiteParser>();

            container.Verify();
        }
        public static void Main(string[] args)
        {
            var handler = container.GetInstance<SiteParser>();
            handler.Executor();
        }
    }
}