using System;
using System.Collections.Generic;
using System.Text;

namespace Contoso.Forms.Parameters.Common
{
    //FormGroupSettingsParameters, FormControlSettingsParameters,  MultiSelectFormControlSettingsParameters, FormGroupArraySettingsParameters
    public abstract class FormItemSettingParameters
    {
        abstract public AbstractControlEnum AbstractControlType { get; }
    }
}
