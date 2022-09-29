﻿using Enrollment.Forms.Configuration.DataForm;
using Enrollment.XPlatform.Services;
using Enrollment.XPlatform.ViewModels;
using Enrollment.XPlatform.ViewModels.ReadOnlys;
using Enrollment.XPlatform.ViewModels.Validatables;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Enrollment.XPlatform.Directives.Factories
{
    public interface IDirectiveManagersFactory
    {
        IClearIfManager GetClearIfManager<TModel>(IEnumerable<IFormField> currentProperties, List<ClearIf<TModel>> conditions);
        IHideIfManager GetHideIfManager<TModel>(IEnumerable<IFormField> currentProperties, List<HideIf<TModel>> conditions);
        IReloadIfManager GetReloadIfManager<TModel>(IEnumerable<IFormField> currentProperties, List<ReloadIf<TModel>> conditions);
        IValidateIfManager GetValidateIfManager<TModel>(IEnumerable<IValidatable> currentProperties, List<ValidateIf<TModel>> conditions);

        IConditionalDirectiveBuilder<TConditionBase, TModel> GetDirectiveConditionsBuilder<TConditionBase, TModel>(IFormGroupSettings formGroupSettings, IEnumerable<IFormField> properties, List<TConditionBase>? parentList = null, string? parentName = null) where TConditionBase : ConditionBase<TModel>, new();

        IDirectiveManagers GetDirectiveManagers(Type modelType, ObservableCollection<IValidatable> properties, IFormGroupSettings formSettings);
        IReadOnlyDirectiveManagers GetReadOnlyDirectiveManagers(Type modelType, ObservableCollection<IReadOnly> properties, IFormGroupSettings formSettings);
    }
}