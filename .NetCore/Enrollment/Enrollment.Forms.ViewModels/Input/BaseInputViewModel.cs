using Enrollment.Forms.ViewModels.Common;
using Enrollment.Forms.ViewModels.Json;
using LogicBuilder.RulesDirector;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Enrollment.Forms.ViewModels.Input
{
    [JsonConverter(typeof(ViewModelConverter))]
    public abstract class BaseInputViewModel
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
        public TextFieldTemplateViewModel TextTemplate { get; set; }
        public DropDownTemplateViewModel DropDownTemplate { get; set; }
        public MultiSelectTemplateViewModel MultiSelectTemplate { get; set; }
        public List<DirectiveViewModel> Directives { get; set; }
        public FormValidationSettingViewModel ValidationSetting { get; set; }
        public FormValidationSettingViewModel UnchangedValidationSetting => ValidationSetting;

        //Validation
        //public string ValidatorFullName { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public bool HasErrors => Errors.Count > 0;
        public abstract void Validate(Dictionary<string, Dictionary<string, string>> validationMessages);
        public abstract InputResponse GetInputResponse();
        public string VariableId => VariableName.Replace('.', '_');
    }
}
