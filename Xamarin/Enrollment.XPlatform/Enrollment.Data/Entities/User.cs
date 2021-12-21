using System.ComponentModel.DataAnnotations;

namespace Enrollment.Data.Entities
{
    public class User : BaseDataClass
    {
        [Key]
        public int UserId { get; set; }

        public string UserName { get; set; }
        public Personal Personal { get; set; }
        public Academic Academic { get; set; }
        public Admissions Admissions { get; set; }
        public Certification Certification { get; set; }
        public ContactInfo ContactInfo { get; set; }
        public MoreInfo MoreInfo { get; set; }
        public Residency Residency { get; set; }
    }
}
