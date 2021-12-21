using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Enrollment.Data.Entities
{
    public class MoreInfo : BaseDataClass
    {
        #region Properties
        [Key, ForeignKey("User")]
        public int UserId { get; set; }

        public virtual User User { get; set; }


        [Display(Name ="What is your most important reason for attending?")]
        [Required()]
        [StringLength(4)]
        public string ReasonForAttending { get; set; }

        [Display(Name ="What is your overall educational goal?")]
        [Required()]
        [StringLength(4)]
        public string OverallEducationalGoal { get; set; }

        [Display(Name ="Military Information:")]
        [Required()]
        public bool IsVeteran { get; set; }

        [Display(Name ="Military Status:")]
        [StringLength(4)]
        public string MilitaryStatus { get; set; }

        [Display(Name ="Veteran Type:")]
        [StringLength(4)]
        public string MilitaryBranch { get; set; }

        [Display(Name ="Branch Served:")]
        [StringLength(4)]
        public string VeteranType { get; set; }

        [Display(Name ="Applicable Government Benefits:")]
        [StringLength(10)]
        public string GovernmentBenefits { get; set; }
        #endregion Properties
    }
}
