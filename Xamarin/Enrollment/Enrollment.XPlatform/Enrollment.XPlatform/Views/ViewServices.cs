using Enrollment.Forms.Configuration.DataForm;
using Enrollment.Forms.Configuration.Validation;
using Enrollment.XPlatform.ViewModels.DetailForm;
using Enrollment.XPlatform.ViewModels.EditForm;
using Enrollment.XPlatform.ViewModels.ListPage;
using Enrollment.XPlatform.ViewModels.ReadOnlys;
using Enrollment.XPlatform.ViewModels.SearchPage;
using Enrollment.XPlatform.ViewModels.TextPage;
using Enrollment.XPlatform.ViewModels.Validatables;
using Enrollment.XPlatform.Views;
using Enrollment.XPlatform.Views.Factories;
using System;
using System.Collections.Generic;

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
