using Contoso.Forms.Configuration.DataForm;
using Contoso.XPlatform.Validators;
using Contoso.XPlatform.ViewModels.ReadOnlys;
using Contoso.XPlatform.ViewModels.Validatables;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Contoso.XPlatform.ViewModels.Factories
{
    public interface IDirectiveManagersFactory
    {
        IClearIfManager GetClearIfManager<TModel>(IEnumerable<IFormField> currentProperties, List<ClearIf<TModel>> conditions);
        IDirectiveManagers GetDirectiveManagers(Type modelType, ObservableCollection<IValidatable> properties, IFormGroupSettings formSettings);
        IHideIfManager GetHideIfManager<TModel>(IEnumerable<IFormField> currentProperties, List<HideIf<TModel>> conditions);
        IReadOnlyDirectiveManagers GetReadOnlyDirectiveManagers(Type modelType, ObservableCollection<IReadOnly> properties, IFormGroupSettings formSettings);
        IReloadIfManager GetReloadIfManager<TModel>(IEnumerable<IFormField> currentProperties, List<ReloadIf<TModel>> conditions);
        IValidateIfManager GetValidateIfManager<TModel>(IEnumerable<IValidatable> currentProperties, List<ValidateIf<TModel>> conditions);
    }
}
