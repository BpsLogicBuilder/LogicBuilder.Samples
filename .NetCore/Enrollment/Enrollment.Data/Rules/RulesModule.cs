using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Enrollment.Data.Rules
{
    [Table("RulesModule", Schema = "Rules")]
    public class RulesModule : BaseDataClass
    {
        public int RulesModuleId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Application { get; set; }
        [Required]
        public byte[] RuleSetFile { get; set; }
        [Required]
        public byte[] ResourceSetFile { get; set; }
        public string LoggedInUserId { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}
