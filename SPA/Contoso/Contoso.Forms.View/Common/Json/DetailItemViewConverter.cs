using Contoso.Utils;

namespace Contoso.Forms.View.Common.Json
{
    public class DetailItemViewConverter : JsonTypeConverter<DetailItemView>
    {
        public override string TypePropertyName => nameof(DetailItemView.TypeFullName);
    }
}
