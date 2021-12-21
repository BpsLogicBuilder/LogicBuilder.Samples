using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contoso.Data.Rules
{
    [Table("RulesModule", Schema = "Rules")]
    public class RulesModule : BaseDataClass
    {
        public int RulesModuleId { get; set; }
        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Name { get; set; }
        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Application { get; set; }
        [Required]
        public byte[] RuleSetFile { get; set; }
        [Required]
        public byte[] ResourceSetFile { get; set; }
        [Column(TypeName = "varchar(256)")]
        public string LoggedInUserId { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}
