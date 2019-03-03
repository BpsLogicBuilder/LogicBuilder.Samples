using CheckMySymptoms.Forms.View.Common;
using CheckMySymptoms.Forms.View.Json;
using LogicBuilder.RulesDirector;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CheckMySymptoms.Forms.View.Input
{
    [JsonConverter(typeof(InputViewConverter))]
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

        private IEnumerable<object> _itemsSource;
        public IEnumerable<object> ItemsSource
        {
            get
            {
                if (_itemsSource == null)
                {
                    if (this.DropDownTemplate == null)
                        return null;

                    //ILookUpsRepository repository = (Application.Current as App).ServiceProvider.GetRequiredService<ILookUpsRepository>();
                    ILookUpsRepository repository = new LookUpsRepository();
                    _itemsSource = System.Threading.Tasks.Task.Run(async () => await repository.GetByListId(this.DropDownTemplate.ListId)).Result;
                }

                return _itemsSource;
            }
        }

        //Validation
        //public string ValidatorFullName { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public bool HasErrors => Errors.Count > 0;
        public abstract void Validate(Dictionary<string, Dictionary<string, string>> validationMessages);
        public abstract InputResponse GetInputResponse();
        public string VariableId => VariableName.Replace('.', '_');

        public abstract void UpdateValue(object @value);
    }
}
