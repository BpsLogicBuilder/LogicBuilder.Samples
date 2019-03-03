using System.Collections.Generic;

namespace CheckMySymptoms.Forms.View.Common
{
    public class FormValidationSettingView
    {
		public object DefaultValue { get; set; }
		public List<ValidatorDescriptionView> Validators { get; set; }
    }
}