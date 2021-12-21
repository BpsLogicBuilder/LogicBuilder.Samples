using Enrollment.Forms.Configuration;
using Enrollment.Forms.Configuration.DataForm;
using Enrollment.XPlatform.Flow.Requests;
using Enrollment.XPlatform.Flow.Settings.Screen;
using Enrollment.XPlatform.Services;
using Enrollment.XPlatform.Utils;
using Enrollment.XPlatform.ViewModels.ReadOnlys;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Enrollment.XPlatform.ViewModels.DetailForm
{
    public abstract class DetailFormEntityViewModelBase : ViewModelBase
    {
        protected DetailFormEntityViewModelBase(ScreenSettings<DataFormSettingsDescriptor> screenSettings, IContextProvider contextProvider)
        {
            this.UiNotificationService = contextProvider.UiNotificationService;
            FormSettings = screenSettings.Settings;
            Buttons = new ObservableCollection<CommandButtonDescriptor>(screenSettings.CommandButtons);
        }

        public Dictionary<string, IReadOnly> BindingPropertiesDictionary
            => FormLayout.Properties.ToDictionary(p => p.Name.ToBindingDictionaryKey());

        public DataFormSettingsDescriptor FormSettings { get; set; }
        public DetailFormLayout FormLayout { get; set; }
        public UiNotificationService UiNotificationService { get; set; }
        public ObservableCollection<CommandButtonDescriptor> Buttons { get; set; }

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

        protected void Next(CommandButtonDescriptor button)
        {
            NavigateNext(button);
        }

        protected Task NavigateNext(CommandButtonDescriptor button)
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
