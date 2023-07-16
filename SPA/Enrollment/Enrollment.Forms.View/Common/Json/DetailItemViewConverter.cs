using Enrollment.Utils;

namespace Enrollment.Forms.View.Common.Json
{
    public class DetailItemViewConverter : JsonTypeConverter<DetailItemView>
    {
        public override string TypePropertyName => nameof(DetailItemView.TypeFullName);
    }
}
