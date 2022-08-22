using Contoso.Forms.Configuration.Navigation;
using System;
using System.Collections.ObjectModel;

namespace Contoso.XPlatform.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private ObservableCollection<NavigationMenuItemDescriptor>? _menuItems;
        public ObservableCollection<NavigationMenuItemDescriptor> MenuItems
        {
            get { return _menuItems ?? throw new ArgumentException($"{nameof(_menuItems)}: {{84CB529B-62A4-4C5C-AF7F-7858C8164139}}"); }
            set
            {
                if (_menuItems == value)
                    return;


                _menuItems = value;
                OnPropertyChanged();
            }
        }
    }
}
