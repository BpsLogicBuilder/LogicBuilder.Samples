using Contoso.XPlatform.ViewModels.Validatables;
using System.Collections.Generic;

namespace Contoso.XPlatform.Validators.Rules
{
    public record MustBeNumberRule<T> : ValidationRuleBase<T>
    {
        public MustBeNumberRule(string fieldName, string validationMessage, ICollection<IValidatable> allProperties) 
            : base(fieldName, validationMessage, allProperties)
        {
        }

        public override string ClassName => nameof(MustBeNumberRule<T>);

        public override bool Check() 
            => Value == null
                || decimal.TryParse(Value.ToString(), out _)
                || double.TryParse(Value.ToString(), out _);
    }
}
