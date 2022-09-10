using Contoso.XPlatform.Flow.Settings.Screen;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.ViewModels;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    internal static class ViewModelServices
    {
        internal static IServiceCollection AddViewModels(this IServiceCollection services) 
            => services.AddTransient<MainPageViewModel>()
                .AddTransient<Func<ScreenSettingsBase, EditFormViewModel>>
                (
                    provider =>
                    screenSettings => ActivatorUtilities.CreateInstance<EditFormViewModel>
                    (
                        provider,
                        provider.GetRequiredService<IContextProvider>(),
                        screenSettings
                    )
                )
                .AddTransient<Func<ScreenSettingsBase, DetailFormViewModel>>
                (
                    provider =>
                    screenSettings => ActivatorUtilities.CreateInstance<DetailFormViewModel>
                    (
                        provider,
                        provider.GetRequiredService<IContextProvider>(),
                        screenSettings
                    )
                )
                .AddTransient<Func<ScreenSettingsBase, SearchPageViewModel>>
                (
                    provider =>
                    screenSettings => ActivatorUtilities.CreateInstance<SearchPageViewModel>
                    (
                        provider,
                        provider.GetRequiredService<IContextProvider>(),
                        screenSettings
                    )
                )
                .AddTransient<Func<ScreenSettingsBase, ListPageViewModel>>
                (
                    provider =>
                    screenSettings => ActivatorUtilities.CreateInstance<ListPageViewModel>
                    (
                        provider,
                        provider.GetRequiredService<IContextProvider>(),
                        screenSettings
                    )
                )
                .AddTransient<Func<ScreenSettingsBase, TextPageViewModel>>
                (
                    provider =>
                    screenSettings => ActivatorUtilities.CreateInstance<TextPageViewModel>
                    (
                        provider,
                        screenSettings
                    )
                )
                .AddTransient<ExtendedSplashViewModel>();
    }
}
