using CheckMySymptoms.Flow.Requests;
using CheckMySymptoms.Flow.ScreenSettings.Views;
using CheckMySymptoms.Forms.View.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace CheckMySymptoms.Screens
{
    public sealed partial class Message : UserControl
    {
        private readonly ScreenSettings<MessageTemplateView> screenSettings;
        private readonly UiNotificationService uiNitificationService;
        private double TransitionOffset { get; set; }

        public Message(ScreenSettings<MessageTemplateView> screenSettings, UiNotificationService uiNitificationService, double transitionOffset)
        {
            this.InitializeComponent();
            this.TransitionOffset = transitionOffset;
            this.screenSettings = screenSettings;
            this.uiNitificationService = uiNitificationService;
        }


        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await uiNitificationService.Next(new DefaultRequest { CommandButtonRequest = new CommandButtonRequest { }, ViewType = ViewType.Message });
        }
    }
}
