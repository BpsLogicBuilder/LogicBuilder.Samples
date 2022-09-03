using Android.App;
using Android.Content.PM;
using Android.OS;
using Contoso.XPlatform.Flow;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui;

namespace Contoso.XPlatform
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
    }
}