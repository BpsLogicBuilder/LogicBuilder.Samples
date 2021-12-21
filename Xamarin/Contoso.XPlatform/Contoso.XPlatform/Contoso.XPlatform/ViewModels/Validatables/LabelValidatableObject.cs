using Contoso.Forms.Configuration.DataForm;
using Contoso.XPlatform.Validators;
using System.Collections.Generic;
using System.Globalization;

namespace Contoso.XPlatform.ViewModels.Validatables
{
    public class LabelValidatableObject<T> : ValidatableObjectBase<T>
    {
        public LabelValidatableObject(string name, string templateName, string title, string placeholder, string stringFormat, IEnumerable<IValidationRule> validations, UiNotificationService uiNotificationService)
            : base(name, templateName, validations, uiNotificationService)
        {
            Placeholder = placeholder;
            Title = title;
            this.stringFormat = stringFormat;
        }

        private string stringFormat;

        public string DisplayText
        {
            get
            {
                if (EqualityComparer<T>.Default.Equals(Value, default(T)))
                    return string.Empty;

                if (string.IsNullOrEmpty(this.stringFormat))
                    return Value.ToString();

                return string.Format(CultureInfo.CurrentCulture, this.stringFormat, Value);
            }
        }

        public override T Value
        {
            get { return base.Value; }
            set
            {
                base.Value = value;

                OnPropertyChanged(nameof(DisplayText));
            }
        }

        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                if (_title == value)
                    return;

                _title = value;
                OnPropertyChanged();
            }
        }

        private string _placeholder;
        public string Placeholder
        {
            get => _placeholder; set
            {
                if (_placeholder == value)
                    return;

                _placeholder = value;
                OnPropertyChanged();
            }
        }
    }
}
