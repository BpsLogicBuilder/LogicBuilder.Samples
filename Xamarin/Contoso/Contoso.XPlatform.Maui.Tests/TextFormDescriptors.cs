using Contoso.Forms.Configuration.TextForm;
using System.Collections.Generic;

namespace Contoso.XPlatform.Maui.Tests
{
    internal static class TextFormDescriptors
    {
        internal static TextFormSettingsDescriptor HomePage = new()
        {
             Title = "Home",
             TextGroups =
             [
                 new TextGroupDescriptor
                 {
                     Title = "Sub Heading",
                     Labels =
                     [
                         new LabelItemDescriptor
                         {
                             Text = "Description"
                         }
                     ]
                 }
             ]
        };
    }
}
