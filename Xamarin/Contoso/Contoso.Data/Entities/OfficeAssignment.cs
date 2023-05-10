namespace Contoso.Data.Entities
{

    public class OfficeAssignment : BaseDataClass
    {
        public int InstructorID { get; set; }

        public string Location { get; set; }

        
        public virtual Instructor Instructor { get; set; }
    }
}
