﻿using Contoso.Forms.Configuration;
using Contoso.Forms.Configuration.DataForm;
using Contoso.XPlatform.Flow.Requests;
using Contoso.XPlatform.Flow.Settings.Screen;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.ViewModels.Validatables;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace Contoso.XPlatform.ViewModels.EditForm
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
