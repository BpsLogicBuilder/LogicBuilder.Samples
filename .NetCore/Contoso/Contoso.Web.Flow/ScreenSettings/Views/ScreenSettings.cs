using Contoso.Forms.View;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Contoso.Web.Flow.ScreenSettings.Views
{
    public class ScreenSettings<TDialogSetting> : ScreenSettingsBase
    {
        public ScreenSettings(TDialogSetting settings, IEnumerable<CommandButtonView> commandButtons, ViewType viewType)
        {
            Settings = settings;
            CommandButtons = commandButtons;
            this.ViewType = viewType;
        }

        public ScreenSettings(TDialogSetting settings, IEnumerable<ValidationResult> errors, ViewType viewType)
        {
            Settings = settings;
            Errors = errors;
            this.ViewType = viewType;
        }

        public override ViewType ViewType { get; }
        public TDialogSetting Settings { get; set; }
    }
}
