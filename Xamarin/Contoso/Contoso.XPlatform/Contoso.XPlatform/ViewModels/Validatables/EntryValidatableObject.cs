using Contoso.XPlatform.Validators;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contoso.XPlatform.ViewModels.Validatables
{
    public class EntryValidatableObject<T> : ValidatableObjectBase<T>
    {
        public EntryValidatableObject(UiNotificationService uiNotificationService, string name, string templateName, string placeholder, string stringFormat, IEnumerable<IValidationRule>? validations) 
            : base(name, templateName, validations, uiNotificationService)
        {
            /*MemberNotNull unvailable in 2.1*/
            _placeholder = null!;
            /*MemberNotNull unvailable in 2.1*/
            Placeholder = placeholder;
            StringFormat = stringFormat;
        }

        public string StringFormat { get; }

        private string _placeholder;
        public string Placeholder
        {
            get => _placeholder;
            //[MemberNotNull(nameof(_placeholder))]
            set
            {
                if (_placeholder == value)
                    return;

                _placeholder = value;
                OnPropertyChanged();
            }
        }

        public ICommand TextChangedCommand => new Command
        (
            async (parameter) =>
            {
                IsDirty = true;
                const int debounceDelay = 1000;
                string text = ((TextChangedEventArgs)parameter).NewTextValue;
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
    }
}
