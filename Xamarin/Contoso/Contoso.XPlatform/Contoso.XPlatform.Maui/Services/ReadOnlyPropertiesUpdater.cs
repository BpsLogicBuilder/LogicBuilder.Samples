using AutoMapper;
using Contoso.Forms.Configuration.DataForm;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.ViewModels.ReadOnlys;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Contoso.XPlatform.Services
{
    public class ReadOnlyPropertiesUpdater : IReadOnlyPropertiesUpdater
    {
        private readonly IMapper mapper;

        public ReadOnlyPropertiesUpdater(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public void UpdateProperties(IEnumerable<IReadOnly> properties, object? entity, List<FormItemSettingsDescriptor> fieldSettings, string? parentField = null) 
            => UpdateReadOnlys(properties, entity, fieldSettings, parentField);

        private void UpdateReadOnlys(IEnumerable<IReadOnly> properties, object? source, List<FormItemSettingsDescriptor> fieldSettings, string? parentField = null)
        {
            IDictionary<string, object> existingValues = mapper.Map<Dictionary<string, object>>(source) ?? new Dictionary<string, object>();
            IDictionary<string, IReadOnly> propertiesDictionary = properties.ToDictionary(p => p.Name);
            foreach (var setting in fieldSettings)
            {
                if (setting is MultiSelectFormControlSettingsDescriptor multiSelectDetailControlSetting)
                {
                    if (existingValues.TryGetValue(multiSelectDetailControlSetting.Field, out object? @value) && @value != null)
                    {
                        propertiesDictionary[GetFieldName(multiSelectDetailControlSetting.Field)].Value = Activator.CreateInstance
                        (
                            typeof(ObservableCollection<>).MakeGenericType
                            (
                                Type.GetType(multiSelectDetailControlSetting.MultiSelectTemplate.ModelType) ?? throw new ArgumentException($"{multiSelectDetailControlSetting.MultiSelectTemplate.ModelType}: {{4AB21DE8-D32A-46F4-90C2-0E4E25F9E303}}")
                            ),
                            new object[] { @value }
                        ) ?? throw new ArgumentException($"{multiSelectDetailControlSetting.MultiSelectTemplate.ModelType}: {{BF676A94-25FE-429D-862B-4FE2633FFBC9}}");
                    }
                }
                else if (setting is FormControlSettingsDescriptor controlSetting)
                {//must stay after MultiSelect because MultiSelect extends FormControl
                    if (existingValues.TryGetValue(controlSetting.Field, out object? @value) && @value != null)
                        propertiesDictionary[GetFieldName(controlSetting.Field)].Value = @value;
                }
                else if (setting is FormGroupSettingsDescriptor formGroupSetting)
                {
                    if (existingValues.TryGetValue(formGroupSetting.Field, out object? @value) && @value != null)
                    {
                        if (formGroupSetting.FormGroupTemplate == null)
                            throw new ArgumentException($"{nameof(formGroupSetting.FormGroupTemplate)}: 85E77F0C-4913-48FE-B6AA-A9AC5D01BEFC");

                        if (formGroupSetting.FormGroupTemplate.TemplateName == FromGroupTemplateNames.InlineFormGroupTemplate)
                            UpdateReadOnlys(properties, @value, formGroupSetting.FieldSettings, GetFieldName(formGroupSetting.Field));
                        else if (formGroupSetting.FormGroupTemplate.TemplateName == FromGroupTemplateNames.PopupFormGroupTemplate)
                            propertiesDictionary[GetFieldName(formGroupSetting.Field)].Value = @value;
                        else
                            throw new ArgumentException($"{nameof(formGroupSetting.FormGroupTemplate.TemplateName)}: 6CCB1B68-6D5B-4417-9074-70A536872EFA");
                    }
                }
                else if (setting is FormGroupArraySettingsDescriptor formGroupArraySetting)
                {
                    if (existingValues.TryGetValue(formGroupArraySetting.Field, out object? @value) && @value != null)
                    {
                        propertiesDictionary[GetFieldName(formGroupArraySetting.Field)].Value = Activator.CreateInstance
                        (
                            typeof(ObservableCollection<>).MakeGenericType
                            (
                                Type.GetType(formGroupArraySetting.ModelType) ?? throw new ArgumentException($"{formGroupArraySetting.ModelType}: {{5901AF9C-4530-448D-B281-BC655263017A}}")
                            ),
                            new object[] { @value }
                        ) ?? throw new ArgumentException($"{formGroupArraySetting.ModelType}: {{AD348E36-C66C-4E5F-B4A3-A3AE9A236F34}}");
                    }
                }
                else if (setting is FormGroupBoxSettingsDescriptor groupBoxSettingsDescriptor)
                {
                    UpdateReadOnlys(properties, source, groupBoxSettingsDescriptor.FieldSettings, parentField);
                }
            }

            string GetFieldName(string field)
                => string.IsNullOrEmpty(parentField)
                    ? field
                    : $"{parentField}.{field}";
        }
    }
}
