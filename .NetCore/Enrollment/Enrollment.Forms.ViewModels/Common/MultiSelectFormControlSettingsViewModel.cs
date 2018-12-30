using System.Collections.Generic;

namespace Enrollment.Forms.ViewModels.Common
{
    public class MultiSelectFormControlSettingsViewModel : FormControlSettingsViewModel
    {
		public override AbstractControlEnum AbstractControlType { get; set; }
		public List<string> KeyFields { get; set; }
		public MultiSelectTemplateViewModel MultiSelectTemplate { get; set; }
    }
}