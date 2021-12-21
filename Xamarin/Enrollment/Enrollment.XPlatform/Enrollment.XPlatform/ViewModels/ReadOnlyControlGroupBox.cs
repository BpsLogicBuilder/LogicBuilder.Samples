using Enrollment.Forms.Configuration;
using Enrollment.Forms.Configuration.DataForm;
using Enrollment.XPlatform.Utils;
using Enrollment.XPlatform.ViewModels.ReadOnlys;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Enrollment.XPlatform.ViewModels
{
    public class ReadOnlyControlGroupBox : ObservableCollection<IReadOnly>
    {
        public ReadOnlyControlGroupBox(IFormGroupBoxSettings groupBoxSettings) : base(new List<IReadOnly>())
        {
            GroupHeader = groupBoxSettings.GroupHeader;
            HeaderBindings = groupBoxSettings.HeaderBindings;
            GroupBoxSettings = groupBoxSettings;
            IsVisible = groupBoxSettings.IsHidden == false;
        }

        public string GroupHeader { get; set; }
        public bool IsVisible { get; set; }
        public MultiBindingDescriptor HeaderBindings { get; set; }
        public IFormGroupBoxSettings GroupBoxSettings { get; set; }
        public Dictionary<string, IReadOnly> BindingPropertiesDictionary
            => this.ToDictionary(p => p.Name.ToBindingDictionaryKey());
    }
}
