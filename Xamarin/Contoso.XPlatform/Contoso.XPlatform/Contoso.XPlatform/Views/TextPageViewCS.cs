using Contoso.Forms.Configuration.TextForm;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.ViewModels;
using Contoso.XPlatform.ViewModels.TextPage;
using System;

using Xamarin.Forms;

namespace Contoso.XPlatform.Views
{
    public class TextPageViewCS : ContentPage
    {
        public TextPageViewCS(TextPageViewModel textPageViewModel)
        {
            this.textPageScreenViewModel = textPageViewModel.TextPageScreenViewModel;
            AddContent();
            Visual = VisualMarker.Material;
            BindingContext = this.textPageScreenViewModel;
        }

        public TextPageScreenViewModel textPageScreenViewModel { get; set; }
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
            LayoutHelpers.AddToolBarItems(this.ToolbarItems, this.textPageScreenViewModel.Buttons);
            Title = this.textPageScreenViewModel.Title;
            Content = new Grid
            {
                Children =
                {
                    new ScrollView { Content = GetScrollViewContent() },
                    GetTransitionGrid()
                }
            };
        }

        private Grid GetTransitionGrid()
        {
            transitionGrid = new Grid().AssignDynamicResource
            (
                VisualElement.BackgroundColorProperty,
                "PageBackgroundColor"
            );

            return transitionGrid;
        }

        private View GetScrollViewContent()
        {
            page = new StackLayout
            {
                Padding = new Thickness(30),
                Children =
                {
                    new Label
                    {
                        Text = this.textPageScreenViewModel.Title,
                        Style = LayoutHelpers.GetStaticStyleResource("HeaderStyle")
                    }
                }
            };

            foreach (var group in textPageScreenViewModel.FormSettings.TextGroups)
            {
                page.Children.Add(GetGroupHeader(group));
                foreach (LabelItemDescriptorBase item in group.Labels)
                {
                    switch (item)
                    {
                        case LabelItemDescriptor labelItemDescriptor:
                            page.Children.Add(GetLabelItem(labelItemDescriptor));
                            break;
                        case HyperLinkLabelItemDescriptor hyperLinkLabelItemDescriptor:
                            page.Children.Add(GetHyperLinkLabelItem(hyperLinkLabelItemDescriptor));
                            break;
                        case FormattedLabelItemDescriptor formattedItemDescriptor:
                            page.Children.Add(GetFornattedLabelItem(formattedItemDescriptor));
                            break;
                        default:
                            throw new ArgumentException($"{nameof(item)}: 615C881A-3EA5-4681-AD72-482E055E728E");
                    }
                }
            }

            return page;

            Label GetFornattedLabelItem(FormattedLabelItemDescriptor formattedItemDescriptor)
            {
                Label formattedLabel = new Label
                {
                    FormattedText = new FormattedString
                    {
                        Spans = { }
                    }
                };

                foreach (SpanItemDescriptorBase item in formattedItemDescriptor.Items)
                {
                    switch (item)
                    {
                        case SpanItemDescriptor spanItemDescriptor:
                            formattedLabel.FormattedText.Spans.Add(GetSpanItem(spanItemDescriptor));
                            break;
                        case HyperLinkSpanItemDescriptor hyperLinkSpanItemDescriptor:
                            formattedLabel.FormattedText.Spans.Add(GetHyperLinkSpanItem(hyperLinkSpanItemDescriptor));
                            break;
                        default:
                            throw new ArgumentException($"{nameof(item)}: BD90BDA3-31E9-4FCC-999E-2486CB527E30");
                    }
                }

                return formattedLabel;
            }

            Span GetHyperLinkSpanItem(HyperLinkSpanItemDescriptor spanItemDescriptor)
                => new Span
                {
                    Text = spanItemDescriptor.Text,
                    Style = LayoutHelpers.GetStaticStyleResource("TextFormHyperLinkSpanStyle"),
                    GestureRecognizers =
                    {
                        new TapGestureRecognizer
                        {
                            CommandParameter = spanItemDescriptor.Url
                        }
                        .AddBinding(TapGestureRecognizer.CommandProperty, new Binding(path: "TapCommand"))
                    }
                };

            Span GetSpanItem(SpanItemDescriptor spanItemDescriptor)
                => new Span
                {
                    Text = spanItemDescriptor.Text,
                    Style = LayoutHelpers.GetStaticStyleResource("TextFormItemSpanStyle")
                };


            Label GetHyperLinkLabelItem(HyperLinkLabelItemDescriptor labelItemDescriptor)
                => new Label
                {
                    Text = labelItemDescriptor.Text,
                    Style = LayoutHelpers.GetStaticStyleResource("TextFormHyperLinkLabelStyle"),
                    GestureRecognizers =
                    {
                        new TapGestureRecognizer
                        {
                            CommandParameter = labelItemDescriptor.Url
                        }
                        .AddBinding(TapGestureRecognizer.CommandProperty, new Binding(path: "TapCommand"))
                    }
                };

            Label GetLabelItem(LabelItemDescriptor labelItemDescriptor)
                => new Label
                {
                    Text = labelItemDescriptor.Text,
                    Style = LayoutHelpers.GetStaticStyleResource("TextFormItemLabelStyle")
                };

            Label GetGroupHeader(TextGroupDescriptor textGroupDescriptor)
                => new Label
                {
                    Text = textGroupDescriptor.Title,
                    Style = LayoutHelpers.GetStaticStyleResource("TextFormGroupHeaderStyle")
                };
        }
    }
}