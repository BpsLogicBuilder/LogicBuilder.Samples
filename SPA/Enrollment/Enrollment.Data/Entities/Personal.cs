namespace Enrollment.Data.Entities
{
    public class Personal : BaseDataClass
    {
        #region Properties
        public int UserId { get; set; }

        public virtual User User { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Suffix { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string County { get; set; }

        public string ZipCode { get; set; }

        public string CellPhone { get; set; }

        public string OtherPhone { get; set; }

        public string PrimaryEmail { get; set; }
        #endregion Properties
    }
}
