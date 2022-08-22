using Contoso.XPlatform.ViewModels.Validatables;
using System.Collections.Generic;

namespace Contoso.XPlatform.Validators.Rules
{
    public record IsMatchRule<T> : ValidationRuleBase<T>
    {
        public IsMatchRule(string fieldName, string validationMessage, ICollection<IValidatable> allProperties, string otherFieldName)
            : base(fieldName, validationMessage, allProperties)
        {
            OtherFieldName = otherFieldName;
        }

        public override string ClassName { get => nameof(IsMatchRule<T>); }
        public string OtherFieldName { get; }
        private ValidatableObjectBase<T> OtherValidatable => (ValidatableObjectBase<T>)PropertiesDictionary[OtherFieldName];

        public override bool Check()
        {
            if (!EqualityComparer<T>.Default.Equals(Value, OtherValidatable.Value)) 
                return false;

            if (!OtherValidatable.Errors.ContainsKey(nameof(IsMatchRule<T>)))
                return true;

            //Clear the error in this control first (otherwise results in circular logic calling OtherValidatable.Validate())
            if (PropertiesDictionary[FieldName].Errors.ContainsKey(nameof(IsMatchRule<T>)))
                PropertiesDictionary[FieldName].Errors.Remove(nameof(IsMatchRule<T>));

            //Then check for validity in the other control (updates the UI for the other control).
            OtherValidatable.Validate();

            return true;
        }
    }
}
