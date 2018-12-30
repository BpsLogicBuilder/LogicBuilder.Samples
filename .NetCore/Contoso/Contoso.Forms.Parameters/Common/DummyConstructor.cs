using System;
using System.Collections.Generic;
using System.Text;

namespace Contoso.Forms.Parameters.Common
{
    public class DummyConstructor
    {
        public DummyConstructor
        (
            DetailFormSettingsParameters form,
            DetailFieldSettingParameters field,
            DetailGroupSettingsParameters group,
            DetailListSettingsParameters list,
            FormControlSettingsParameters formControlSettings,
            FormGroupArraySettingsParameters formGroupArraySettings,
            FormGroupSettingsParameters formGroupSettings,
            MultiSelectFormControlSettingsParameters multiSelectFormControlSettings,
            CommandButtonParameters commandButtonData,
            ValidatorArgumentParameters validatorArguments
        )
        {
        }
    }
}
