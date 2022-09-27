using Enrollment.Forms.Configuration.TextForm;
using Enrollment.XPlatform.Constants;
using Enrollment.XPlatform.Utils;
using Enrollment.XPlatform.ViewModels.TextPage;
using System;
using Xamarin.Forms;

namespace Enrollment.XPlatform.Views
{
    public class TextPageViewCS : ContentPage
    {
        public TextPageViewCS(TextPageViewModel textPageViewModel)
        {
            this.TextPageScreenViewModel = textPageViewModel;
            /*MemberNotNull unvailable in 2.1*/
            transitionGrid = null!;
            page = null!;
            /*MemberNotNull unvailable in 2.1*/
            AddContent();
            Visual = VisualMarker.Material;
            BindingContext = this.TextPageScreenViewModel;
        }

        public TextPageViewModel TextPageScreenViewModel { get; set; }
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
            LayoutHelpers.AddToolBarItems(this.ToolbarItems, this.TextPageScreenViewModel.Buttons);
            Title = this.TextPageScreenViewModel.Title;
            Content = new Grid
            {
                Children =
                {
                    new ScrollView { Content = GetScrollViewContent() },
                    GetTransitionGrid()
                }
            };
        }

        //[MemberNotNull(nameof(transitionGrid))]
        private Grid GetTransitionGrid()
        {
            transitionGrid = new Grid
            {
                Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.TransitionGridStyle)
            };

            return transitionGrid;
        }

        //[MemberNotNull(nameof(page))]
        private View GetScrollViewContent()
        {
            page = new StackLayout
            {
                Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.TextPageStackLayoutStyle),
                Children =
                {
                    new Label
                    {
                        Text = this.TextPageScreenViewModel.Title,
                        Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.HeaderStyle)
                    }
                }
            };

            foreach (var group in TextPageScreenViewModel.FormSettings.TextGroups)
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
                Label formattedLabel = new()
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
                => new()
                {
                    Text = spanItemDescriptor.Text,
                    Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.TextFormHyperLinkSpanStyle),
                    GestureRecognizers =
                    {
                        new TapGestureRecognizer
                        {
                            CommandParameter = spanItemDescriptor.Url
                        }
                        .AddBinding(TapGestureRecognizer.CommandProperty, new Binding(path: nameof(TextPageViewModel.TapCommand)))
                    }
                };

            Span GetSpanItem(SpanItemDescriptor spanItemDescriptor)
                => new()
                {
                    Text = spanItemDescriptor.Text,
                    Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.TextFormItemSpanStyle)
                };


            Label GetHyperLinkLabelItem(HyperLinkLabelItemDescriptor labelItemDescriptor)
                => new()
                {
                    Text = labelItemDescriptor.Text,
                    Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.TextFormHyperLinkLabelStyle),
                    GestureRecognizers =
                    {
                        new TapGestureRecognizer
                        {
                            CommandParameter = labelItemDescriptor.Url
                        }
                        .AddBinding(TapGestureRecognizer.CommandProperty, new Binding(path: nameof(TextPageViewModel.TapCommand)))
                    }
                };

            Label GetLabelItem(LabelItemDescriptor labelItemDescriptor)
                => new()
                {
                    Text = labelItemDescriptor.Text,
                    Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.TextFormItemLabelStyle)
                };

            Label GetGroupHeader(TextGroupDescriptor textGroupDescriptor)
                => new()
                {
                    Text = textGroupDescriptor.Title,
                    Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.TextFormGroupHeaderStyle)
                };
        }
    }
}