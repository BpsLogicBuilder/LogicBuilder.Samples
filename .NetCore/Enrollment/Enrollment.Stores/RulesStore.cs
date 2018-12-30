using Enrollment.Contexts;
using LogicBuilder.EntityFrameworkCore.SqlServer.Crud.DataStores;
using System;

namespace Enrollment.Stores
{
    public class RulesStore : StoreBase, IRulesStore
    {
        public RulesStore(RulesContext context)
            : base(context)
        {
        }
    }
}
