using System.Collections.Generic;

namespace Contoso.Forms.Configuration.TextForm
{
    public class TextGroupDescriptor
    {
        public string Title { get; set; }
        public List<LabelItemDescriptorBase> Labels { get; set; }
    }
}
