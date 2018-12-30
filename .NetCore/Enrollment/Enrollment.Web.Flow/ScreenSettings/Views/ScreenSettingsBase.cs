using Enrollment.Forms.View;
using Enrollment.Web.Flow.ScreenSettings.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Enrollment.Web.Flow.ScreenSettings.Views
{
    [JsonConverter(typeof(DialogSettingsConverter))]
    abstract public class ScreenSettingsBase
    {
        abstract public ViewType ViewType { get; }
        public IEnumerable<CommandButtonView> CommandButtons { get; set; }
        public IEnumerable<ValidationResult> Errors { get; set; }
    }
}
