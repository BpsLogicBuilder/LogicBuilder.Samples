using Enrollment.Forms.Configuration;
using System.Collections.Generic;

namespace Enrollment.XPlatform.Flow.Settings.Screen
{
    public class ScreenSettings<TFormDescriptor> : ScreenSettingsBase
    {
        public ScreenSettings(TFormDescriptor settings, IEnumerable<CommandButtonDescriptor> commandButtons, ViewType viewType)
        {
            Settings = settings;
            CommandButtons = commandButtons;
            this.ViewType = viewType;
        }

        public override ViewType ViewType { get; }
        public TFormDescriptor Settings { get; set; }
    }
}
