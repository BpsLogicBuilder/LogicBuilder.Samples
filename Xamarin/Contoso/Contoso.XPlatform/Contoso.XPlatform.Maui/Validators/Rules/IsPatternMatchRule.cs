using Contoso.XPlatform.ViewModels.Validatables;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Contoso.XPlatform.Validators.Rules
{
    public record IsPatternMatchRule : ValidationRuleBase<string>
    {
        public IsPatternMatchRule(string fieldName, string validationMessage, ICollection<IValidatable> allProperties, string pattern) : base(fieldName, validationMessage, allProperties)
        {
            if (string.IsNullOrEmpty(pattern))
                throw new ArgumentException($"{pattern}: 9455AB00-2527-4543-95F3-805F478C06DE");

            Pattern = pattern;
        }

        public override string ClassName => nameof(IsPatternMatchRule);
        public string Pattern { get; }

        public override bool Check()
            => string.IsNullOrEmpty(Value) ? true : Regex.IsMatch(Value, Pattern);
    }
}
