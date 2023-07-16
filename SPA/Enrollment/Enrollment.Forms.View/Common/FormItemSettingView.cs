using Enrollment.Forms.View.Common.Json;
using System.Text.Json.Serialization;

namespace Enrollment.Forms.View.Common
{
    [JsonConverter(typeof(FormItemSettingViewConverter))]
    abstract public class FormItemSettingView
    {
        abstract public AbstractControlEnum AbstractControlType { get; set; }
        public string TypeFullName => this.GetType().AssemblyQualifiedName;
    }
}