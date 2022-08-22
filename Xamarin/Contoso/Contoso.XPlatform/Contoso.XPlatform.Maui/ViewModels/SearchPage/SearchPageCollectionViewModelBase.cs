using Contoso.Forms.Configuration;
using Contoso.Forms.Configuration.SearchForm;
using Contoso.XPlatform.Flow.Settings.Screen;
using Microsoft.Maui;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace Contoso.XPlatform.ViewModels.SearchPage
{
    public class SearchPageCollectionViewModelBase : ViewModelBase, IDisposable
    {
        protected SearchPageCollectionViewModelBase(ScreenSettings<SearchFormSettingsDescriptor> screenSettings)
        {
            FormSettings = screenSettings.Settings;
            Buttons = new ObservableCollection<CommandButtonDescriptor>(screenSettings.CommandButtons);
            FilterPlaceholder = this.FormSettings.FilterPlaceholder;
            Title = this.FormSettings.Title;
        }

        public SearchFormSettingsDescriptor FormSettings { get; set; }
        public ObservableCollection<CommandButtonDescriptor> Buttons { get; set; }

        private string _filterPlaceholder;
        public string FilterPlaceholder
        {
            get => _filterPlaceholder;
            [MemberNotNull(nameof(_filterPlaceholder))]
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
            [MemberNotNull(nameof(_title))]
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
