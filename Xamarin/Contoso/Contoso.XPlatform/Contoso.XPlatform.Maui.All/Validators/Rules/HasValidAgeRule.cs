using Contoso.XPlatform.ViewModels.Validatables;
using System;
using System.Collections.Generic;

namespace Contoso.XPlatform.Validators.Rules
{
    public record HasValidAgeRule : ValidationRuleBase<DateTime>
    {
        public HasValidAgeRule(string fieldName, string validationMessage, ICollection<IValidatable> allProperties) 
            : base(fieldName, validationMessage, allProperties)
        {
        }

        public override string ClassName { get => nameof(HasValidAgeRule); }

       public override bool Check()
        {
            int age = DateTime.Today.Year - Value.Year;
            return age >= 18;
        }
    }
}
