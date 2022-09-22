using Android.App;
using Android.Runtime;
using Enrollment.XPlatform.Flow;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using System;

namespace Enrollment.XPlatform
{
    [Application]
    public class MainApplication : MauiApplication
    {
        public MainApplication(IntPtr handle, JniHandleOwnership ownership)
            : base(handle, ownership)
        {
        }

        protected override MauiApp CreateMauiApp()
        {
            MauiApp mauiApp = MauiProgram.CreateMauiApp();
            MauiProgram.Init(ConfigureServices);
            return mauiApp;
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IAppLogger, AppLogger>();
        }
    }
}