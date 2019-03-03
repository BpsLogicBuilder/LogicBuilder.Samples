using CheckMySymptoms.Flow.ScreenSettings.Views;
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
    public sealed partial class Exception : UserControl
    {
        public Exception()
        {
            this.InitializeComponent();
        }

        public Exception(ScreenSettings<ExceptionView> screenSettings, double transitionOffset)
        {
            this.InitializeComponent();
            this.TransitionOffset = transitionOffset;
            ScreenSettings = screenSettings;
            Message = this.ScreenSettings.Settings.Message;
        }

        private ScreenSettings<ExceptionView> ScreenSettings { get; set; }
        private string Message { get; set; }
        private double TransitionOffset { get; set; }
    }
}
