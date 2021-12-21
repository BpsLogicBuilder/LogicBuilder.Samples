using Contoso.Forms.Configuration;
using Contoso.Forms.Configuration.DataForm;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.ViewModels;
using Contoso.XPlatform.ViewModels.DetailForm;
using Contoso.XPlatform.ViewModels.ReadOnlys;
using System.Linq;

using Xamarin.Forms;

namespace Contoso.XPlatform.Views
{
    public class DetailFormViewCS : ContentPage
    {
        public DetailFormViewCS(DetailFormViewModel detailFormViewModel)
        {
            this.detailFormEntityViewModel = detailFormViewModel.DetailFormEntityViewModel;
            AddContent();
            Visual = VisualMarker.Material;
            BindingContext = this.detailFormEntityViewModel;
        }

        private DetailFormEntityViewModelBase detailFormEntityViewModel;
        private Grid transitionGrid;
        private StackLayout page;

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (transitionGrid.IsVisible)
                await page.EntranceTransition(transitionGrid, 150);
        }

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
                        page = new StackLayout
                        {
                            Padding = new Thickness(30),
                            Children =
                            {
                                new Label
                                {
                                    Style = LayoutHelpers.GetStaticStyleResource("HeaderStyle")
                                }
                                .AddBinding
                                (
                                    Label.TextProperty,
                                    GetLabelBinding
                                    (
                                        detailFormEntityViewModel.FormSettings.HeaderBindings,
                                        $"{nameof(DetailFormEntityViewModelBase.FormSettings)}.{nameof(DataFormSettingsDescriptor.Title)}"
                                    )
                                ),
                                new Label
                                {
                                    IsVisible = detailFormEntityViewModel.FormSettings.FormType == FormType.Delete,
                                    Style = LayoutHelpers.GetStaticStyleResource("DetailFormDeleteQuestionStyle")
                                }
                                .AddBinding
                                (
                                    Label.TextProperty,
                                    GetLabelBinding
                                    (
                                        detailFormEntityViewModel.FormSettings.SubtitleBindings,
                                        $"{nameof(DetailFormEntityViewModelBase.FormSettings)}.{nameof(DataFormSettingsDescriptor.Title)}"
                                    )
                                ),
                                new ScrollView
                                {
                                    Content = detailFormEntityViewModel.FormLayout.ControlGroupBoxList.Aggregate
                                    (
                                        new StackLayout(),
                                        (stackLayout, controlBox) =>
                                        {
                                            if (controlBox.IsVisible == false)
                                                return stackLayout;

                                            stackLayout.Children.Add
                                            (
                                                new Label
                                                {
                                                    Style = LayoutHelpers.GetStaticStyleResource("DetailFormGroupHeaderStyle"),
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
                                                new StackLayout
                                                {
                                                    VerticalOptions = LayoutOptions.StartAndExpand,
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
                        transitionGrid = new Grid().AssignDynamicResource
                        (
                            VisualElement.BackgroundColorProperty,
                            "PageBackgroundColor"
                        )
                    )
                }
            };
        }
    }
}