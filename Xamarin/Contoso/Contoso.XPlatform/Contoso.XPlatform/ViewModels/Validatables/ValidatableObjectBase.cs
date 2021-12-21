using Contoso.XPlatform.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Contoso.XPlatform.ViewModels.Validatables
{
    public class ValidatableObjectBase<T> : IValidatable
    {
        public ValidatableObjectBase(string name, string templateName, IEnumerable<IValidationRule> validations, UiNotificationService uiNotificationService)
        {
            Name = name;
            TemplateName = templateName;
            Validations = validations?.ToList();
            this.uiNotificationService = uiNotificationService;
        }

        #region Fields
        private T _value;
        private bool _isValid = true;
        private string _name;
        private bool _isDirty;
        private bool _isVisible = true;
        private bool _isEnabled = true;
        private Dictionary<string, string> _errors = new Dictionary<string, string>();
        private string _templateName;
        protected UiNotificationService uiNotificationService;

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion Fields

        #region Properties
        public bool IsDirty
        {
            get => _isDirty;
            set
            {
                if (_isDirty == value)
                    return;

                _isDirty = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                if (_name == value)
                    return;

                _name = value;
                OnPropertyChanged();
            }
        }

        public string TemplateName
        {
            get => _templateName;
            set
            {
                if (_templateName == value)
                    return;

                _templateName = value;
                OnPropertyChanged();
            }
        }

        public bool IsValid
        {
            get => _isValid;
            set
            {
                if (_isValid == value)
                    return;

                _isValid = value;
                OnPropertyChanged();
            }
        }

        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                if (_isVisible == value)
                    return;

                _isVisible = value;
                OnPropertyChanged();
            }
        }

        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                if (_isEnabled == value)
                    return;

                _isEnabled = value;
                OnPropertyChanged();
            }
        }

        public virtual T Value
        {
            get => _value;
            set
            {
                if (EqualityComparer<T>.Default.Equals(_value, value))
                {
                    if (EqualityComparer<T>.Default.Equals(_value, default(T)))
                    {
                        this.uiNotificationService.NotifyPropertyChanged(this.Name);
                    }

                    return;
                }

                _value = value;
                this.uiNotificationService.NotifyPropertyChanged(this.Name);
                OnPropertyChanged();
            }
        }

        public Dictionary<string, string> Errors
        {
            get => _errors;
            set
            {
                if (_errors == value)
                    return;

                _errors = value;
                OnPropertyChanged();
            }
        }

        public List<IValidationRule> Validations { get; }

        object IFormField.Value { get => Value; set => Value = (T)value; }

        public Type Type => typeof(T);
        #endregion Properties

        public virtual bool Validate()
        {
            Errors = Validations
                        ?.Where(v => !v.Check())
                        .ToDictionary(v => v.ClassName, v => v.ValidationMessage);

            IsValid = Errors?.Any() != true;

            return this.IsValid;
        }

        public virtual void Clear()
        {
            Value = default;
            IsValid = Validate();
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
