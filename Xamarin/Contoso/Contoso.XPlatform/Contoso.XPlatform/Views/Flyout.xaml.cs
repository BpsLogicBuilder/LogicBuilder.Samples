using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contoso.XPlatform.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Flyout : ContentPage
    {
        public CollectionView ListView;

        public Flyout()
        {
            InitializeComponent();
            Visual = VisualMarker.Material;
            ListView = MenuItemsListView;
        }
    }
}