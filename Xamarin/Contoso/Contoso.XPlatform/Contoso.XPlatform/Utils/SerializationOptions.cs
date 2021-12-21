using Contoso.Bsl.Business.Responses.Json;
using Contoso.Common.Configuration.Json;
using Contoso.Domain.Json;
using Contoso.Utils;
using System.Text.Json;

namespace Contoso.XPlatform.Utils
{
    public static class SerializationOptions
    {
        private static JsonSerializerOptions _default;
        public static JsonSerializerOptions Default
        {
            get
            {
                if (_default != null)
                    return _default;

                _default = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters =
                    {
                        new DescriptorConverter(),
                        new ModelConverter(),
                        new ObjectConverter(),
                        new ResponseConverter()
                    }
                };

                return _default;
            }
        }
    }
}
