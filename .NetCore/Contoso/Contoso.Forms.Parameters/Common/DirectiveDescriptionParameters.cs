using LogicBuilder.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Contoso.Forms.Parameters.Common
{
    public class DirectiveDescriptionParameters
    {
        public DirectiveDescriptionParameters()
        {
        }

        public DirectiveDescriptionParameters
        (
            [Comments("Class name")]
            [NameValue(AttributeNames.DEFAULTVALUE, "Directives")]
            string className,

            [Comments("Function")]
            [NameValue(AttributeNames.DEFAULTVALUE, "hideIf")]
            [Domain("disableIf,hideIf,validateIf")]
            string functionName,

            [Comments("Where applicable, add arguments for the directive function.")]
            List<DirectiveArgumentParameters> arguments = null
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
