using Contoso.XPlatform.Validators;
using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Contoso.XPlatform.ViewModels.Validatables
{
    public class EntryValidatableObject<T> : ValidatableObjectBase<T>
    {
        public EntryValidatableObject(UiNotificationService uiNotificationService, string name, string templateName, string placeholder, string stringFormat, IEnumerable<IValidationRule>? validations) 
            : base(name, templateName, validations, uiNotificationService)
        {
            Placeholder = placeholder;
            StringFormat = stringFormat;
        }

        public string StringFormat { get; }

        private string _placeholder;
        public string Placeholder
        {
            get => _placeholder;
            [MemberNotNull(nameof(_placeholder))]
            set
            {
                if (_placeholder == value)
                    return;

                _placeholder = value;
                OnPropertyChanged();
            }
        }

        private ICommand? _textChangedCommand;
        public ICommand TextChangedCommand
        {
            get
            {
                if (_textChangedCommand != null)
                    return _textChangedCommand;

                string? text = null;
                _textChangedCommand = new Command
                (
                    async (parameter) =>
                    {
                        IsDirty = true;
                        const int debounceDelay = 1000;
                        text = ((TextChangedEventArgs)parameter).NewTextValue;
                        if (text == null)
                            return;

                        await Task.Delay(debounceDelay).ContinueWith
                        (
                            (task, oldText) =>
                            {
                                if (text == (string)oldText!)/*oldText is never null*/
                                    IsValid = Validate();
                            },
                            text
                        );
                    }
                );

                return _textChangedCommand;
            }
        }
    }
}
