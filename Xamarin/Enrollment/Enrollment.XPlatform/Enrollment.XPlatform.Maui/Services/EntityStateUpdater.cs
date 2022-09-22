using AutoMapper;
using Enrollment.Forms.Configuration.DataForm;
using Enrollment.XPlatform.Utils;
using Enrollment.XPlatform.ViewModels.Validatables;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Enrollment.XPlatform.Services
{
    public class EntityStateUpdater : IEntityStateUpdater
    {
        private readonly IMapper mapper;

        public EntityStateUpdater(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public TModel GetUpdatedModel<TModel>(TModel? existingEntity, Dictionary<string, object?> existing, ObservableCollection<IValidatable> modifiedProperties, List<FormItemSettingsDescriptor> fieldSettings)
        {
            Dictionary<string, object?> current = modifiedProperties.ValidatableListToObjectDictionary
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
                typeof(Dictionary<string, object?>),
                typeof(TModel)
            );
        }
    }
}
