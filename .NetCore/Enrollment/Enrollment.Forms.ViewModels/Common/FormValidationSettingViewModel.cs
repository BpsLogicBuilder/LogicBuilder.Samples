using System.Collections.Generic;

namespace Enrollment.Forms.ViewModels.Common
{
    public class FormValidationSettingViewModel
    {
		public object DefaultValue { get; set; }
		public List<ValidatorDescriptionViewModel> Validators { get; set; }
    }
}