using Contoso.Utils;
using Contoso.Web.Flow.ScreenSettings.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contoso.Web.Flow.ScreenSettings.Json
{
    public class DialogSettingsConverter : JsonTypeConverter<ScreenSettingsBase>
    {
        public override string TypePropertyName => "TypeFullName";

        protected override Type GetDerivedType(string typeName) 
            => typeof(ScreenSettingsBase).Assembly.GetType(typeName, true, false);
    }
}
