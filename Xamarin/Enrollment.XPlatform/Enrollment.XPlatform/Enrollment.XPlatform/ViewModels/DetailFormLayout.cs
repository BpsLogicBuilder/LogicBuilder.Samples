using Enrollment.Forms.Configuration.DataForm;
using Enrollment.XPlatform.ViewModels.ReadOnlys;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Enrollment.XPlatform.ViewModels
{
    public class DetailFormLayout
    {
        public DetailFormLayout()
        {
            ControlGroupBoxList = new List<ReadOnlyControlGroupBox>();
            Properties = new ObservableCollection<IReadOnly>();
        }

        public void Add(IReadOnly readOnly, IFormGroupBoxSettings groupBoxSettings)
        {
            Properties.Add(readOnly);

            if (!ControlGroupBoxList.Any())
                throw new InvalidOperationException("{40FA092B-D705-44B4-A1B8-151BD2FCD2CD}");

            ControlGroupBoxList
                .Single(g => object.ReferenceEquals(g.GroupBoxSettings, groupBoxSettings))
                .Add(readOnly);
        }

        public void AddControlGroupBox(IFormGroupBoxSettings groupBoxSettings) 
            => ControlGroupBoxList.Add(new ReadOnlyControlGroupBox(groupBoxSettings));

        public List<ReadOnlyControlGroupBox> ControlGroupBoxList { get; }

        public ObservableCollection<IReadOnly> Properties { get; }
    }
}
