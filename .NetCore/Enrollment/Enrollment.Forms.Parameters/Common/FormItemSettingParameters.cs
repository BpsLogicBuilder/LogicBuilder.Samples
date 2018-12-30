using System;
using System.Collections.Generic;
using System.Text;

namespace Enrollment.Forms.Parameters.Common
{
    //FormGroupSettingsParameters, FormControlSettingsParameters,  MultiSelectFormControlSettingsParameters, FormGroupArraySettingsParameters
    public abstract class FormItemSettingParameters
    {
        abstract public AbstractControlEnum AbstractControlType { get; }
    }
}
