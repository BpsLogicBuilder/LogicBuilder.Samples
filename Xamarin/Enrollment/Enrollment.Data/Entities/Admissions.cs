namespace Enrollment.Data.Entities
{
    public class Admissions : BaseDataClass
    {
        #region Properties
        public int UserId { get; set; }

        public virtual User User { get; set; }

        public string EnteringStatus { get; set; }

        public string EnrollmentTerm { get; set; }

        public string EnrollmentYear { get; set; }

        public string ProgramType { get; set; }

        public string Program { get; set; }
        #endregion Properties
    }
}
