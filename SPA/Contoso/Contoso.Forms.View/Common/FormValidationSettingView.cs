using System.Collections.Generic;

namespace Contoso.Forms.View.Common
{
    public class FormValidationSettingView
    {
		public object DefaultValue { get; set; }
		public List<ValidatorDescriptionView> Validators { get; set; }
    }
}