
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contoso.XPlatform.Themes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LightTheme : ResourceDictionary
    {
        public LightTheme()
        {
            InitializeComponent();
        }
    }
}