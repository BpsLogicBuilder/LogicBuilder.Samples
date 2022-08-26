using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Microsoft.Extensions.DependencyInjection;
using Contoso.XPlatform.Flow;
using Microsoft.Maui;

namespace Contoso.XPlatform.Droid
{
    [Activity(Label = "Contoso.XPlatform", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnCreate(Bundle? savedInstanceState)
        {
            MauiProgram.Init(ConfigureServices);
            base.OnCreate(savedInstanceState);
        }

        void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IAppLogger, AppLogger>();
        }
    }
}