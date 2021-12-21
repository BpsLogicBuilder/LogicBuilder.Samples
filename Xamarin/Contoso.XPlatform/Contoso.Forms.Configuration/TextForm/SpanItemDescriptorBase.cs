using Contoso.Forms.Configuration.TextForm.Json;
using System.Text.Json.Serialization;

namespace Contoso.Forms.Configuration.TextForm
{
    [JsonConverter(typeof(SpanItemDescriptorConverter))]
    public abstract class SpanItemDescriptorBase
    {
        public string TypeString => this.GetType().AssemblyQualifiedName;
    }
}
