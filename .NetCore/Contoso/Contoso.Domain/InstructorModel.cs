using LogicBuilder.Attributes;
using System.Collections.Generic;


namespace Contoso.Domain.Entities
{
    public class InstructorModel : BaseModelClass
    {
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Instructor_ID")]
		public int ID { get; set; }

		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Instructor_LastName")]
		public string LastName { get; set; }

		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Instructor_FirstName")]
		public string FirstName { get; set; }

        public string FullName { get; set; }

		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Instructor_HireDate")]
		public System.DateTime HireDate { get; set; }

        [ListEditorControl(ListControlType.HashSetForm)]
        [AlsoKnownAs("Instructor_Courses")]
		public ICollection<CourseAssignmentModel> Courses { get; set; }

        [AlsoKnownAs("Instructor_OfficeAssignment")]
		public OfficeAssignmentModel OfficeAssignment { get; set; }
    }
}