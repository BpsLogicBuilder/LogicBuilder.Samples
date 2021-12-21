using AutoMapper;
using Contoso.Forms.Configuration.DataForm;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.ViewModels.Validatables;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Contoso.XPlatform.Services
{
    public class EntityStateUpdater : IEntityStateUpdater
    {
        private readonly IMapper mapper;

        public EntityStateUpdater(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public TModel GetUpdatedModel<TModel>(TModel existingEntity, Dictionary<string, object> existing, ObservableCollection<IValidatable> modifiedProperties, List<FormItemSettingsDescriptor> fieldSettings)
        {
            Dictionary<string, object> current = modifiedProperties.ValidatableListToObjectDictionary
            (
                mapper,
                fieldSettings
            );

            EntityMapper.UpdateEntityStates
            (
                existing,
                current,
                fieldSettings
            );

            if (existingEntity == null)
            {
                return mapper.Map<TModel>(current);
            }

            return (TModel)mapper.Map
            (
                current,
                existingEntity,
                typeof(Dictionary<string, object>),
                typeof(TModel)
            );
        }
    }
}
