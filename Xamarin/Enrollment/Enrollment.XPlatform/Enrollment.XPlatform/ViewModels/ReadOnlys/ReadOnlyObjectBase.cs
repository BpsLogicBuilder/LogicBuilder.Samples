using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Enrollment.XPlatform.ViewModels.ReadOnlys
{
    public abstract class ReadOnlyObjectBase<T> : IReadOnly
    {
        protected ReadOnlyObjectBase(string name, string templateName, IUiNotificationService uiNotificationService)
        {
            /*MemberNotNull unvailable in 2.1*/
            _name = null!;
            _templateName = null!;
            /*MemberNotNull unavailable in 2.1*/
            Name = name;
            TemplateName = templateName;
            this.uiNotificationService = uiNotificationService;
        }

        #region Fields
        private T? _value;
        private string _name;
        private string _templateName;
        private bool _isVisible = true;
        protected IUiNotificationService uiNotificationService;

        public event PropertyChangedEventHandler? PropertyChanged;
        #endregion Fields

        #region Properties
        public string Name
        {
            get => _name;
            //[MemberNotNull(nameof(_name))]
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
            //[MemberNotNull(nameof(_templateName))]
            set
            {
                if (_templateName == value)
                    return;

                _templateName = value;
                OnPropertyChanged();
            }
        }

        public virtual T? Value
        {
            get => _value;
            set
            {
                if (EqualityComparer<T>.Default.Equals(_value!, value!))/*EqualityComparer not built for nullable reference types in 2.1*/
                {
                    if (EqualityComparer<T>.Default.Equals(_value!, default!))/*EqualityComparer not built for nullable reference types in 2.1*/
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

        object? IFormField.Value { get => Value; set => Value = (T?)value; }
        #endregion Properties

        public virtual void Clear()
        {
            Value = default;
        }

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
            => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
