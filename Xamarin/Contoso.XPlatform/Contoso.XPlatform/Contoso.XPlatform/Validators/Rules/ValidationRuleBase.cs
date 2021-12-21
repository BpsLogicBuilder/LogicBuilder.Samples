using Contoso.XPlatform.ViewModels.Validatables;
using System.Collections.Generic;
using System.Linq;

namespace Contoso.XPlatform.Validators.Rules
{
    public abstract record ValidationRuleBase<T> : IValidationRule
    {
        protected ValidationRuleBase(string fieldName, string validationMessage, ICollection<IValidatable> allProperties)
        {
            FieldName = fieldName;
            ValidationMessage = validationMessage;
            AllProperties = allProperties;
        }

        public string FieldName { get; }
        public abstract string ClassName { get; }
        public string ValidationMessage { get; set; }
        public ICollection<IValidatable> AllProperties { get; set; }

        protected IDictionary<string, IValidatable> PropertiesDictionary
            => AllProperties.ToDictionary(p => p.Name);

        protected T Value
            => ((ValidatableObjectBase<T>)PropertiesDictionary[FieldName]).Value;

        public abstract bool Check();
    }
}