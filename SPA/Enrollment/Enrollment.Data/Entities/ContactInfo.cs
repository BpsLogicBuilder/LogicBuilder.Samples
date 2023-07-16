using System;

namespace Enrollment.Data.Entities
{
    public class ContactInfo : BaseDataClass
    {
        #region Properties
        public int UserId { get; set; }

        public virtual User User { get; set; }

        public bool HasFormerName { get; set; }

        public string FormerFirstName { get; set; }

        public string FormerMiddleName { get; set; }

        public string FormerLastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string SocialSecurityNumber { get; set; }

        public string Gender { get; set; }

        public string Race { get; set; }

        public string Ethnicity { get; set; }

        public string EnergencyContactFirstName { get; set; }

        public string EnergencyContactLastName { get; set; }

        public string EnergencyContactRelationship { get; set; }

        public string EnergencyContactPhoneNumber { get; set; }
        #endregion Properties
    }
}
