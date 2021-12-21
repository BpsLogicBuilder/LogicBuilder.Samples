using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Enrollment.Data.Entities
{
    public class Admissions : BaseDataClass
    {
        #region Properties
        [Key]
        [ForeignKey("User")]
        public int UserId { get; set; }

        public virtual User User { get; set; }

        [Display(Name = "What is your entering status?")]
        [Required()]
        [StringLength(1)]
        public string EnteringStatus { get; set; }

        [Display(Name = "Select Enrollment Term you wish to attend:")]
        [Required()]
        [StringLength(6)]
        public string EnrollmentTerm { get; set; }

        [Display(Name = "Select Enrollment Year you wish to attend:")]
        [Required()]
        [StringLength(4)]
        public string EnrollmentYear { get; set; }

        [Display(Name = "Choose a program type:")]
        [Required()]
        [StringLength(55)]
        public string ProgramType { get; set; }

        [Display(Name = "Choose a program:")]
        [Required()]
        [StringLength(55)]
        public string Program { get; set; }
        #endregion Properties
    }
}
