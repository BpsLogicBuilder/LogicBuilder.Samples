using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Enrollment.XPlatform.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BusyIndicator : ContentPage
    {
        public BusyIndicator()
        {
            InitializeComponent();
            Visual = VisualMarker.Material;
            this.BackgroundColor = Color.Transparent;
        }
    }
}