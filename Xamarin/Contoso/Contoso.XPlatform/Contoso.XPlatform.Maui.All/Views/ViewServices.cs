using Contoso.XPlatform.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    internal static class ViewServices
    {
        internal static IServiceCollection AddViews(this IServiceCollection services)
        {
            return services
                .AddTransient<ExtendedSplashView>()
                .AddTransient<MainPageView>();
        }
    }
}
