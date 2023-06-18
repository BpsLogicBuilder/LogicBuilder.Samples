using Enrollment.Contexts;
using LogicBuilder.EntityFrameworkCore.SqlServer.Crud.DataStores;

namespace Enrollment.Stores
{
    public class SchoolStore : StoreBase, ISchoolStore
    {
        public SchoolStore(SchoolContext context) : base(context)
        {
        }
    }
}
