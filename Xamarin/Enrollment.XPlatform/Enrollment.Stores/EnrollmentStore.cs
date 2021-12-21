using Enrollment.Contexts;
using LogicBuilder.EntityFrameworkCore.SqlServer.Crud.DataStores;

namespace Enrollment.Stores
{
    public class EnrollmentStore : StoreBase, IEnrollmentStore
    {
        public EnrollmentStore(EnrollmentContext context)
           : base(context)
        {
        }
    }
}
