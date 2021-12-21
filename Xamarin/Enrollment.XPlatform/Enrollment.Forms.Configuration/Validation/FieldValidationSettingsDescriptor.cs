using System.Collections.Generic;

namespace Enrollment.Forms.Configuration.Validation
{
    public class FieldValidationSettingsDescriptor
    {
        public object DefaultValue { get; set; }
        public List<ValidatorDefinitionDescriptor> Validators { get; set; }
    }
}
