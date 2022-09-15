using Contoso.Forms.Configuration.DataForm;
using Contoso.Forms.Configuration.Validation;
using Contoso.XPlatform.ViewModels;
using Contoso.XPlatform.ViewModels.Factories;
using Contoso.XPlatform.ViewModels.Validatables;
using Contoso.XPlatform.ViewModels.Validatables.Factories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Contoso.XPlatform.Services
{
    public class UpdateOnlyFieldsCollectionBuilder : FieldsCollectionBuilder, IUpdateOnlyFieldsCollectionBuilder
    {
        public UpdateOnlyFieldsCollectionBuilder(
            ICollectionBuilderFactory collectionBuilderFactory,
            IContextProvider contextProvider,
            IValidatableFactory validatableFactory,
            IValidatableValueHelper validatableValueHelper,
            List<FormItemSettingsDescriptor> fieldSettings,
            IFormGroupBoxSettings groupBoxSettings,
            Dictionary<string, List<ValidationRuleDescriptor>> validationMessages,
            Type modelType,
            EditFormLayout? formLayout = null,
            string? parentName = null) : base(
                collectionBuilderFactory,
                contextProvider,
                validatableFactory,
                validatableValueHelper,
                fieldSettings,
                groupBoxSettings,
                validationMessages,
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

            collectionBuilderFactory.GetUpdateOnlyFieldsCollectionBuilder
            (
                this.modelType,
                setting.FieldSettings,
                setting,
                this.ValidationMessages,
                this.formLayout,
                 this.parentName
            ).CreateFields();
        }

        protected override void AddFormGroupInline(FormGroupSettingsDescriptor setting)
        {
            collectionBuilderFactory.GetUpdateOnlyFieldsCollectionBuilder
            (
                this.modelType,
                setting.FieldSettings,
                this.groupBoxSettings,
                setting.ValidationMessages,
                this.formLayout,
                GetFieldName(setting.Field)
            ).CreateFields();
        }
    }
}
