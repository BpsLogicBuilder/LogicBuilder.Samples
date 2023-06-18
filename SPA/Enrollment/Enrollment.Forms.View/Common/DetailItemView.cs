using Enrollment.Forms.View.Common.Json;
using System.Text.Json.Serialization;

namespace Enrollment.Forms.View.Common
{
    [JsonConverter(typeof(DetailItemViewConverter))]
    abstract public class DetailItemView
    {
        abstract public DetailItemEnum DetailType { get; set; }
        public string TypeFullName => this.GetType().AssemblyQualifiedName;
    }
}