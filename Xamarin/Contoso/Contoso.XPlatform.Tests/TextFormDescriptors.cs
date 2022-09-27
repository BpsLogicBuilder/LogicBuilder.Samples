using Contoso.Forms.Configuration.TextForm;
using System.Collections.Generic;

namespace Contoso.XPlatform.Tests
{
    internal static class TextFormDescriptors
    {
        internal static TextFormSettingsDescriptor HomePage = new()
        {
             Title = "Home",
             TextGroups = new List<TextGroupDescriptor>
             {
                 new TextGroupDescriptor
                 {
                     Title = "Sub Heading",
                     Labels = new List<LabelItemDescriptorBase>
                     {
                         new LabelItemDescriptor
                         {
                             Text = "Description"
                         }
                     }
                 }
             }
        };
    }
}
