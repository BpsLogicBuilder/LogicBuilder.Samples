using Enrollment.Forms.Configuration;
using Enrollment.Forms.Configuration.DataForm;
using Enrollment.XPlatform.Constants;
using Enrollment.XPlatform.Utils;
using Enrollment.XPlatform.ViewModels;
using Enrollment.XPlatform.ViewModels.DetailForm;
using Enrollment.XPlatform.ViewModels.ReadOnlys;
using Microsoft.Maui.Controls;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Enrollment.XPlatform.Views
{
    public class DetailFormViewCS : ContentPage
    {
        public DetailFormViewCS(DetailFormViewModelBase detailFormViewModel)
        {
            this.detailFormEntityViewModel = detailFormViewModel;
            AddContent();
            //Visual = VisualMarker.Default;
            BindingContext = this.detailFormEntityViewModel;
        }

        private readonly DetailFormViewModelBase detailFormEntityViewModel;
        private Grid transitionGrid;
        private VerticalStackLayout page;

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (transitionGrid.IsVisible)
                await page.EntranceTransition(transitionGrid, 150);
        }

        [MemberNotNull(nameof(transitionGrid), nameof(page))]
        private void AddContent()
        {
            LayoutHelpers.AddToolBarItems(this.ToolbarItems, this.detailFormEntityViewModel.Buttons);
            Title = detailFormEntityViewModel.FormSettings.Title;

            BindingBase GetLabelBinding(MultiBindingDescriptor multiBindingDescriptor, string bindingName)
            {
                if (multiBindingDescriptor == null)
                    return new Binding(bindingName);

                return new MultiBinding
                {
                    StringFormat = multiBindingDescriptor.StringFormat,
                    Bindings = multiBindingDescriptor.Fields.Select
                    (
                        field => new Binding($"{nameof(ReadOnlyControlGroupBox.BindingPropertiesDictionary)}[{field.ToBindingDictionaryKey()}].{nameof(IReadOnly.Value)}")
                    )
                    .Cast<BindingBase>()
                    .ToList()
                };
            }

            Content = new Grid
            {
                Children =
                {
                    (
                        page = new VerticalStackLayout
                        {
                            Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.DetailFormStackLayoutStyle),
                            Children =
                            {
                                new Label
                                {
                                    Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.HeaderStyle)
                                }
                                .AddBinding
                                (
                                    Label.TextProperty,
                                    GetLabelBinding
                                    (
                                        detailFormEntityViewModel.FormSettings.HeaderBindings,
                                        $"{nameof(DetailFormViewModelBase.FormSettings)}.{nameof(DataFormSettingsDescriptor.Title)}"
                                    )
                                ),
                                new Label
                                {
                                    IsVisible = detailFormEntityViewModel.FormSettings.FormType == FormType.Delete,
                                    Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.DetailFormDeleteQuestionStyle)
                                }
                                .AddBinding
                                (
                                    Label.TextProperty,
                                    GetLabelBinding
                                    (
                                        detailFormEntityViewModel.FormSettings.SubtitleBindings,
                                        $"{nameof(DetailFormViewModelBase.FormSettings)}.{nameof(DataFormSettingsDescriptor.Title)}"
                                    )
                                ),
                                new ScrollView
                                {
                                    Content = detailFormEntityViewModel.FormLayout.ControlGroupBoxList.Aggregate
                                    (
                                        new VerticalStackLayout(),
                                        (stackLayout, controlBox) =>
                                        {
                                            if (controlBox.IsVisible == false)
                                                return stackLayout;

                                            stackLayout.Children.Add
                                            (
                                                new Label
                                                {
                                                    Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.DetailFormGroupHeaderStyle),
                                                    BindingContext = controlBox
                                                }
                                                .AddBinding
                                                (
                                                    Label.TextProperty,
                                                    GetLabelBinding(controlBox.HeaderBindings, $"{nameof(ReadOnlyControlGroupBox.GroupHeader)}")
                                                )
                                            );
                                            stackLayout.Children.Add
                                            (
                                                new VerticalStackLayout
                                                {
                                                    VerticalOptions = LayoutOptions.Start,
                                                    BindingContext = controlBox
                                                }
                                                .AddBinding(BindableLayout.ItemsSourceProperty, new Binding("."))
                                                .SetDataTemplateSelector(DetailFormViewHelpers.ReadOnlyControlTemplateSelector)
                                            );

                                            return stackLayout;
                                        }
                                    )
                                }
                            }
                        }
                    ),
                    (
                        transitionGrid = new Grid
                        {
                            Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.TransitionGridStyle)
                        }
                    )
                }
            };
        }
    }
}