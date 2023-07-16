using LogicBuilder.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enrollment.Forms.Parameters.Common
{
    public class FormGroupArraySettingsParameters : FormItemSettingParameters
    {
        public FormGroupArraySettingsParameters()
        {
        }

        public FormGroupArraySettingsParameters
        (
            [Comments("Update modelType first. Source property name from the target object.")]
            [ParameterEditorControl(ParameterControlType.ParameterSourcedPropertyInput)]
            [NameValue(AttributeNames.PROPERTYSOURCEPARAMETER, "modelType")]
            string field,

            [Comments("HTML template for the form group.")]
            FormGroupTemplateParameters formGroupTemplate,

            [Comments("Configuration for each field in one of the array's form groups.")]
            List<FormItemSettingParameters> fieldSettings,

            [Comments("Usually just a list of one item - the primary key. Additional fields apply when the primary key is a composite key.")]
            List<string> keyFields,

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
            [NameValue(AttributeNames.DEFAULTVALUE, "Enrollment.Domain.Entities")]
            [Comments("Fully qualified class name for the model type.")]
            string modelType = null,

            [ParameterEditorControl(ParameterControlType.ParameterSourceOnly)]
            [Comments("Fully qualified class name for the model type.")]
            string arrayElementType = "Enrollment.Domain.Entities"
        )
        {
            Field = field;
            FormGroupTemplate = formGroupTemplate;
            FieldSettings = fieldSettings;
            KeyFields = keyFields;
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
            ArrayElementType = arrayElementType;
        }

        public override AbstractControlEnum AbstractControlType => AbstractControlEnum.FormGroupArray;
        public string Field { get; set; }
        public FormGroupTemplateParameters FormGroupTemplate { get; set; }
        public List<FormItemSettingParameters> FieldSettings { get; set; }
        public Dictionary<string, Dictionary<string, string>> ValidationMessages { get; set; }
        public List<string> KeyFields { get; set; }
        public Dictionary<string, List<DirectiveParameters>> ConditionalDirectives { get; set; }
        public string Title { get; set; }
        public bool ShowTitle { get; set; }
        public string ArrayElementType { get; set; }
    }
}
