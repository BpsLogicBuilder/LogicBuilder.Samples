using Contoso.XPlatform.ViewModels.Validatables;
using System.Collections.Generic;
using System.Linq;

namespace Contoso.XPlatform.Validators.Rules
{
    public record AtLeastOneRequiredRule<T> : ValidationRuleBase<T> where T : IEnumerable<object>
    {
        public AtLeastOneRequiredRule(string fieldName, string validationMessage, ICollection<IValidatable> allProperties)
            : base(fieldName, validationMessage, allProperties)
        {
        }

        public override string ClassName { get => nameof(AtLeastOneRequiredRule<List<string>>); }

        public override bool Check() => Value?.Any() == true;
    }
}
