using Enrollment.XPlatform.Flow;

namespace Enrollment.XPlatform.iOS
{
    public class AppLogger : IAppLogger
    {
        public void LogMessage(string group, string message)
        {
            System.Diagnostics.Debug.WriteLine($"{group}: {message}");
        }
    }
}