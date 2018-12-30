using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Enrollment.Data.Entities
{
    public class Institution : BaseDataClass
    {
        #region Properties
        [Key]
        public int InstitutionId { get; set; }

        public virtual Academic Academic { get; set; }

        [Required]
        [StringLength(256)]
        public string InstitutionState { get; set; }

        [Required]
        public string InstitutionName { get; set; }

        [Required]
        [StringLength(4)]
        public string StartYear { get; set; }

        [Required]
        [StringLength(4)]
        public string EndYear { get; set; }

        [Required]
        [StringLength(4)]
        public string HighestDegreeEarned { get; set; }

        public DateTime? MonthYearGraduated { get; set; }

        [ForeignKey("Academic")]
        public int UserId { get; set; }
        #endregion Properties
    }
}
