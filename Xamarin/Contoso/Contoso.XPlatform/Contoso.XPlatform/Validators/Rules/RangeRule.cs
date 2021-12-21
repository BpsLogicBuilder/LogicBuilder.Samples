using Contoso.XPlatform.ViewModels.Validatables;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contoso.XPlatform.Validators.Rules
{
    public record RangeRule<T> : ValidationRuleBase<T> where T : IComparable<T>
    {
        public RangeRule(string fieldName, string validationMessage, ICollection<IValidatable> allProperties, T min,  T max)
            : base(fieldName, validationMessage, allProperties)
        {
            Min = min;
            Max = max;
        }

        public override string ClassName => nameof(RangeRule<T>);
        public T Min { get; }
        public T Max { get; }

        public override bool Check()
        {
            if (Min.CompareTo(Value) > 0)
                return false;

            if (Max.CompareTo(Value) < 0)
                return false;

            return true;
        }
    }
}
