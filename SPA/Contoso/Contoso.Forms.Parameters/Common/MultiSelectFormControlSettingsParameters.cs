using LogicBuilder.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contoso.Forms.Parameters.Common
{
    public class MultiSelectFormControlSettingsParameters : FormControlSettingsParameters
    {
        public MultiSelectFormControlSettingsParameters()
        {
        }

        public MultiSelectFormControlSettingsParameters
        (
            [Comments("Usually just a list of one item - the primary key. Additional fields apply when the primary key is a composite key.")]
            List<string> keyFields,

            [Comments("Update modelType first. Source property name from the target object.")]
            [ParameterEditorControl(ParameterControlType.ParameterSourcedPropertyInput)]
            [NameValue(AttributeNames.PROPERTYSOURCEPARAMETER, "modelType")]
            string field,

            [Comments("ID attribute for the DOM element - usually (field)_id - also used on the label's for attribute.")]
            [NameValue(AttributeNames.DEFAULTVALUE, "(field)_id")]
            string domElementId,

            [Comments("Title")]
            [NameValue(AttributeNames.DEFAULTVALUE, "Title")]
            string title,

            [Comments("Place holder text.")]
            [NameValue(AttributeNames.DEFAULTVALUE, "(Title) required")]
            string placeHolder,

            [Comments("text/numeric/boolean/date")]
            [Domain("text,numeric,boolean,date")]
            [ParameterEditorControl(ParameterControlType.DropDown)]
            string type,

            [Comments("Defines the field's default value, validation functions (and arguments for the validator where necessary).")]
            FormValidationSettingParameters validationSetting = null,

            [Comments("HTML template applicable to multi-select elements.")]
            MultiSelectTemplateParameters multiSelectTemplate = null,

            [ParameterEditorControl(ParameterControlType.ParameterSourceOnly)]
            [NameValue(AttributeNames.DEFAULTVALUE, "Contoso.Domain.Entities")]
            [Comments("Fully qualified class name for the model type.")]
            string modelType = null
        ) : base
            (
                field, domElementId, title, placeHolder, type, validationSetting, null, null, modelType
            )
        {
            KeyFields = keyFields;
            MultiSelectTemplate = multiSelectTemplate;
        }

        public override AbstractControlEnum AbstractControlType { get => AbstractControlEnum.MultiSelectFormControl; }
        public List<string> KeyFields { get; set; }
        public MultiSelectTemplateParameters MultiSelectTemplate { get; set; }
    }
}
