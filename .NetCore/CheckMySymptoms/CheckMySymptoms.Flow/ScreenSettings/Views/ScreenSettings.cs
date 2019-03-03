using CheckMySymptoms.Forms.View;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CheckMySymptoms.Flow.ScreenSettings.Views
{
    public class ScreenSettings<TDialogSetting> : ScreenSettingsBase where TDialogSetting : ViewBase
    {
        public ScreenSettings(TDialogSetting settings, IEnumerable<CommandButtonView> commandButtons, ViewType viewType, MenuItem dialogListItem)
        {
            this.Settings = settings;
            this.CommandButtons = commandButtons;
            this.ViewType = viewType;
            this.DialogListItem = dialogListItem;
        }

        public ScreenSettings(TDialogSetting settings, IEnumerable<ValidationResult> errors, ViewType viewType)
        {
            this.Settings = settings;
            this.Errors = errors;
            this.ViewType = viewType;
        }

        public override ViewType ViewType { get; }
        public TDialogSetting Settings { get; set; }
        public override ViewBase View => Settings;
    }
}
