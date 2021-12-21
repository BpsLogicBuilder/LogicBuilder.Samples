using Android.Util;
using Enrollment.XPlatform.Flow;

namespace Enrollment.XPlatform.Droid
{
    public class AppLogger : IAppLogger
    {
        public void LogMessage(string group, string message)
        {
            Log.Debug($"X:{group}", message);
        }
    }
}