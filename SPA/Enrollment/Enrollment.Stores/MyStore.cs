using Enrollment.Contexts;
using LogicBuilder.EntityFrameworkCore.SqlServer.Crud.DataStores;

namespace Enrollment.Stores
{
    public class MyStore : StoreBase, IMyStore
    {
        public MyStore(MyContext context) : base(context)
        {
        }
    }
}
