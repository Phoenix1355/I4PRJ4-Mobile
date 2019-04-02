using Prism;
using Prism.Ioc;
using Prism.Logging;

namespace i4prj.SmartCab.UnitTests.Mocks
{
    public class NUnitPlatformInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<ILoggerFacade, TraceLogger>();
        }
    }
}
