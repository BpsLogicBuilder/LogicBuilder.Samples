using Contoso.Forms.Configuration.DataForm;
using Contoso.XPlatform.Services;
using System.Collections.Generic;
using System.Globalization;

namespace Contoso.XPlatform.ViewModels.ReadOnlys
{
    public class TextFieldReadOnlyObject<T> : ReadOnlyObjectBase<T>
    {
        public TextFieldReadOnlyObject(string name, string templateName, string title, string stringFormat, IContextProvider contextProvider) : base(name, templateName, contextProvider.UiNotificationService)
        {
            this.Title = title;
            this._stringFormat = stringFormat;
        }

        private readonly string _stringFormat;

        public string DisplayText
        {
            get
            {
                if (EqualityComparer<T>.Default.Equals(Value, default(T)))
                    return string.Empty;

                if (string.IsNullOrEmpty(this._stringFormat))
                    return Value.ToString();

                return string.Format(CultureInfo.CurrentCulture, this._stringFormat, Value);
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

        public override T Value
        {
            get { return base.Value; }
            set
            {
                base.Value = value;
                OnPropertyChanged(nameof(DisplayText));
            }
        }
    }
}
