using Contoso.XPlatform.Validators;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contoso.XPlatform.ViewModels.Validatables
{
    public class CheckboxValidatableObject : ValidatableObjectBase<bool>
    {
        public CheckboxValidatableObject(UiNotificationService uiNotificationService, string name, string templateName, string checkboxLabel, IEnumerable<IValidationRule>? validations)
            : base(name, templateName, validations, uiNotificationService)
        {
            /*MemberNotNull unvailable in 2.1*/
            _checkboxLabel = null!;
            /*MemberNotNull unvailable in 2.1*/
            CheckboxLabel = checkboxLabel;
        }

        private string _checkboxLabel;
        public string CheckboxLabel
        {
            get => _checkboxLabel;
            //[MemberNotNull(nameof(_checkboxLabel))]
            set
            {
                if (_checkboxLabel == value)
                    return;

                _checkboxLabel = value;
                OnPropertyChanged();
            }
        }

        public ICommand CheckedChangedCommand => new Command
        (
            () =>
            {
                IsDirty = true;
                IsValid = Validate();
            }
        );
    }
}
