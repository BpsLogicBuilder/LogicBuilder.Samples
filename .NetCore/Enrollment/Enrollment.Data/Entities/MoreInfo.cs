namespace Enrollment.Data.Entities
{
    public class MoreInfo : BaseDataClass
    {
        #region Properties
        public int UserId { get; set; }

        public virtual User User { get; set; }

        public string ReasonForAttending { get; set; }

        public string OverallEducationalGoal { get; set; }

        public bool IsVeteran { get; set; }

        public string MilitaryStatus { get; set; }

        public string MilitaryBranch { get; set; }

        public string VeteranType { get; set; }

        public string GovernmentBenefits { get; set; }
        #endregion Properties
    }
}
