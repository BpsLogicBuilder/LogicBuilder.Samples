using Contoso.Forms.Configuration.Directives;
using System;
using System.Linq.Expressions;

namespace Contoso.XPlatform.Validators
{
    public class ValidateIf<T> : ConditionBase<T>
    {
        public ValidateIf()
        {
            /*Properties will be created through inline initialization*/
            Validator = null!;
        }

        public IValidationRule Validator { get; set; }
    }
}
