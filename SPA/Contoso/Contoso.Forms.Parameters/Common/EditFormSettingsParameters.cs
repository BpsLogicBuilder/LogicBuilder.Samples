using LogicBuilder.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Contoso.Forms.Parameters.Common
{
    public class EditFormSettingsParameters
    {
        public EditFormSettingsParameters()
        {
        }

        public EditFormSettingsParameters
        (
            [NameValue(AttributeNames.DEFAULTVALUE, "Title")]
            [Comments("Header field on the form")]
            string title,

            [ParameterEditorControl(ParameterControlType.ParameterSourcedPropertyInput)]
            [NameValue(AttributeNames.PROPERTYSOURCEPARAMETER, "modelType")]
            [Comments("Update modelType first. This field is displayed next to the title - empty on Create.")]
            string displayField,

            [Comments("Includes the URL's for create, read, update and delete.")]
            FormRequestDetailsParameters requestDetails,

            [Comments("Input validation messages for each field.")]
            List<ValidationMessageParameters> validationMessages,

            [Comments("List of fields and form groups for this form.")]
            List<FormItemSettingParameters> fieldSettings,

            [Comments("Conditional directtives for each field.")]
            List<VariableDirectivesParameters> conditionalDirectives = null,

            [ParameterEditorControl(ParameterControlType.ParameterSourceOnly)]
            [Comments("Assembly qualified class name for the model type.")]
            string modelType = "Contoso.Domain.Entities.XXXX , Contoso.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"
        )
        {
            Title = title;
            DisplayField = displayField;
            RequestDetails = requestDetails;
            ValidationMessages = validationMessages.ToDictionary(kvp => kvp.Field, kvp => kvp.Methods);
            FieldSettings = fieldSettings;
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

        public string Title { get; set; }
        public string DisplayField { get; set; }
        public FormRequestDetailsParameters RequestDetails { get; set; }
        public Dictionary<string, Dictionary<string, string>> ValidationMessages { get; set; }
        public List<FormItemSettingParameters> FieldSettings { get; set; }
        public Dictionary<string, List<DirectiveParameters>> ConditionalDirectives { get; set; }
        public string ModelType { get; set; }//Helps Json.Net on create
    }
}
