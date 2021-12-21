using LogicBuilder.Attributes;
using LogicBuilder.Data;
using LogicBuilder.Domain;
using LogicBuilder.EntityFrameworkCore.SqlServer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Contoso.Bsl.Flow
{
    public static class PersistenceOperations<TModel, TData> where TModel : BaseModel where TData : BaseData
    {
        [AlsoKnownAs("Persistence_AddChange")]
        [FunctionGroup(FunctionGroup.Standard)]
        public static void AddChange(IContextRepository repository, TModel entity) 
            => repository.AddChange<TModel, TData>(entity);

        [AlsoKnownAs("Persistence_AddChanges")]
        [FunctionGroup(FunctionGroup.Standard)]
        public static void AddChanges(IContextRepository repository, ICollection<TModel> entities)
            => repository.AddChanges<TModel, TData>(entities);

        [AlsoKnownAs("Persistence_AddGraphChange")]
        [FunctionGroup(FunctionGroup.Standard)]
        public static void AddGraphChange(IContextRepository repository, TModel entity)
            => repository.AddGraphChange<TModel, TData>(entity);

        [AlsoKnownAs("Persistence_AddGraphChanges")]
        [FunctionGroup(FunctionGroup.Standard)]
        public static void AddGraphChanges(IContextRepository repository, ICollection<TModel> entities)
            => repository.AddGraphChanges<TModel, TData>(entities);

        [AlsoKnownAs("Persistence_Delete")]
        [FunctionGroup(FunctionGroup.Standard)]
        public static bool Delete(IContextRepository repository, Expression<Func<TModel, bool>> filter)
            => repository.DeleteAsync<TModel, TData>(filter).Result;

        [AlsoKnownAs("Persistence_Save")]
        [FunctionGroup(FunctionGroup.Standard)]
        public static bool Save(IContextRepository repository, TModel entity)
            => repository.SaveAsync<TModel, TData>(entity).Result;

        [AlsoKnownAs("Persistence_SaveList")]
        [FunctionGroup(FunctionGroup.Standard)]
        public static bool Save(IContextRepository repository, ICollection<TModel> entities)
            => repository.SaveAsync<TModel, TData>(entities).Result;

        [AlsoKnownAs("Persistence_SaveGraph")]
        [FunctionGroup(FunctionGroup.Standard)]
        public static bool SaveGraph(IContextRepository repository, TModel entity)
            => repository.SaveGraphAsync<TModel, TData>(entity).Result;

        [AlsoKnownAs("Persistence_SaveGraphList")]
        [FunctionGroup(FunctionGroup.Standard)]
        public static bool SaveGraphs(IContextRepository repository, ICollection<TModel> entities)
            => repository.SaveGraphsAsync<TModel, TData>(entities).Result;
    }
}
