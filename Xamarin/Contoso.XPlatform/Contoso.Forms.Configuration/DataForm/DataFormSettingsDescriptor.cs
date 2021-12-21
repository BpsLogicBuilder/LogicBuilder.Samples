using Contoso.Common.Configuration.ItemFilter;
using Contoso.Forms.Configuration.Directives;
using Contoso.Forms.Configuration.Validation;
using System.Collections.Generic;

namespace Contoso.Forms.Configuration.DataForm
{
    public class DataFormSettingsDescriptor : IFormGroupSettings
    {
        public string Title { get; set; }
        public FormRequestDetailsDescriptor RequestDetails { get; set; }
        public Dictionary<string, List<ValidationRuleDescriptor>> ValidationMessages { get; set; }
        public List<FormItemSettingsDescriptor> FieldSettings { get; set; }
        public FormType FormType { get; set; }
        public string ModelType { get; set; }
        public Dictionary<string, List<DirectiveDescriptor>> ConditionalDirectives { get; set; }
        public MultiBindingDescriptor HeaderBindings { get; set; }
        public MultiBindingDescriptor SubtitleBindings { get; set; }
        public ItemFilterGroupDescriptor ItemFilterGroup { get; set; }
        public string GroupHeader => Title;
        public bool IsHidden => false;
    }
}
