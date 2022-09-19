using Contoso.XPlatform.Validators;

namespace Contoso.XPlatform.Directives
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
