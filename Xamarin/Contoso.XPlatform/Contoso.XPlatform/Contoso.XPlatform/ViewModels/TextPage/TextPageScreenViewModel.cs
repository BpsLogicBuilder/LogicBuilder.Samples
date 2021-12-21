using Contoso.Forms.Configuration;
using Contoso.Forms.Configuration.TextForm;
using Contoso.XPlatform.Flow.Requests;
using Contoso.XPlatform.Flow.Settings.Screen;
using Contoso.XPlatform.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Contoso.XPlatform.ViewModels.TextPage
{
    public class TextPageScreenViewModel : ViewModelBase, IDisposable
    {
        public TextPageScreenViewModel(ScreenSettings<TextFormSettingsDescriptor> screenSettings, UiNotificationService uiNotificationService)
        {
            FormSettings = screenSettings.Settings;
            this.uiNotificationService = uiNotificationService;
            Buttons = new ObservableCollection<CommandButtonDescriptor>(screenSettings.CommandButtons);
            Title = this.FormSettings.Title;
        }

        private readonly UiNotificationService uiNotificationService;
        public TextFormSettingsDescriptor FormSettings { get; set; }
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

        public ICommand TapCommand => new Command<string>(async (url) => await Launcher.OpenAsync(url));

        private ICommand _nextCommand;
        public ICommand NextCommand
        {
            get
            {
                if (_nextCommand != null)
                    return _nextCommand;

                _nextCommand = new Command<CommandButtonDescriptor>
                (
                     Next
                );

                return _nextCommand;
            }
        }

        private void Next(CommandButtonDescriptor button)
        {
            NavigateNext(button);
        }

        private Task NavigateNext(CommandButtonDescriptor button)
        {
            using (IScopedFlowManagerService flowManagerService = App.ServiceProvider.GetRequiredService<IScopedFlowManagerService>())
            {
                flowManagerService.CopyFlowItems();

                return flowManagerService.Next
                (
                    new CommandButtonRequest
                    {
                        NewSelection = button.ShortString
                    }
                );
            }
        }
    }
}
