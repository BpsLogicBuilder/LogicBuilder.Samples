namespace Enrollment.Data.Entities
{
    public class Certification : BaseDataClass
    {
        #region Properties
        public int UserId { get; set; }

        public virtual User User { get; set; }

        public bool CertificateStatementChecked { get; set; }
        public bool DeclarationStatementChecked { get; set; }
        public bool PolicyStatementsChecked { get; set; }
        #endregion Properties
    }
}
