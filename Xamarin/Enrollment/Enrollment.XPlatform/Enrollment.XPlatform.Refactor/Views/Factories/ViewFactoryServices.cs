using Microsoft.Extensions.DependencyInjection;

namespace Enrollment.XPlatform.Views.Factories
{
    internal static class ViewFactoryServices
    {
        internal static IServiceCollection AddViewFactories(this IServiceCollection services) 
            => services
                .AddTransient<IDetailFormFactory, DetailFormFactory>()
                .AddTransient<IFlyoutDetailPageFactory, FlyoutDetailPageFactory>()
                .AddTransient<IEditFormFactory, EditFormFactory>()
                .AddTransient<IListPageFactory, ListPageFactory>()
                .AddTransient<IPopupFormFactory, PopupFormFactory>()
                .AddTransient<ISearchPageFactory, SearchPageFactory>()
                .AddTransient<ITextPageFactory, TextPageFactory>();
    }
}
