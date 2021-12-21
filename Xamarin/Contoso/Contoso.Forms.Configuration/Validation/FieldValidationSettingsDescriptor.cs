using System.Collections.Generic;

namespace Contoso.Forms.Configuration.Validation
{
    public class FieldValidationSettingsDescriptor
    {
        public object DefaultValue { get; set; }
        public List<ValidatorDefinitionDescriptor> Validators { get; set; }
    }
}
