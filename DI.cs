using System;
using SimpleInjector;

namespace Parser.DI
{
    public static class DI
    {
        static readonly Container container;

        container = new Container();
        container.Register<IParserSettings, ParserSettings>();
        
        container.Verify();
    }
}
