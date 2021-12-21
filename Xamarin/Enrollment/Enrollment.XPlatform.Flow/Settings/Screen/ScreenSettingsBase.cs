using Enrollment.Forms.Configuration;
using System.Collections.Generic;

namespace Enrollment.XPlatform.Flow.Settings.Screen
{
    public abstract class ScreenSettingsBase
    {
        abstract public ViewType ViewType { get; }
        public IEnumerable<CommandButtonDescriptor> CommandButtons { get; set; }
    }
}
