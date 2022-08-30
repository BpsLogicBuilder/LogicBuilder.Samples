using Microsoft.Maui.Controls;

namespace Contoso.XPlatform
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