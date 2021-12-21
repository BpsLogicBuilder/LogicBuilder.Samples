using Contoso.XPlatform.Flow;

namespace Contoso.XPlatform.iOS
{
    public class AppLogger : IAppLogger
    {
        public void LogMessage(string group, string message)
        {
            System.Diagnostics.Debug.WriteLine($"{group}: {message}");
        }
    }
}