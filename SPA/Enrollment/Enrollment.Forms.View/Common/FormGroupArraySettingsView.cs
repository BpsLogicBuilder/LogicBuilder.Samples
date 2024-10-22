﻿using System.Collections.Generic;

namespace Enrollment.Forms.View.Common
{
    public class FormGroupArraySettingsView : FormItemSettingView
    {
        public override AbstractControlEnum AbstractControlType { get; set; }
        public string Field { get; set; }
        public FormGroupTemplateView FormGroupTemplate { get; set; }
        public List<FormItemSettingView> FieldSettings { get; set; }
        public Dictionary<string, Dictionary<string, string>> ValidationMessages { get; set; }
        public List<string> KeyFields { get; set; }
        public Dictionary<string, List<DirectiveView>> ConditionalDirectives { get; set; }
        public string Title { get; set; }
        public bool ShowTitle { get; set; }
        public string ArrayElementType { get; set; }
    }
}