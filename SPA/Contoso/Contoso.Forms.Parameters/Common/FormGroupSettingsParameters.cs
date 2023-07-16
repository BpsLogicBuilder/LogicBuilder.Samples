using LogicBuilder.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Contoso.Forms.Parameters.Common
{
    public class FormGroupSettingsParameters : FormItemSettingParameters
    {
        public FormGroupSettingsParameters()
        {
        }

        public FormGroupSettingsParameters
        (
            [Comments("Update modelType first. Source property name from the target object.")]
            [ParameterEditorControl(ParameterControlType.ParameterSourcedPropertyInput)]
            [NameValue(AttributeNames.PROPERTYSOURCEPARAMETER, "modelType")]
            string field,

            [Comments("HTML template for the form group.")]
            FormGroupTemplateParameters formGroupTemplate,

            [Comments("Configuration for each field in the form group.")]
            List<FormItemSettingParameters> fieldSettings,

            [Comments("Input validation messages for each field.")]
            List<ValidationMessageParameters> validationMessages,

            [NameValue(AttributeNames.DEFAULTVALUE, "Title")]
            [Comments("Title for the form group.")]
            string title,

            [NameValue(AttributeNames.DEFAULTVALUE, "true")]
            bool showTitle,

            [Comments("Conditional directtives for each field.")]
            List<VariableDirectivesParameters> conditionalDirectives = null,

            [ParameterEditorControl(ParameterControlType.ParameterSourceOnly)]
            [NameValue(AttributeNames.DEFAULTVALUE, "Contoso.Domain.Entities")]
            [Comments("Fully qualified class name for the model type.")]
            string modelType = null
        )
        {
            Field = field;
            FormGroupTemplate = formGroupTemplate;
            FieldSettings = fieldSettings;
            ValidationMessages = validationMessages.ToDictionary(kvp => kvp.Field, kvp => kvp.Methods);
            Title = title;
            ShowTitle = showTitle;
            ConditionalDirectives = conditionalDirectives == null
                                            ? new Dictionary<string, List<DirectiveParameters>>()
                                            : conditionalDirectives
                                                .Select(cd => new VariableDirectivesParameters
                                                {
                                                    Field = cd.Field.Replace('.', '_'),
                                                    ConditionalDirectives = cd.ConditionalDirectives
                                                })
                                                .ToDictionary(kvp => kvp.Field, kvp => kvp.ConditionalDirectives);
            ModelType = modelType;
        }

        public override AbstractControlEnum AbstractControlType => AbstractControlEnum.FormGroup;
        public string Field { get; set; }
        public FormGroupTemplateParameters FormGroupTemplate { get; set; }
        public List<FormItemSettingParameters> FieldSettings { get; set; }
        public Dictionary<string, Dictionary<string, string>> ValidationMessages { get; set; }
        public Dictionary<string, List<DirectiveParameters>> ConditionalDirectives { get; set; }
        public string Title { get; set; }
        public bool ShowTitle { get; set; }
        public string ModelType { get; set; }//No harm in sending the model type
    }
}
