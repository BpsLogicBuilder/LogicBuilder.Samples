using Contoso.Forms.Configuration.DataForm;
using Contoso.Forms.Configuration.Validation;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Contoso.XPlatform.Utils
{
    internal class UpdateOnlyFieldsCollectionHelper : FieldsCollectionHelper
    {
        public UpdateOnlyFieldsCollectionHelper(List<FormItemSettingsDescriptor> fieldSettings,
            IFormGroupBoxSettings groupBoxSettings,
            Dictionary<string, List<ValidationRuleDescriptor>> validationMessages,
            IContextProvider contextProvider,
            Type modelType,
            EditFormLayout formLayout = null,
            string parentName = null) : base(fieldSettings,
                groupBoxSettings,
                validationMessages,
                contextProvider,
                modelType,
                formLayout,
                parentName)
        {
        }

        protected override void AddFormControl(FormControlSettingsDescriptor setting)
        {
            if (setting.UpdateOnlyTextTemplate != null)
            {
                AddTextControl(setting, setting.UpdateOnlyTextTemplate);
            }
            else if (setting.TextTemplate != null)
                AddTextControl(setting, setting.TextTemplate);
            else if (setting.DropDownTemplate != null)
                AddDropdownControl(setting);
            else
                throw new ArgumentException($"{nameof(setting)}: 32652CB4-2574-4E5B-9B3F-7E47B37425AD");
        }

        protected override void AddGroupBoxSettings(FormGroupBoxSettingsDescriptor setting)
        {
            this.formLayout.AddControlGroupBox(setting);

            if (setting.FieldSettings.Any(s => s is FormGroupBoxSettingsDescriptor))
                throw new ArgumentException($"{nameof(setting.FieldSettings)}: 25BFA228-1B14-4B32-AA1C-8F8002DF4413");

            new UpdateOnlyFieldsCollectionHelper
            (
                setting.FieldSettings,
                setting,
                this.validationMessages,
                this.contextProvider,
                this.modelType,
                this.formLayout,
                this.parentName
            ).CreateFields();
        }

        protected override void AddFormGroupInline(FormGroupSettingsDescriptor setting) 
            => new UpdateOnlyFieldsCollectionHelper
            (
                setting.FieldSettings,
                this.groupBoxSettings,
                setting.ValidationMessages,
                this.contextProvider,
                this.modelType,
                this.formLayout,
                GetFieldName(setting.Field)
            ).CreateFields();
    }
}
