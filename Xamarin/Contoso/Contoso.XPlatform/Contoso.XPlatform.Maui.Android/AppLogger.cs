using Android.Util;
using Contoso.XPlatform.Flow;

namespace Contoso.XPlatform.Droid
{
    public class AppLogger : IAppLogger
    {
        public void LogMessage(string group, string message)
        {
            Log.Debug($"X:{group}", message);
        }
    }
}