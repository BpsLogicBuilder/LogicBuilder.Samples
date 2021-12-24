using AutoMapper;
using Contoso.Data;
using Contoso.Domain;
using Contoso.Forms.Parameters;
using Contoso.Forms.Parameters.Common;
using Contoso.Repositories;
using LogicBuilder.Attributes;
using LogicBuilder.Expressions.EntityFrameworkCore;
using LogicBuilder.Expressions.Utils.DataSource;
using System.Linq;
using System.Threading.Tasks;

namespace Contoso.Web.Flow.Flow
{
    public static class CrudOperations<TModel, TData> where TModel : BaseModelClass, new() where TData : BaseDataClass
    {
        public static TModel GetModel
        (
            [Comments("Filter.")]
            FilterGroupParameters filterGroup,

            [Comments("Variable reference to the repository.")]
            ISchoolRepository repository,

            [Comments("Variable reference to AutoMapper's IMapper.")]
            IMapper mapper,

            [ListEditorControl(ListControlType.HashSetForm)]
            [ParameterEditorControl(ParameterControlType.ParameterSourcedPropertyInput)]
            [NameValue(AttributeNames.PROPERTYSOURCEPARAMETER, "modelType")]
            [Comments("Update the model type first for help with property names.")]
            string[] includes = null,

            [ParameterEditorControl(ParameterControlType.TypeAutoComplete)]
            [NameValue(AttributeNames.DEFAULTVALUE, "Contoso.Domain.Entities")]
            [Comments("Fully qualified class name for the model type.")]
            string modelType = null
        )
        {
            FilterGroup fg = mapper.Map<FilterGroup>(filterGroup);
            TModel model = repository.GetItemsAsync<TModel, TData>(fg.GetFilterExpression<TModel>(), null, includes.BuildIncludesExpressionCollection<TModel>()).Result.SingleOrDefault();
            return model ?? new TModel();
        }

        public static void SaveModel(FilterGroupParameters filterGroup, TModel model, ISchoolRepository repository, IMapper mapper)
        {
            FilterGroup fg = mapper.Map<FilterGroup>(filterGroup);
            TModel existing = Task.Run(async () => await repository.GetItemsAsync<TModel, TData>(fg.GetFilterExpression<TModel>())).Result.SingleOrDefault();

            model.EntityState = existing != null
                    ? LogicBuilder.Domain.EntityStateType.Modified
                    : LogicBuilder.Domain.EntityStateType.Added;

            repository.SaveGraphAsync<TModel, TData>(model).Wait();
        }
    }
}
