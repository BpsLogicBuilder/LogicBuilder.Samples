using Enrollment.Forms.Configuration;
using Enrollment.Forms.Configuration.ListForm;
using Enrollment.XPlatform.Flow.Settings.Screen;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace Enrollment.XPlatform.ViewModels.ListPage
{
    public abstract class ListPageViewModelBase : ViewModelBase, IDisposable
    {
        protected ListPageViewModelBase(ScreenSettings<ListFormSettingsDescriptor> screenSettings)
        {
            FormSettings = screenSettings.Settings;
            Buttons = new ObservableCollection<CommandButtonDescriptor>(screenSettings.CommandButtons);
            /*MemberNotNull unvailable in 2.1*/
            _title = null!;
            /*MemberNotNull unavailable in 2.1*/
            Title = this.FormSettings.Title;
        }

        public ListFormSettingsDescriptor FormSettings { get; set; }
        public ObservableCollection<CommandButtonDescriptor> Buttons { get; set; }

        private string _title;
        public string Title
        {
            get => _title;
            //[MemberNotNull(nameof(_title))]
            set
            {
                if (_title == value)
                    return;

                _title = value;
                OnPropertyChanged();
            }
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
