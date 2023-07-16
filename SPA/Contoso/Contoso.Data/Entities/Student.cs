using System;
using System.Collections.Generic;

namespace Contoso.Data.Entities
{
    public class Student : BaseDataClass
    {
        public int ID { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public DateTime EnrollmentDate { get; set; }


        public ICollection<Enrollment> Enrollments { get; set; }
    }

}
