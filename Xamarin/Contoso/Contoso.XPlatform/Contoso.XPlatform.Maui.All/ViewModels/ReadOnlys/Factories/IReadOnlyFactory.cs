using Contoso.Forms.Configuration;
using Contoso.Forms.Configuration.DataForm;
using System;
using System.Collections.Generic;

namespace Contoso.XPlatform.ViewModels.ReadOnlys.Factories
{
    public interface IReadOnlyFactory
    {
        IReadOnly CreateCheckboxReadOnlyObject(string name, string templateName, string checkboxLabel);
        IReadOnly CreateFormArrayReadOnlyObject(Type elementType, string name, FormGroupArraySettingsDescriptor setting);
        IReadOnly CreateFormReadOnlyObject(Type fieldType, string name, IChildFormGroupSettings setting);
        IReadOnly CreateHiddenReadOnlyObject(Type fieldType, string name, string templateName);
        IReadOnly CreateMultiSelectReadOnlyObject(Type elementType, string name, List<string> keyFields, string title, string stringFormat, MultiSelectTemplateDescriptor multiSelectTemplate);
        IReadOnly CreatePickerReadOnlyObject(Type fieldType, string name, string title, string stringFormat, DropDownTemplateDescriptor dropDownTemplate);
        IReadOnly CreateSwitchReadOnlyObject(string name, string templateName, string switchLabel);
        IReadOnly CreateTextFieldReadOnlyObject(Type fieldType, string name, string templateName, string title, string stringFormat);
    }
}
