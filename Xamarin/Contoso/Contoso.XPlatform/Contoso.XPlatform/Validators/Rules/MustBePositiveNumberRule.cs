using Contoso.XPlatform.ViewModels.Validatables;
using System.Collections.Generic;

namespace Contoso.XPlatform.Validators.Rules
{
    public record MustBePositiveNumberRule<T> : ValidationRuleBase<T>
    {
        public MustBePositiveNumberRule(string fieldName, string validationMessage, ICollection<IValidatable> allProperties)
            : base(fieldName, validationMessage, allProperties)
        {
        }

        public override string ClassName => nameof(MustBeNumberRule<T>);

        public override bool Check()
            => Value == null
                || (decimal.TryParse(Value.ToString(), out decimal decimalNumber) && decimalNumber > 0)
                || (double.TryParse(Value.ToString(), out double doubleNumber) && doubleNumber > 0);
    }
}
