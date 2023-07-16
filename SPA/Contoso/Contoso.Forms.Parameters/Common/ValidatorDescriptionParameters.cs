using LogicBuilder.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Contoso.Forms.Parameters.Common
{
    public class ValidatorDescriptionParameters
    {
        public ValidatorDescriptionParameters()
        {
        }

        public ValidatorDescriptionParameters
        (
            [Comments("Class name")]
            [NameValue(AttributeNames.DEFAULTVALUE, "Validators")]
            string className,

            [Comments("Function")]
            [NameValue(AttributeNames.DEFAULTVALUE, "required")]
            string functionName,

            [Comments("Where applicable, add arguments for the validator function.")]
            List<ValidatorArgumentParameters> arguments = null
        )
        {
            ClassName = className;
            FunctionName = functionName;
            Arguments = arguments?.ToDictionary(kvp => kvp.Name, kvp => kvp.Value);
        }

        public string ClassName { get; set; }
        public string FunctionName { get; set; }
        public Dictionary<string, object> Arguments { get; set; }
    }
}
