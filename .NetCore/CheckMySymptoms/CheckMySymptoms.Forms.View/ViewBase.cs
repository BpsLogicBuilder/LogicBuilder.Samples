using CheckMySymptoms.Utils;

namespace CheckMySymptoms.Forms.View
{
    //[JsonConverter(typeof(ViewConverter))]
    public abstract class ViewBase
    {
        public string TypeFullName { get { return this.GetType().ToTypeString(); } }

        public abstract void UpdateFields(object fields);
    }
}
