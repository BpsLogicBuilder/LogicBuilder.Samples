using CheckMySymptoms.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace CheckMySymptoms.Flow.ScreenSettings
{
    public class MenuItem : ObservableObject
    {
        public int Stage { get; set; }
        public string Text { get; set; }
        public string Tooltip { get; set; }
        public string Icon { get; set; }
        private bool _last;

        public bool Last
        {
            get { return _last; }
            set
            {
                Set(ref _last, value);
                Enabled = !Last;
            }
        }

        public double FromHorizontalOffset => Last ? 1500 : 0;

        private bool _enabled;

        public bool Enabled
        {
            get { return _enabled; }
            set { Set(ref _enabled, value); }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj.GetType() != typeof(MenuItem))
                return false;

            MenuItem other = (MenuItem)obj;

            return other.Text == this.Text && other.Last == this.Last;
        }

        public override int GetHashCode()
            => (this.Text ?? string.Empty).GetHashCode();
    }
}
