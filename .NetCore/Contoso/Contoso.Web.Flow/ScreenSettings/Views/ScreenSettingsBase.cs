using Contoso.Forms.View;
using Contoso.Web.Flow.ScreenSettings.Json;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Contoso.Web.Flow.ScreenSettings.Views
{
    [JsonConverter(typeof(DialogSettingsConverter))]
    abstract public class ScreenSettingsBase
    {
        abstract public ViewType ViewType { get; }
        public IEnumerable<CommandButtonView> CommandButtons { get; set; }
        public IEnumerable<ValidationResult> Errors { get; set; }
    }
}
