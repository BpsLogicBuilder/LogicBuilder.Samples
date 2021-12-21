using AutoMapper;
using Contoso.Stores;
using LogicBuilder.EntityFrameworkCore.SqlServer.Repositories;

namespace Contoso.Repositories
{
    public class RulesRepository : ContextRepositoryBase, IRulesRepository
    {
        public RulesRepository(IRulesStore store, IMapper mapper) : base(store, mapper)
        {
        }
    }
}
