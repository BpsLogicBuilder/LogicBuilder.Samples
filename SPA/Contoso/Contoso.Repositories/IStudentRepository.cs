using Contoso.Data.Entities;
using Contoso.Domain.Entities;
using LogicBuilder.EntityFrameworkCore.SqlServer.Repositories;

namespace Contoso.Repositories
{
    public interface IStudentRepository : IModelRepository<StudentModel, Student>
    {
    }
}
