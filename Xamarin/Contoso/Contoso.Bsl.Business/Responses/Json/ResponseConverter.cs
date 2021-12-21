using Contoso.Utils;

namespace Contoso.Bsl.Business.Responses.Json
{
    public class ResponseConverter : JsonTypeConverter<BaseResponse>
    {
        public override string TypePropertyName => "TypeFullName";
    }
}
