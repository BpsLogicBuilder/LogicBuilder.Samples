using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Enrollment.Data.Entities
{
    public class ContactInfo : BaseDataClass
    {
        #region Properties
        [Key]
        [ForeignKey("User")]
        public int UserId { get; set; }

        public virtual User User { get; set; }

        [Display(Name = "Could your records be under another name?")]
        [Required()]
        public bool HasFormerName { get; set; }

        [Display(Name = "Former First Name:")]
        [StringLength(30)]
        public string FormerFirstName { get; set; }

        [Display(Name = "Former Middle Name:")]
        [StringLength(30)]
        public string FormerMiddleName { get; set; }

        [Display(Name = "Former Last Name:")]
        [StringLength(30)]
        public string FormerLastName { get; set; }

        [Display(Name = "Date Of Birth:")]
        [Required]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Social Security #:")]
        [Required]
        [RegularExpression(@"^\d{3}-\d{2}-\d{4}$")]
        [StringLength(11)]
        public string SocialSecurityNumber { get; set; }

        [Display(Name = "Gender:")]
        [Required()]
        [StringLength(1)]
        public string Gender { get; set; }

        [Display(Name = "Race:")]
        [Required()]
        [StringLength(3)]
        public string Race { get; set; }

        [Display(Name = "Ethnicity:")]
        [Required()]
        [StringLength(3)]
        public string Ethnicity { get; set; }

        [Display(Name = "Energency Contact First Name:")]
        [Required]
        [StringLength(30)]
        public string EnergencyContactFirstName { get; set; }

        [Display(Name = "Energency Contact Last Name:")]
        [Required]
        [StringLength(30)]
        public string EnergencyContactLastName { get; set; }

        [Display(Name = "Energency Contact Relationship:")]
        [Required]
        [StringLength(30)]
        public string EnergencyContactRelationship { get; set; }


        [Display(Name = "Energency Contact Phone #:")]
        [RegularExpression(@"^\d{3}-\d{3}-\d{4}$")]
        [Required]
        [StringLength(12)]
        public string EnergencyContactPhoneNumber { get; set; }
        #endregion Properties
    }
}
