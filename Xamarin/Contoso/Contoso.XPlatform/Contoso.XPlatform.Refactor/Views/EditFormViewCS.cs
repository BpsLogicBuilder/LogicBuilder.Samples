using Contoso.Forms.Configuration;
using Contoso.Forms.Configuration.DataForm;
using Contoso.XPlatform.Constants;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.ViewModels;
using Contoso.XPlatform.ViewModels.EditForm;
using Contoso.XPlatform.ViewModels.Validatables;
using System.Linq;
using Xamarin.Forms;

namespace Contoso.XPlatform.Views
{
    public class EditFormViewCS : ContentPage
    {
        public EditFormViewCS(EditFormViewModelBase editFormViewModel)
        {
            this.editFormEntityViewModel = editFormViewModel;
            /*MemberNotNull unvailable in 2.1*/
            transitionGrid = null!;
            page = null!;
            /*MemberNotNull unvailable in 2.1*/
            AddContent();
            Visual = VisualMarker.Material;
            BindingContext = this.editFormEntityViewModel;
        }

        private readonly EditFormViewModelBase editFormEntityViewModel;
        private Grid transitionGrid;
        private StackLayout page;

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (transitionGrid.IsVisible)
                await page.EntranceTransition(transitionGrid, 150);
        }

        //[MemberNotNull(nameof(transitionGrid), nameof(page))]
        private void AddContent()
        {
            LayoutHelpers.AddToolBarItems(this.ToolbarItems, this.editFormEntityViewModel.Buttons);
            Title = editFormEntityViewModel.FormSettings.Title;

            BindingBase GetHeaderBinding(MultiBindingDescriptor multiBindingDescriptor, string bindingName)
            {
                if (multiBindingDescriptor == null)
                    return new Binding(bindingName);

                return new MultiBinding
                {
                    StringFormat = multiBindingDescriptor.StringFormat,
                    Bindings = multiBindingDescriptor.Fields.Select
                    (
                        field => new Binding($"{nameof(ControlGroupBox.BindingPropertiesDictionary)}[{field.ToBindingDictionaryKey()}].{nameof(IValidatable.Value)}")
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
                            Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.EditFormStackLayoutStyle),
                            Children =
                            {
                                new Label
                                {
                                    Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.HeaderStyle)
                                }
                                .AddBinding
                                (
                                    Label.TextProperty,
                                    GetHeaderBinding
                                    (
                                        editFormEntityViewModel.FormSettings.HeaderBindings,
                                        $"{nameof(EditFormViewModelBase.FormSettings)}.{nameof(DataFormSettingsDescriptor.Title)}"
                                    )
                                ),
                                new ScrollView
                                {
                                    Content = editFormEntityViewModel.FormLayout.ControlGroupBoxList.Aggregate
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
                                                    Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.EditFormGroupHeaderStyle),
                                                    BindingContext = controlBox
                                                }
                                                .AddBinding
                                                (
                                                    Label.TextProperty,
                                                    GetHeaderBinding(controlBox.HeaderBindings, $"{nameof(ControlGroupBox.GroupHeader)}")
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
                                                .SetDataTemplateSelector(EditFormViewHelpers.QuestionTemplateSelector)
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