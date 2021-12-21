using Contoso.Contexts;
using LogicBuilder.EntityFrameworkCore.SqlServer.Crud.DataStores;

namespace Contoso.Stores
{
    public class RulesStore : StoreBase, IRulesStore
    {
        public RulesStore(RulesContext context)
            : base(context)
        {
        }
    }
}
