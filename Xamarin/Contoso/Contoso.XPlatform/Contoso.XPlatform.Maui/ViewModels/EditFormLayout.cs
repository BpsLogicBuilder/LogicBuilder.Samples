using Contoso.Forms.Configuration.DataForm;
using Contoso.XPlatform.ViewModels.Validatables;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Contoso.XPlatform.ViewModels
{
    public class EditFormLayout
    {
        public EditFormLayout()
        {
            ControlGroupBoxList = new List<ControlGroupBox>();
            Properties = new ObservableCollection<IValidatable>();
        }

        public void Add(IValidatable validatable, IFormGroupBoxSettings groupBoxSettings)
        {
            Properties.Add(validatable);

            if (!ControlGroupBoxList.Any())
                throw new InvalidOperationException("{AA443C6C-7007-498B-9404-54D87CE1278C}");

            ControlGroupBoxList
                .Single(g => object.ReferenceEquals(g.GroupBoxSettings, groupBoxSettings))
                .Add(validatable);
        }

        public void AddControlGroupBox(IFormGroupBoxSettings groupBoxSettings)
            => ControlGroupBoxList.Add(new ControlGroupBox(groupBoxSettings));

        public List<ControlGroupBox> ControlGroupBoxList { get; }

        public ObservableCollection<IValidatable> Properties { get; }
    }
}
