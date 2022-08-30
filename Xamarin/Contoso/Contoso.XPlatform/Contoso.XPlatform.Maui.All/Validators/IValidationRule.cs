using Contoso.XPlatform.ViewModels.Validatables;
using System.Collections.Generic;

namespace Contoso.XPlatform.Validators
{
    public interface IValidationRule
    {
        string FieldName { get; }
        string ClassName { get; }
        ICollection<IValidatable> AllProperties { get; set; }
        string ValidationMessage { get; set; }
        bool Check();
    }
}
