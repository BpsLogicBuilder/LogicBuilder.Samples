using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Contoso.XPlatform.ViewModels.ReadOnlys
{
    public abstract class ReadOnlyObjectBase<T> : IReadOnly
    {
        protected ReadOnlyObjectBase(string name, string templateName, UiNotificationService uiNotificationService)
        {
            Name = name;
            TemplateName = templateName;
            this.uiNotificationService = uiNotificationService;
        }

        #region Fields
        private T _value;
        private string _name;
        private string _templateName;
        private bool _isVisible = true;
        protected UiNotificationService uiNotificationService;

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion Fields

        #region Properties
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

        public virtual T Value
        {
            get => _value;
            set
            {
                if (EqualityComparer<T>.Default.Equals(_value, value))
                {
                    if (EqualityComparer<T>.Default.Equals(_value, default))
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

        object IFormField.Value { get => Value; set => Value = (T)value; }
        #endregion Properties

        public virtual void Clear()
        {
            Value = default;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
