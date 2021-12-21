using Contoso.Forms.Configuration.TextForm.Json;
using System.Text.Json.Serialization;

namespace Contoso.Forms.Configuration.TextForm
{
    [JsonConverter(typeof(LabelItemDescriptorConverter))]
    public abstract class LabelItemDescriptorBase
    {
        public string TypeString => this.GetType().AssemblyQualifiedName;
    }
}
