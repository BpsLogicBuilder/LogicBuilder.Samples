using System;

namespace Enrollment.Data.Entities
{
    public class Institution : BaseDataClass
    {
        #region Properties
        public int InstitutionId { get; set; }

        public virtual Academic Academic { get; set; }

        public string InstitutionState { get; set; }

        public string InstitutionName { get; set; }

        public string StartYear { get; set; }

        public string EndYear { get; set; }

        public string HighestDegreeEarned { get; set; }

        public DateTime? MonthYearGraduated { get; set; }

        public int UserId { get; set; }
        #endregion Properties
    }
}
