using Enrollment.Forms.View.Common;
using Enrollment.Spa.Flow.ScreenSettings.Views;
using Enrollment.Utils;
using System;

namespace Enrollment.Spa.Flow.ScreenSettings.Json
{
    public class ScreenSettingsConverter : JsonTypeConverter<ScreenSettingsBase>
    {
        public override string TypePropertyName => nameof(ScreenSettingsBase.TypeFullName);
    }
}
