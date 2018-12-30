using Enrollment.Contexts;
using LogicBuilder.EntityFrameworkCore.SqlServer.Crud.DataStores;
using System;
using System.Collections.Generic;
using System.Text;

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
