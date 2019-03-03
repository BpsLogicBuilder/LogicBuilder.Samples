using CheckMySymptoms.Forms.View;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CheckMySymptoms.Flow.ScreenSettings.Views
{
    abstract public class ScreenSettingsBase
    {
        abstract public ViewType ViewType { get; }
        public IEnumerable<CommandButtonView> CommandButtons { get; set; }
        public IEnumerable<ValidationResult> Errors { get; set; }
        public MenuItem DialogListItem { get; set; }
        public abstract ViewBase View { get; }
    }
}
