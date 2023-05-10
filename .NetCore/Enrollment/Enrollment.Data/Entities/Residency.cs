using System.Collections.Generic;

namespace Enrollment.Data.Entities
{
    public class Residency : BaseDataClass
    {
        #region Properties
        public int UserId { get; set; }

        public virtual User User { get; set; }

        public string CitizenshipStatus { get; set; }

        public string ImmigrationStatus { get; set; }

        public string CountryOfCitizenship { get; set; }

        public string ResidentState { get; set; }

        public bool HasValidDriversLicense { get; set; }

        public string DriversLicenseState { get; set; }

        public string DriversLicenseNumber { get; set; }

        public ICollection<StateLivedIn> StatesLivedIn { get; set; }
        #endregion Properties
    }
}
