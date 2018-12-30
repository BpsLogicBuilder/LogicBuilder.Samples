using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Enrollment.Contexts.Configuations
{
    interface ITableConfiguration
    {
        void Configure(ModelBuilder modelBuilder);
    }
}
