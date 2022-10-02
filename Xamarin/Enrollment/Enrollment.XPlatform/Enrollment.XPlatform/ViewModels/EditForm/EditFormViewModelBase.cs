using Enrollment.Forms.Configuration;
using Enrollment.Forms.Configuration.DataForm;
using Enrollment.XPlatform.Flow.Requests;
using Enrollment.XPlatform.Flow.Settings.Screen;
using Enrollment.XPlatform.Services;
using Enrollment.XPlatform.Utils;
using Enrollment.XPlatform.ViewModels.Validatables;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Enrollment.XPlatform.ViewModels.EditForm
{
    public abstract class EditFormViewModelBase : ViewModelBase, IDisposable
    {
        protected EditFormViewModelBase(
            ScreenSettings<DataFormSettingsDescriptor> screenSettings,
            IUiNotificationService uiNotificationService)
        {
            this.UiNotificationService = uiNotificationService;
            FormSettings = screenSettings.Settings;
            Buttons = new ObservableCollection<CommandButtonDescriptor>(screenSettings.CommandButtons);
        }

        abstract public EditFormLayout FormLayout { get; set; }

        public Dictionary<string, IValidatable> BindingPropertiesDictionary
            => FormLayout.Properties.ToDictionary(p => p.Name.ToBindingDictionaryKey());

        public DataFormSettingsDescriptor FormSettings { get; set; }
        public IUiNotificationService UiNotificationService { get; set; }
        public ObservableCollection<CommandButtonDescriptor> Buttons { get; set; }

        private ICommand? _nextCommand;
        public ICommand NextCommand
        {
            get
            {
                if (_nextCommand != null)
                    return _nextCommand;

                _nextCommand = new Command<CommandButtonDescriptor>
                (
                     EditFormViewModelBase.Next
                );

                return _nextCommand;
            }
        }

        protected static void Next(CommandButtonDescriptor button)
        {
            EditFormViewModelBase.NavigateNext(button);
        }

        private static Task NavigateNext(CommandButtonDescriptor button)
        {
            using IScopedFlowManagerService flowManagerService = App.ServiceProvider.GetRequiredService<IScopedFlowManagerService>();
            flowManagerService.CopyFlowItems();
            return flowManagerService.Next
            (
                new CommandButtonRequest
                {
                    NewSelection = button.ShortString
                }
            );
        }

        public bool AreFieldsValid()
            => FormLayout.Properties.Aggregate
            (
                true,
                (isTrue, next) => next.Validate() && isTrue
            );

        public virtual void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
