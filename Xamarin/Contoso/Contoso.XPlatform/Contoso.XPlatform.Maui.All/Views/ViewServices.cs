using Contoso.XPlatform.ViewModels;
using Contoso.XPlatform.ViewModels.DetailForm;
using Contoso.XPlatform.ViewModels.EditForm;
using Contoso.XPlatform.ViewModels.ListPage;
using Contoso.XPlatform.ViewModels.ReadOnlys;
using Contoso.XPlatform.ViewModels.SearchPage;
using Contoso.XPlatform.ViewModels.TextPage;
using Contoso.XPlatform.ViewModels.Validatables;
using Contoso.XPlatform.Views;
using Contoso.XPlatform.Views.Factories;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    internal static class ViewServices
    {
        internal static IServiceCollection AddViews(this IServiceCollection services)
        {
            return services
                .AddTransient<ExtendedSplashView>()
                .AddTransient<MainPageView>()
                .AddTransient<Func<IValidatable, ChildFormArrayPageCS>>
                (
                    provider =>
                    validatable => ActivatorUtilities.CreateInstance<ChildFormArrayPageCS>
                    (
                        provider,
                        validatable
                    )
                )
                .AddTransient<Func<IValidatable, ChildFormPageCS>>
                (
                    provider =>
                    validatable => ActivatorUtilities.CreateInstance<ChildFormPageCS>
                    (
                        provider,
                        validatable
                    )
                )
                .AddTransient<Func<DetailFormViewModelBase, DetailFormViewCS>>
                (
                    provider =>
                    viewModel => ActivatorUtilities.CreateInstance<DetailFormViewCS>
                    (
                        provider,
                        viewModel
                    )
                )
                .AddTransient<Func<EditFormViewModelBase, EditFormViewCS>>
                (
                    provider =>
                    viewModel => ActivatorUtilities.CreateInstance<EditFormViewCS>
                    (
                        provider,
                        viewModel
                    )
                )
                .AddTransient<Func<ListPageViewModelBase, ListPageViewCS>>
                (
                    provider =>
                    viewModel => ActivatorUtilities.CreateInstance<ListPageViewCS>
                    (
                        provider,
                        viewModel
                    )
                )
                .AddTransient<Func<IValidatable, MultiSelectPageCS>>
                (
                    provider =>
                    validatable => ActivatorUtilities.CreateInstance<MultiSelectPageCS>
                    (
                        provider,
                        validatable
                    )
                )
                .AddTransient<Func<IReadOnly, ReadOnlyChildFormArrayPageCS>>
                (
                    provider =>
                    readOnly => ActivatorUtilities.CreateInstance<ReadOnlyChildFormArrayPageCS>
                    (
                        provider,
                        readOnly
                    )
                )
                .AddTransient<Func<IReadOnly, ReadOnlyChildFormPageCS>>
                (
                    provider =>
                    readOnly => ActivatorUtilities.CreateInstance<ReadOnlyChildFormPageCS>
                    (
                        provider,
                        readOnly
                    )
                )
                .AddTransient<Func<IReadOnly, ReadOnlyMultiSelectPageCS>>
                (
                    provider =>
                    readOnly => ActivatorUtilities.CreateInstance<ReadOnlyMultiSelectPageCS>
                    (
                        provider,
                        readOnly
                    )
                )
                .AddTransient<Func<SearchPageViewModelBase, SearchPageViewCS>>
                (
                    provider =>
                    viewModel => ActivatorUtilities.CreateInstance<SearchPageViewCS>
                    (
                        provider,
                        viewModel
                    )
                )
                .AddTransient<Func<TextPageViewModel, TextPageViewCS>>
                (
                    provider =>
                    viewModel => ActivatorUtilities.CreateInstance<TextPageViewCS>
                    (
                        provider,
                        viewModel
                    )
                )
                .AddViewFactories();
        }
    }
}
