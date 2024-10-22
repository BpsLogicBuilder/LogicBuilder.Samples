﻿using Contoso.Forms.Configuration;
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
    public class TextPageViewModel : ViewModelBase, IDisposable
    {
        public TextPageViewModel(ScreenSettings<TextFormSettingsDescriptor> screenSettings)
        {
            FormSettings = screenSettings.Settings;
            Buttons = new ObservableCollection<CommandButtonDescriptor>(screenSettings.CommandButtons);
            /*MemberNotNull unvailable in 2.1*/
            _title = null!;
            /*MemberNotNull unvailable in 2.1*/
            Title = this.FormSettings.Title;
        }

        public TextFormSettingsDescriptor FormSettings { get; set; }
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
            GC.SuppressFinalize(this);
        }

        public static ICommand TapCommand => new Command<string>(async (url) => await Launcher.OpenAsync(url));

        private ICommand? _nextCommand;
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
            TextPageViewModel.NavigateNext(button);
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
    }
}
