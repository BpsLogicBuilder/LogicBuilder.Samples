using AutoMapper;
using Enrollment.Stores;
using LogicBuilder.EntityFrameworkCore.SqlServer.Repositories;

namespace Enrollment.Repositories
{
    public class MyRepository : ContextRepositoryBase, IMyRepository
    {
        public MyRepository(IMyStore store, IMapper mapper) : base(store, mapper)
        {
        }
    }
}
