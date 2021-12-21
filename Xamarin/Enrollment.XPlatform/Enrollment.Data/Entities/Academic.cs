using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Enrollment.Data.Entities
{
    public class Academic : BaseDataClass
    {
        #region Properties
        [Key]
        [ForeignKey("User")]
        public int UserId { get; set; }

        public virtual User User { get; set; }

        [Display(Name = "Last High School Location:")]
        [Required()]
        [StringLength(256)]
        public string LastHighSchoolLocation { get; set; }

        [Display(Name = "NC High School Name:")]
        [StringLength(256)]
        public string NcHighSchoolName { get; set; }

        [Display(Name = "Home School Type:")]
        [StringLength(256)]
        public string HomeSchoolType { get; set; }

        [Display(Name = "Home School Association:")]
        [StringLength(256)]
        public string HomeSchoolAssociation { get; set; }

        [Required]
        public DateTime FromDate { get; set; }

        [Display(Name = "To Date:")]
        [Required]
        public DateTime ToDate { get; set; }

        [Display(Name = "Graduation Status:")]
        [Required()]
        [StringLength(256)]
        public string GraduationStatus { get; set; }

        [Display(Name = "Did you receive a GED?")]
        public bool? ReceivedGed { get; set; }

        [Display(Name = "Where did you receive your GED?")]
 
        public string GedLocation { get; set; }

        [Display(Name = "Date GED Received:")]
        public DateTime? GedDate { get; set; }

        [Display(Name = "Have you ever earned college credit hours at Charlotte School of Science")]
        [Required()]
        public bool EarnedCreditAtCmc { get; set; }

        [Display(Name = "Have you attended other colleges or universities?")]
        [Required()]
        public bool AttendedPriorColleges { get; set; }


        public ICollection<Institution> Institutions { get; set; }
        #endregion Properties
    }
}
