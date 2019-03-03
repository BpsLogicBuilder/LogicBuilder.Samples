using LogicBuilder.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMySymptoms.Forms.Parameters.Common
{
    public class FormValidationSettingParameters
    {
        public FormValidationSettingParameters()
        {
        }

        public FormValidationSettingParameters
        (
            [Comments("Default value for the form control.")]
            object defaultValue = null,

            [Comments("Confifuration for validation classes, functions (and arguments for the validator where necessary).")]
            List<ValidatorDescriptionParameters> validators = null
        )
        {
            DefaultValue = defaultValue;
            Validators = validators ?? new List<ValidatorDescriptionParameters>();
        }

        public object DefaultValue { get; set; }
        public List<ValidatorDescriptionParameters> Validators { get; set; }
    }
}
