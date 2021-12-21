using Contoso.Forms.Configuration;
using Contoso.Forms.Configuration.ListForm;
using Contoso.XPlatform.Flow.Settings.Screen;
using System;
using System.Collections.ObjectModel;

namespace Contoso.XPlatform.ViewModels.ListPage
{
    public abstract class ListPageCollectionViewModelBase : ViewModelBase, IDisposable
    {
        protected ListPageCollectionViewModelBase(ScreenSettings<ListFormSettingsDescriptor> screenSettings)
        {
            FormSettings = screenSettings.Settings;
            Buttons = new ObservableCollection<CommandButtonDescriptor>(screenSettings.CommandButtons);
            Title = this.FormSettings.Title;
        }

        public ListFormSettingsDescriptor FormSettings { get; set; }
        public ObservableCollection<CommandButtonDescriptor> Buttons { get; set; }

        private string _title;
        public string Title
        {
            get => _title;
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
