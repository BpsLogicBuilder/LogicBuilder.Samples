using CheckMySymptoms.Forms.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckMySymptoms.ViewModels
{
    public class SelectChoiceView
    {
        public string Caption { get; set; }
        public string Message { get; set; }
        public ObservableCollection<CommandButtonView> Options { get; set; }
    }
}
