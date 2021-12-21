
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contoso.XPlatform.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChildFormArrayPage : ContentPage
    {
        public ChildFormArrayPage()
        {
            InitializeComponent();
            this.BackgroundColor = Color.Transparent;
        }
    }
}