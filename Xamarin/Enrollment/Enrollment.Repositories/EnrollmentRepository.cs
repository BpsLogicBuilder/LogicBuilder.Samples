using AutoMapper;
using Enrollment.Stores;
using LogicBuilder.EntityFrameworkCore.SqlServer.Repositories;

namespace Enrollment.Repositories
{
    public class EnrollmentRepository : ContextRepositoryBase, IEnrollmentRepository
    {
        public EnrollmentRepository(IEnrollmentStore store, IMapper mapper) : base(store, mapper)
        {
        }
    }
}
