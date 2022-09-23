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
                    validatable => new ChildFormArrayPageCS
                    (
                        validatable
                    )
                )
                .AddTransient<Func<IValidatable, ChildFormPageCS>>
                (
                    provider =>
                    validatable => new ChildFormPageCS
                    (
                        validatable
                    )
                )
                .AddTransient<Func<DetailFormViewModelBase, DetailFormViewCS>>
                (
                    provider =>
                    viewModel => new DetailFormViewCS
                    (
                        viewModel
                    )
                )
                .AddTransient<Func<EditFormViewModelBase, EditFormViewCS>>
                (
                    provider =>
                    viewModel => new EditFormViewCS
                    (
                        viewModel
                    )
                )
                .AddTransient<Func<ListPageViewModelBase, ListPageViewCS>>
                (
                    provider =>
                    viewModel => new ListPageViewCS
                    (
                        viewModel
                    )
                )
                .AddTransient<Func<IValidatable, MultiSelectPageCS>>
                (
                    provider =>
                    validatable => new MultiSelectPageCS
                    (
                        validatable
                    )
                )
                .AddTransient<Func<IReadOnly, ReadOnlyChildFormArrayPageCS>>
                (
                    provider =>
                    readOnly => new ReadOnlyChildFormArrayPageCS
                    (
                        readOnly
                    )
                )
                .AddTransient<Func<IReadOnly, ReadOnlyChildFormPageCS>>
                (
                    provider =>
                    readOnly => new ReadOnlyChildFormPageCS
                    (
                        readOnly
                    )
                )
                .AddTransient<Func<IReadOnly, ReadOnlyMultiSelectPageCS>>
                (
                    provider =>
                    readOnly => new ReadOnlyMultiSelectPageCS
                    (
                        readOnly
                    )
                )
                .AddTransient<Func<SearchPageViewModelBase, SearchPageViewCS>>
                (
                    provider =>
                    viewModel => new SearchPageViewCS
                    (
                        viewModel
                    )
                )
                .AddTransient<Func<TextPageViewModel, TextPageViewCS>>
                (
                    provider =>
                    viewModel => new TextPageViewCS
                    (
                        viewModel
                    )
                )
                .AddViewFactories();
        }
    }
}
