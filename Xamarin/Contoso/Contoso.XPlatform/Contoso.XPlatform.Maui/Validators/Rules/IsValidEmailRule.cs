using Contoso.XPlatform.ViewModels.Validatables;
using System.Collections.Generic;

namespace Contoso.XPlatform.Validators.Rules
{
    public record IsValidEmailRule : ValidationRuleBase<string>
    {
        public IsValidEmailRule(string fieldName, string validationMessage, ICollection<IValidatable> allProperties)
            : base(fieldName, validationMessage, allProperties)
        {
        }

        public override string ClassName { get => nameof(IsValidEmailRule); }

        public override bool Check()
        {
            try
            {
                return string.IsNullOrEmpty(Value) ? true : new System.Net.Mail.MailAddress(Value).Address == Value;
            }
            catch
            {
                return false;
            }
        }
    }
}
