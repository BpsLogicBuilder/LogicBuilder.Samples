using Enrollment.Forms.Configuration;
using Enrollment.Forms.Configuration.SearchForm;
using Enrollment.XPlatform.Flow.Settings.Screen;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace Enrollment.XPlatform.ViewModels.SearchPage
{
    public class SearchPageViewModelBase : ViewModelBase, IDisposable
    {
        protected SearchPageViewModelBase(ScreenSettings<SearchFormSettingsDescriptor> screenSettings)
        {
            FormSettings = screenSettings.Settings;
            Buttons = new ObservableCollection<CommandButtonDescriptor>(screenSettings.CommandButtons);
            /*MemberNotNull unvailable in 2.1*/
            _filterPlaceholder = null!;
            _title = null!;
            /*MemberNotNull unvailable in 2.1*/
            FilterPlaceholder = this.FormSettings.FilterPlaceholder;
            Title = this.FormSettings.Title;
        }

        public SearchFormSettingsDescriptor FormSettings { get; set; }
        public ObservableCollection<CommandButtonDescriptor> Buttons { get; set; }

        private string _filterPlaceholder;
        public string FilterPlaceholder
        {
            get => _filterPlaceholder;
            //[MemberNotNull(nameof(_filterPlaceholder))]
            set
            {
                if (_filterPlaceholder == value)
                    return;

                _filterPlaceholder = value;
                OnPropertyChanged();
            }
        }

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
