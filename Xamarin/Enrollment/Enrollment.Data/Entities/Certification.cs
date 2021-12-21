using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Enrollment.Data.Entities
{
    public class Certification : BaseDataClass
    {
        #region Properties
        [Key]
        [ForeignKey("User")]
        public int UserId { get; set; }

        public virtual User User { get; set; }

        public bool CertificateStatementChecked { get; set; }
        public bool DeclarationStatementChecked { get; set; }
        public bool PolicyStatementsChecked { get; set; }
        #endregion Properties
    }
}
