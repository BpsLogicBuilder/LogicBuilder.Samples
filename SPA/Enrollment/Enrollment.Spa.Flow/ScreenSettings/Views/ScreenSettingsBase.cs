using Enrollment.Forms.View;
using Enrollment.Spa.Flow.ScreenSettings.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Enrollment.Spa.Flow.ScreenSettings.Views
{
    [JsonConverter(typeof(ScreenSettingsConverter))]
    abstract public class ScreenSettingsBase
    {
        abstract public ViewType ViewType { get; }
        public IEnumerable<CommandButtonView>? CommandButtons { get; set; }
        public IEnumerable<ValidationResult>? Errors { get; set; }
        public string TypeFullName => this.GetType().AssemblyQualifiedName ?? throw new ArgumentException($"{this.GetType().Name}: {{75FE4EA4-09BF-40C2-A750-50E46A801147}}");
    }
}
