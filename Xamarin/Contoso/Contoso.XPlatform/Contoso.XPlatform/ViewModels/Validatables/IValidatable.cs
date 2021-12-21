using Contoso.XPlatform.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Contoso.XPlatform.ViewModels.Validatables
{
    public interface IValidatable : INotifyPropertyChanged, IFormField
    {
        string TemplateName { get; set; }
        bool IsValid { get; set; }
        bool IsDirty { get; set; }
        bool IsEnabled { get; set; }
        Type Type { get; }

        List<IValidationRule> Validations { get; }
        Dictionary<string, string> Errors { get; set; }

        bool Validate();
    }
}
