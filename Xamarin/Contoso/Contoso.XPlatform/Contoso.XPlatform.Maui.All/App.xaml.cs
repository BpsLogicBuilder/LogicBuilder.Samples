using Microsoft.Maui.Controls;

namespace Contoso.XPlatform.Maui
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}