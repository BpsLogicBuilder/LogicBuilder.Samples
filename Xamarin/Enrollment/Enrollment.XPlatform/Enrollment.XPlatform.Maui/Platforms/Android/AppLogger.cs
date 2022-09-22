﻿using Android.Util;
using Enrollment.XPlatform.Flow;

namespace Enrollment.XPlatform
{
    public class AppLogger : IAppLogger
    {
        public void LogMessage(string group, string message)
        {
            Log.Debug($"X:{group}", message);
        }
    }
}
