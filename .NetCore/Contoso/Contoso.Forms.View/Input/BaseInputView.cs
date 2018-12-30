using Contoso.Forms.View.Common;
using Contoso.Forms.View.Json;
using LogicBuilder.RulesDirector;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contoso.Forms.View.Input
{
    [JsonConverter(typeof(ViewConverter))]
    public abstract class BaseInputView
    {
        //LogicBuilder.Forms.Parameters
        public int Id { get; set; }
        public string VariableName { get; set; }
        public abstract string TypeString { get; }
        public abstract Type Type { get; }
        public abstract object Input { get; }

        //Data
        public string Text { get; set; }
        public string ClassAttribute { get; set; }
        public string ToolTipText { get; set; }
        public string Placeholder { get; set; }
        public string HtmlType { get; set; }
        public bool? ReadOnly { get; set; }
        public TextFieldTemplateView TextTemplate { get; set; }
        public DropDownTemplateView DropDownTemplate { get; set; }
        public MultiSelectTemplateView MultiSelectTemplate { get; set; }
        public List<DirectiveView> Directives { get; set; }
        public FormValidationSettingView ValidationSetting { get; set; }
        public FormValidationSettingView UnchangedValidationSetting => ValidationSetting;

        //Validation
        //public string ValidatorFullName { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public bool HasErrors => Errors.Count > 0;
        public abstract void Validate(Dictionary<string, Dictionary<string, string>> validationMessages);
        public abstract InputResponse GetInputResponse();
        public string VariableId => VariableName.Replace('.', '_');
    }
}
