using Contoso.XPlatform.ViewModels.Validatables;
using System.Collections.Generic;

namespace Contoso.XPlatform.Validators.Rules
{
    public record IsLengthValidRule : ValidationRuleBase<string>
    {
        public IsLengthValidRule(string fieldName, string validationMessage, ICollection<IValidatable> allProperties, int minimunLength, int maximunLength)
            : base(fieldName, validationMessage, allProperties)
        {
            MinimunLength = minimunLength;
            MaximunLength = maximunLength;
        }

        public override string ClassName { get => nameof(IsLengthValidRule); }
        public int MinimunLength { get; }
        public int MaximunLength { get; }

        public override bool Check()
        {
            if (Value == null)
                return false;

            return Value.Length >= MinimunLength && Value.Length <= MaximunLength;
        }
    }
}
