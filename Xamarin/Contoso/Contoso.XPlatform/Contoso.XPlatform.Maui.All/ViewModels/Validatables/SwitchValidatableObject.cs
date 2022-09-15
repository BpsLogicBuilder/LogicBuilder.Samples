using Contoso.XPlatform.Validators;
using System.Collections.Generic;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using Microsoft.Maui;
using System.Diagnostics.CodeAnalysis;

namespace Contoso.XPlatform.ViewModels.Validatables
{
    public class SwitchValidatableObject : ValidatableObjectBase<bool>
    {
        public SwitchValidatableObject(UiNotificationService uiNotificationService, string name, string templateName, string switchLabel, IEnumerable<IValidationRule>? validations)
            : base(name, templateName, validations, uiNotificationService)
        {
            SwitchLabel = switchLabel;
        }

        private string _switchLabel;
        public string SwitchLabel
        {
            get => _switchLabel;
            [MemberNotNull(nameof(_switchLabel))]
            set
            {
                if (_switchLabel == value)
                    return;

                _switchLabel = value;
                OnPropertyChanged();
            }
        }

        public override bool Value
        {
            get => base.Value;
            set
            {
                base.Value = value;
                this.uiNotificationService.NotifyPropertyChanged(this.Name);
                OnPropertyChanged();
            }
        }

        public ICommand ToggledCommand => new Command
        (
            () =>
            {
                IsDirty = true;
                IsValid = Validate();
            }
        );
    }
}
