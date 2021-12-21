using Contoso.Bsl.Business.Responses.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Contoso.Bsl.Business.Responses
{
    [JsonConverter(typeof(ResponseConverter))]
    public abstract class BaseResponse
    {
        public bool Success { get; set; }
        public ICollection<string> ErrorMessages { get; set; }
        public string TypeFullName => this.GetType().AssemblyQualifiedName;
    }
}
