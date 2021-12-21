using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Enrollment.Data.Entities
{
    public class StateLivedIn : BaseDataClass
    {
        #region Properties
        [Key]
        public int StateLivedInId { get; set; }

        [Required]
        [StringLength(25)]
        [Display(Name = "State:")]
        public string State { get; set; }

        [ForeignKey("Residency")]
        public int UserId { get; set; }

        public virtual Residency Residency { get; set; }
        #endregion Properties
    }
}
