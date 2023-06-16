using Contoso.Forms.View.Common;
using Contoso.Spa.Flow.ScreenSettings.Views;
using Contoso.Utils;
using System;

namespace Contoso.Spa.Flow.ScreenSettings.Json
{
    public class ScreenSettingsConverter : JsonTypeConverter<ScreenSettingsBase>
    {
        public override string TypePropertyName => nameof(ScreenSettingsBase.TypeFullName);
    }
}
