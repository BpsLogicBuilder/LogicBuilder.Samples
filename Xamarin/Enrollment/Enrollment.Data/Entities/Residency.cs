using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Enrollment.Data.Entities
{
    public class Residency : BaseDataClass
    {
        #region Properties
        [Key, ForeignKey("User")]
        public int UserId { get; set; }

        public virtual User User { get; set; }

        [Display(Name ="Citizenship Status:")]
        [Required()]
        [StringLength(6)]
        public string CitizenshipStatus { get; set; }

        [Display(Name ="Immigration Status:")]
        [StringLength(6)]
        public string ImmigrationStatus { get; set; }

        [Display(Name ="Country of Citizenship:")]
        [StringLength(55)]
        public string CountryOfCitizenship { get; set; }

        [Display(Name ="Resident State:")]
        [Required()]
        [StringLength(55)]
        public string ResidentState { get; set; }

        [Display(Name ="Do you have a valid driver’s license?")]
        [Required()]
        public bool HasValidDriversLicense { get; set; }

        [Display(Name ="State Driver’s License Issued From:")]
        [StringLength(10)]
        public string DriversLicenseState { get; set; }

        [Display(Name ="Driver's License Number:")]
        [StringLength(25)]
        public string DriversLicenseNumber { get; set; }

        public ICollection<StateLivedIn> StatesLivedIn { get; set; }
        #endregion Properties
    }
}
