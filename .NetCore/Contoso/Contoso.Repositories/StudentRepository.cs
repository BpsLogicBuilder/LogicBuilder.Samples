using AutoMapper;
using Contoso.Data.Entities;
using Contoso.Domain.Entities;
using Contoso.Stores;
using LogicBuilder.EntityFrameworkCore.SqlServer.Repositories;

namespace Contoso.Repositories
{
    public class StudentRepository : ModelRepositoryBase<StudentModel, Student>, IStudentRepository
    {
        public StudentRepository(ISchoolStore store, IMapper mapper) : base(store, mapper)
        {
        }
    }
}
