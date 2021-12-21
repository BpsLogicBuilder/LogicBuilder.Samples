using Contoso.XPlatform.ViewModels.Validatables;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Contoso.XPlatform.Validators.Rules
{
    public record IsValidPasswordRule : ValidationRuleBase<string>
    {
        public IsValidPasswordRule(string fieldName, string validationMessage, ICollection<IValidatable> allProperties)
            : base(fieldName, validationMessage, allProperties)
        {
        }

        private static readonly Regex RegexPassword = new Regex("(?=.*[A-Z])(?=.*\\d)(?=.*[¡!@#$%*¿?\\-_.\\(\\)])[A-Za-z\\d¡!@#$%*¿?\\-\\(\\)&]{8,20}");
        public override string ClassName { get => nameof(IsValidPasswordRule); }

        public override bool Check() => string.IsNullOrEmpty(Value) ? true : RegexPassword.IsMatch(Value ?? string.Empty);
    }
}
