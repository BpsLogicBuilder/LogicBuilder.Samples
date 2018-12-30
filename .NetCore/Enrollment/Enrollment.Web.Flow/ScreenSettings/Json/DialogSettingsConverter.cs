using Enrollment.Utils;
using Enrollment.Web.Flow.ScreenSettings.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Enrollment.Web.Flow.ScreenSettings.Json
{
    public class DialogSettingsConverter : JsonTypeConverter<ScreenSettingsBase>
    {
        public override string TypePropertyName => "TypeFullName";

        protected override Type GetDerivedType(string typeName)
            => typeof(ScreenSettingsBase).Assembly.GetType(typeName, true, false);
    }
}
