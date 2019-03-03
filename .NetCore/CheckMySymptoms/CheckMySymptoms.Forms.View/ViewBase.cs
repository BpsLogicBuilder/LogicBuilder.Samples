using CheckMySymptoms.Forms.View.Json;
using CheckMySymptoms.Utils;
using Newtonsoft.Json;

namespace CheckMySymptoms.Forms.View
{
    [JsonConverter(typeof(ViewConverter))]
    public abstract class ViewBase
    {
        public string TypeFullName { get { return this.GetType().ToTypeString(); } }

        public abstract void UpdateFields(object fields);
    }
}
