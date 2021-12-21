using AutoMapper;
using Contoso.Forms.Configuration.DataForm;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.ViewModels.Validatables;
using LogicBuilder.Expressions.Utils;
using LogicBuilder.RulesDirector;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Contoso.XPlatform.Services
{
    public class PropertiesUpdater : IPropertiesUpdater
    {
        private readonly IMapper mapper;

        public PropertiesUpdater(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public void UpdateProperties(IEnumerable<IValidatable> properties, object entity, List<FormItemSettingsDescriptor> fieldSettings, string parentField = null) 
            => UpdateValidatables(properties, entity, fieldSettings, parentField);

        private void UpdateValidatables(IEnumerable<IValidatable> properties, object source, List<FormItemSettingsDescriptor> fieldSettings, string parentField = null)
        {
            IDictionary<string, object> existingValues = mapper.Map<Dictionary<string, object>>(source) ?? new Dictionary<string, object>();
            IDictionary<string, IValidatable> propertiesDictionary = properties.ToDictionary(p => p.Name);
            foreach (var setting in fieldSettings)
            {
                if (setting is MultiSelectFormControlSettingsDescriptor multiSelectFormControlSetting)
                {
                    if (existingValues.TryGetValue(multiSelectFormControlSetting.Field, out object @value) && @value != null)
                    {
                        IValidatable multiSelectValidatable = propertiesDictionary[GetFieldName(multiSelectFormControlSetting.Field)];

                        if (!multiSelectValidatable.Type.GetUnderlyingElementType().AssignableFrom(@value.GetType().GetUnderlyingElementType()))
                            throw new ArgumentException($"{nameof(multiSelectFormControlSetting)}: 74B8794A-9C00-4A25-8089-10957DF0A5EC");

                        multiSelectValidatable.Value = Activator.CreateInstance
                        (
                            multiSelectValidatable.Type,
                            new object[] { @value }
                        );
                    }
                }
                else if (setting is FormControlSettingsDescriptor controlSetting)
                {//must stay after MultiSelect because MultiSelect extends FormControl
                    if (existingValues.TryGetValue(controlSetting.Field, out object @value) && @value != null)
                    {
                        if (!propertiesDictionary[GetFieldName(controlSetting.Field)].Type.AssignableFrom(@value.GetType()))
                            throw new ArgumentException($"{nameof(controlSetting)}: F4B014E4-B04C-455D-8AE5-1F2551BEC190");

                        propertiesDictionary[GetFieldName(controlSetting.Field)].Value = @value;
                    }
                }
                else if (setting is FormGroupSettingsDescriptor formGroupSetting)
                {
                    if (existingValues.TryGetValue(formGroupSetting.Field, out object @value) && @value != null)
                    {
                        if (formGroupSetting.FormGroupTemplate == null)
                            throw new ArgumentException($"{nameof(formGroupSetting.FormGroupTemplate)}: 74E0697E-B5EF-4939-B0B4-8B7E4AE5544B");

                        if (formGroupSetting.FormGroupTemplate.TemplateName == FromGroupTemplateNames.InlineFormGroupTemplate)
                            UpdateValidatables(properties, @value, formGroupSetting.FieldSettings, GetFieldName(formGroupSetting.Field));
                        else if (formGroupSetting.FormGroupTemplate.TemplateName == FromGroupTemplateNames.PopupFormGroupTemplate)
                        {
                            if (!propertiesDictionary[GetFieldName(formGroupSetting.Field)].Type.AssignableFrom(@value.GetType()))
                                throw new ArgumentException($"{nameof(formGroupSetting)}: 83ADA1B9-5951-4643-BE40-E9BB6DB45C06");

                            propertiesDictionary[GetFieldName(formGroupSetting.Field)].Value = @value;
                        }
                        else
                            throw new ArgumentException($"{nameof(formGroupSetting.FormGroupTemplate.TemplateName)}: 5504FE49-2766-4D7C-916D-8FC633477DB1");
                    }
                }
                else if (setting is FormGroupArraySettingsDescriptor formGroupArraySetting)
                {
                    if (existingValues.TryGetValue(formGroupArraySetting.Field, out object @value) && @value != null)
                    {
                        IValidatable forArrayValidatable = propertiesDictionary[GetFieldName(formGroupArraySetting.Field)];

                        if (!forArrayValidatable.Type.GetUnderlyingElementType().AssignableFrom(@value.GetType().GetUnderlyingElementType()))
                            throw new ArgumentException($"{nameof(multiSelectFormControlSetting)}: CCB454D1-8119-475B-9A2B-1EB10E513959");

                        forArrayValidatable.Value = Activator.CreateInstance
                        (
                            forArrayValidatable.Type,
                            new object[] { @value }
                        );
                    }
                }
                else if (setting is FormGroupBoxSettingsDescriptor groupBoxSettingsDescriptor)
                {
                    UpdateValidatables(properties, source, groupBoxSettingsDescriptor.FieldSettings, parentField);
                }
            }

            string GetFieldName(string field)
                => string.IsNullOrEmpty(parentField)
                    ? field
                    : $"{parentField}.{field}";
        }
    }
}
