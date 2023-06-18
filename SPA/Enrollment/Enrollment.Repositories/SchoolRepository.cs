using AutoMapper;
using Enrollment.Stores;
using LogicBuilder.EntityFrameworkCore.SqlServer.Repositories;

namespace Enrollment.Repositories
{
    public class SchoolRepository : ContextRepositoryBase, ISchoolRepository
    {
        public SchoolRepository(ISchoolStore store, IMapper mapper) : base(store, mapper)
        {
        }
    }
}
