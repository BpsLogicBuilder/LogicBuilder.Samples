using System.Collections.Generic;

namespace Enrollment.Forms.View.Common
{
    public class FormValidationSettingView
    {
        public object DefaultValue { get; set; }
        public List<ValidatorDescriptionView> Validators { get; set; }
    }
}