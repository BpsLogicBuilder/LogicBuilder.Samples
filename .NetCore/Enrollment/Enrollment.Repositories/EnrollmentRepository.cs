using AutoMapper;
using Enrollment.Stores;
using LogicBuilder.EntityFrameworkCore.SqlServer.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Enrollment.Repositories
{
    public class EnrollmentRepository : ContextRepositoryBase, IEnrollmentRepository
    {
        public EnrollmentRepository(IEnrollmentStore store, IMapper mapper) : base(store, mapper)
        {
        }
    }
}
