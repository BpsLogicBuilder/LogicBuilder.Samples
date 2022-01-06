using Contoso.Bsl.Business.Responses.Json;
using LogicBuilder.Attributes;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Contoso.Bsl.Business.Responses
{
    [JsonConverter(typeof(ResponseConverter))]
    public abstract class BaseResponse
    {
        [AlsoKnownAs("Response_Success")]
        public bool Success { get; set; }
        [AlsoKnownAs("Response_ErrorMessages")]
        public ICollection<string> ErrorMessages { get; set; }
        public string TypeFullName => this.GetType().AssemblyQualifiedName;
    }
}
