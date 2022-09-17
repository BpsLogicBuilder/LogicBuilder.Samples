using Contoso.Forms.Configuration.DataForm;
using Contoso.XPlatform.Validators;
using Contoso.XPlatform.ViewModels.ReadOnlys;
using Contoso.XPlatform.ViewModels.Validatables;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Contoso.XPlatform.ViewModels.Factories
{
    internal class DirectiveManagersFactory : IDirectiveManagersFactory
    {
        private readonly Func<Type, IEnumerable<IFormField>, object, IClearIfManager> _getClearIfManager;
        private readonly Func<Type, ObservableCollection<IValidatable>, IFormGroupSettings, IDirectiveManagers> _getDirectiveManagers;
        private readonly Func<Type, IEnumerable<IFormField>, object, IHideIfManager> _getHideIfManager;
        private readonly Func<Type, ObservableCollection<IReadOnly>, IFormGroupSettings, IReadOnlyDirectiveManagers> _getReadOnlyDirectiveManagers;
        private readonly Func<Type, IEnumerable<IFormField>, object, IReloadIfManager> _getReloadIfManager;
        private readonly Func<Type, IEnumerable<IFormField>, object, IValidateIfManager> _getValidateIfManager;

        public DirectiveManagersFactory(
            Func<Type, IEnumerable<IFormField>, object, IClearIfManager> getClearIfManager,
            Func<Type, ObservableCollection<IValidatable>, IFormGroupSettings, IDirectiveManagers> getDirectiveManagers,
            Func<Type, IEnumerable<IFormField>, object, IHideIfManager> getHideIfManager,
            Func<Type, ObservableCollection<IReadOnly>, IFormGroupSettings, IReadOnlyDirectiveManagers> getReadOnlyDirectiveManagers,
            Func<Type, IEnumerable<IFormField>, object, IReloadIfManager> getReloadIfManager,
            Func<Type, IEnumerable<IFormField>, object, IValidateIfManager> getValidateIfManager)
        {
            _getClearIfManager = getClearIfManager;
            _getHideIfManager = getHideIfManager;
            _getReloadIfManager = getReloadIfManager;
            _getValidateIfManager = getValidateIfManager;
            _getDirectiveManagers = getDirectiveManagers;
            _getReadOnlyDirectiveManagers = getReadOnlyDirectiveManagers;
        }

        public IClearIfManager GetClearIfManager<TModel>(IEnumerable<IFormField> currentProperties, List<ClearIf<TModel>> conditions) 
            => _getClearIfManager
            (
                typeof(TModel),
                currentProperties,
                conditions
            );

        public IDirectiveManagers GetDirectiveManagers(Type modelType, ObservableCollection<IValidatable> properties, IFormGroupSettings formSettings) 
            => _getDirectiveManagers
            (
                modelType,
                properties,
                formSettings
            );

        public IHideIfManager GetHideIfManager<TModel>(IEnumerable<IFormField> currentProperties, List<HideIf<TModel>> conditions)
            => _getHideIfManager
            (
                typeof(TModel),
                currentProperties,
                conditions
            );

        public IReadOnlyDirectiveManagers GetReadOnlyDirectiveManagers(Type modelType, ObservableCollection<IReadOnly> properties, IFormGroupSettings formSettings)
            => _getReadOnlyDirectiveManagers
            (
                modelType,
                properties,
                formSettings
            );

        public IReloadIfManager GetReloadIfManager<TModel>(IEnumerable<IFormField> currentProperties, List<ReloadIf<TModel>> conditions)
            => _getReloadIfManager
            (
                typeof(TModel),
                currentProperties,
                conditions
            );

        public IValidateIfManager GetValidateIfManager<TModel>(IEnumerable<IValidatable> currentProperties, List<ValidateIf<TModel>> conditions)
            => _getValidateIfManager
            (
                typeof(TModel),
                currentProperties,
                conditions
            );
    }
}
