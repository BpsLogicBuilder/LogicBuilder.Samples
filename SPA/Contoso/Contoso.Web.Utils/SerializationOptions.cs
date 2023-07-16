using Contoso.Common.Configuration.Json;
using Contoso.Domain.Json;
using Contoso.Utils;
using System;
using System.Text.Json;

namespace Contoso.Web.Utils
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

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                options.Converters.Add(new DescriptorConverter());
                options.Converters.Add(new ModelConverter());
                options.Converters.Add(new ObjectConverter());

                _default = options;

                return _default;
            }
        }
    }
}
