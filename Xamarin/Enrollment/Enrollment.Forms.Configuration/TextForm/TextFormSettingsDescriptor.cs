using System.Collections.Generic;

namespace Enrollment.Forms.Configuration.TextForm
{
    public class TextFormSettingsDescriptor
    {
        public string Title { get; set; }
        public List<TextGroupDescriptor> TextGroups { get; set; }
    }
}
