using Enrollment.XPlatform.Flow;
using System;

namespace Enrollment.XPlatform.Tests.Mocks
{
    public class AppLoggerMock : IAppLogger
    {
        public void LogMessage(string group, string message)
        {
            throw new NotImplementedException();
        }
    }
}
