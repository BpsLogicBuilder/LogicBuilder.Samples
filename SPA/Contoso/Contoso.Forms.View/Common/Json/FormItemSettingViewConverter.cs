using Contoso.Utils;

namespace Contoso.Forms.View.Common.Json
{
    public class FormItemSettingViewConverter : JsonTypeConverter<FormItemSettingView>
    {
        public override string TypePropertyName => nameof(FormItemSettingView.TypeFullName);
    }
}
