using Contoso.Forms.Configuration.DataForm;
using Contoso.XPlatform.ViewModels.ReadOnlys;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Contoso.XPlatform.ViewModels
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
                throw new InvalidOperationException("{196C3BD2-23A7-4AB1-ACA0-62F627F904EB}");

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
