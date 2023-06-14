using Contoso.Common.Configuration.Json;
using System.Text.Json.Serialization;

namespace Contoso.Common.Configuration.ExpressionDescriptors
{
    [JsonConverter(typeof(DescriptorConverter))]
    public abstract class OperatorDescriptorBase : IExpressionOperatorDescriptor
    {
        public string TypeString => this.GetType().AssemblyQualifiedName;
    }
}
