using Prism.Logging;
using System.Diagnostics;

namespace i4prj.SmartCab.UnitTests.Mocks
{
    public class TraceLogger : ILoggerFacade
    {
        public void Log(string message, Category category, Priority priority)
        {
            Trace.WriteLine($"{category} - {priority}: {message}");
        }
    }
}
