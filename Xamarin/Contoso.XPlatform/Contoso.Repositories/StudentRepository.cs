using AutoMapper;
using Contoso.Data.Entities;
using Contoso.Stores;
using Contoso.Domain.Entities;
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
