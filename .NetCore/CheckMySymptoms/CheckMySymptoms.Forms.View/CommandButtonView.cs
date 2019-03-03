using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace CheckMySymptoms.Forms.View
{
    public class CommandButtonView : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string ShortString { get; set; }
        public string LongString { get; set; }
        public bool Cancel { get; set; }
        public int? GridId { get; set; }
        public bool? GridCommandButton { get; set; }
        public bool? SelectFormButton { get; set; }
        public string ButtonIcon { get; set; }
        public string ClassString { get; set; }
        public string SymptomText { get; set; }
        public bool AddToSymptions { get; set; }

        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (value != _isSelected)
                {
                    _isSelected = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
