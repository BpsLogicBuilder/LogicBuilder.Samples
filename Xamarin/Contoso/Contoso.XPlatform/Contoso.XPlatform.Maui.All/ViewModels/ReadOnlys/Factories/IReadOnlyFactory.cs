using Contoso.Forms.Configuration.DataForm;
using System;

namespace Contoso.XPlatform.ViewModels.ReadOnlys.Factories
{
    public interface IReadOnlyFactory
    {
        IReadOnly CreateFormReadOnlyObject(Type fieldType, string name, IChildFormGroupSettings setting);
        IReadOnly CreateHiddenReadOnlyObject(Type fieldType, string name, string templateName);
    }
}
