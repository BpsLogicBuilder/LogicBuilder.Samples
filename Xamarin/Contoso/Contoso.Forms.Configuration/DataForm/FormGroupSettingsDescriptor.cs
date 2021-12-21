using Contoso.Forms.Configuration.Directives;
using Contoso.Forms.Configuration.Validation;
using System.Collections.Generic;

namespace Contoso.Forms.Configuration.DataForm
{
    public class FormGroupSettingsDescriptor : FormItemSettingsDescriptor, IChildFormGroupSettings
    {
        public override AbstractControlEnumDescriptor AbstractControlType => AbstractControlEnumDescriptor.FormGroup;
        public string Field { get; set; }
        public string Title { get; set; }
        public string ValidFormControlText { get; set; }
        public string InvalidFormControlText { get; set; }
        public string Placeholder { get; set; }
        public string ModelType { get; set; }
        public FormGroupTemplateDescriptor FormGroupTemplate { get; set; }
        public List<FormItemSettingsDescriptor> FieldSettings { get; set; }
        public Dictionary<string, List<ValidationRuleDescriptor>> ValidationMessages { get; set; }
        public Dictionary<string, List<DirectiveDescriptor>> ConditionalDirectives { get; set; }
        public MultiBindingDescriptor HeaderBindings { get; set; }
        public string GroupHeader => Title;
        public bool IsHidden => false;
    }
}
