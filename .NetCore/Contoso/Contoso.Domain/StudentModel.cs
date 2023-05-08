using LogicBuilder.Attributes;
using System.Collections.Generic;


namespace Contoso.Domain.Entities
{
    public class StudentModel : BaseModelClass
    {
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Student_ID")]
		public int ID { get; set; }

		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Student_LastName")]
		public string LastName { get; set; }

		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Student_FirstName")]
		public string FirstName { get; set; }

        public string FullName { get; set; }

		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Student_EnrollmentDate")]
		public System.DateTime EnrollmentDate { get; set; }

		[AlsoKnownAs("Student_Enrollments")]
		public ICollection<EnrollmentModel> Enrollments { get; set; }
    }
}