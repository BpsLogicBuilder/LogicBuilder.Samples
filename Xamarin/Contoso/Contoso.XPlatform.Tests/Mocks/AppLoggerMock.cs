using Contoso.XPlatform.Flow;
using System;

namespace Contoso.XPlatform.Tests.Mocks
{
    public class AppLoggerMock : IAppLogger
    {
        public void LogMessage(string group, string message)
        {
            throw new NotImplementedException();
        }
    }
}
