using System;
using System.Collections.ObjectModel;

namespace Enrollment.XPlatform.ViewModels
{
    public class FlyoutMenuItem : ViewModelBase
    {
        private string? _initialModule;
        private string? _text;
        private bool _active;
        private ObservableCollection<FlyoutMenuItem>? _subItems;
        private string? _icon;

        public string InitialModule
        {
            get => _initialModule ?? throw new ArgumentException(nameof(_initialModule));
            set
            {
                _initialModule = value;
                OnPropertyChanged();
            }
        }

        public string Text
        {
            get => _text ?? throw new ArgumentException(nameof(_text));
            set
            {
                _text = value;
                OnPropertyChanged();
            }
        }

        public string Icon
        {
            get => _icon ?? throw new ArgumentException(nameof(_icon));
            set
            {
                _icon = value;
                OnPropertyChanged();
            }
        }

        public bool Active
        {
            get => _active;
            set
            {
                _active = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<FlyoutMenuItem>? SubItems
        {
            get => _subItems;
            set
            {
                _subItems = value;
                OnPropertyChanged();
            }
        }

        public override bool Equals(object? obj)
        {
            if (obj == null)
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj is not FlyoutMenuItem flyoutMenuItem)
                return false;

            return this.Text == flyoutMenuItem.Text && this.Active == flyoutMenuItem.Active;
        }

        public override int GetHashCode()
        {
            return (_text ?? string.Empty).GetHashCode();
        }
    }
}
