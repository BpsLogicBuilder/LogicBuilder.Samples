using CheckMySymptoms.Flow.ScreenSettings;
using CheckMySymptoms.Forms.View;
using CheckMySymptoms.Forms.View.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;

namespace CheckMySymptoms.ViewModels
{
    public class MainPageViewModel : ObservableObject
    {
        public MainPageViewModel()
        {
            _menuItems = new ObservableCollection<MenuItem>();
        }

        
        public ObservableCollection<CommandButtonView> CommandButtons { get; set; }
        public ObservableCollection<string> DialogList { get; set; }


        private ObservableCollection<MenuItem> _menuItems;
        public ObservableCollection<MenuItem> MenuItems
        {
            get { return _menuItems; }
            set
            {
                Set(ref _menuItems, value);
                BackButtonEnabled = _menuItems?.Count > 1;
            }
        }

        private bool _backButtonEnabled;

        public bool BackButtonEnabled
        {
            get { return _backButtonEnabled; }
            set { Set(ref _backButtonEnabled, value); }
        }

        //public ObservableCollection<CategoryBase> Categories { get; set; }
    }
}
