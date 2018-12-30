using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Enrollment.Data.Entities
{
    public class Personal : BaseDataClass
    {
        #region Properties
        [Key, ForeignKey("User")]
        public int UserId { get; set; }

        public virtual User User { get; set; }

        [Display(Name ="First Name:")]
        [Required]
        [StringLength(30)]
        public string FirstName { get; set; }

        [Display(Name ="Middle Name:")]
        [StringLength(20)]
        public string MiddleName { get; set; }

        [Display(Name ="Last Name:")]
        [Required]
        [StringLength(30)]
        public string LastName { get; set; }

        [Display(Name ="Suffix:")]
        [StringLength(6)]
        public string Suffix { get; set; }

        [Display(Name ="Mailing Address1:")]
        [Required]
        [StringLength(30)]
        public string Address1 { get; set; }

        [Display(Name ="Mailing Address2:")]
        [StringLength(30)]
        public string Address2 { get; set; }

        [Display(Name ="City:")]
        [Required]
        [StringLength(30)]
        public string City { get; set; }

        [Required]
        [StringLength(25)]
        [Display(Name ="State:")]
        public string State { get; set; }

        [Display(Name ="County:")]
        [StringLength(25)]
        public string County { get; set; }

        [Display(Name ="Zip Code (00000):")]
        [Required]
        [StringLength(5)]
        public string ZipCode { get; set; }

        [Display(Name ="Cell Phone:")]
        [StringLength(15)]
        public string CellPhone { get; set; }

        [Display(Name ="Other Phone:")]
        [StringLength(15)]
        public string OtherPhone { get; set; }

        [Display(Name ="Primary Email):")]
        [StringLength(40)]
        public string PrimaryEmail { get; set; }
        #endregion Properties
    }
}
