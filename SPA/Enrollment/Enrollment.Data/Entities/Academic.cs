using System;
using System.Collections.Generic;

namespace Enrollment.Data.Entities
{
    public class Academic : BaseDataClass
    {
        #region Properties
        public int UserId { get; set; }

        public virtual User User { get; set; }

        public string LastHighSchoolLocation { get; set; }

        public string NcHighSchoolName { get; set; }

        public string HomeSchoolType { get; set; }

        public string HomeSchoolAssociation { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public string GraduationStatus { get; set; }

        public bool? ReceivedGed { get; set; }
 
        public string GedLocation { get; set; }

        public DateTime? GedDate { get; set; }

        public bool EarnedCreditAtCmc { get; set; }

        public bool AttendedPriorColleges { get; set; }

        public ICollection<Institution> Institutions { get; set; }
        #endregion Properties
    }
}
