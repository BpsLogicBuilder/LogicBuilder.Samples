using Enrollment.Utils;

namespace Enrollment.Bsl.Business.Responses.Json
{
    public class ResponseConverter : JsonTypeConverter<BaseResponse>
    {
        public override string TypePropertyName => "TypeFullName";
    }
}
