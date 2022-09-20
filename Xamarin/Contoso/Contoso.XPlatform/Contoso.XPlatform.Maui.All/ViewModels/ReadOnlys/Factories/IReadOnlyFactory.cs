using Contoso.Forms.Configuration.DataForm;
using System;

namespace Contoso.XPlatform.ViewModels.ReadOnlys.Factories
{
    public interface IReadOnlyFactory
    {
        IReadOnly CreateCheckboxReadOnlyObject(string name, string templateName, string checkboxLabel);
        IReadOnly CreateFormReadOnlyObject(Type fieldType, string name, IChildFormGroupSettings setting);
        IReadOnly CreateHiddenReadOnlyObject(Type fieldType, string name, string templateName);
        IReadOnly CreateSwitchReadOnlyObject(string name, string templateName, string switchLabel);
        IReadOnly CreateTextFieldReadOnlyObject(Type fieldType, string name, string templateName, string title, string stringFormat);
    }
}
