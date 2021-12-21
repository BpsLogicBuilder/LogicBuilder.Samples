using Contoso.Forms.Configuration.Navigation;
using System.Collections.ObjectModel;

namespace Contoso.XPlatform.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private ObservableCollection<NavigationMenuItemDescriptor> _menuItems;
        public ObservableCollection<NavigationMenuItemDescriptor> MenuItems
        {
            get { return _menuItems; }
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
