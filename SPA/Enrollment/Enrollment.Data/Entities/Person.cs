using System;

namespace Enrollment.Data.Entities
{
    public class Person : BaseDataClass
    {
        public int ID { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}
