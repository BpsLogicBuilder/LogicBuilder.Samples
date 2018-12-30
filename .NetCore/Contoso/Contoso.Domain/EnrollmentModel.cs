using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LogicBuilder.Attributes;


namespace Contoso.Domain.Entities
{
    public class EnrollmentModel : BaseModelClass
    {
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Enrollment_EnrollmentID")]
		public int EnrollmentID { get; set; }

		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Enrollment_CourseID")]
		public int CourseID { get; set; }

		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Enrollment_StudentID")]
		public int StudentID { get; set; }

		[DisplayFormat(NullDisplayText = "No grade")]
		[AlsoKnownAs("Enrollment_Grade")]
		public Grade? Grade { get; set; }

        public string GradeLetter { get; set; }

        public string CourseTitle { get; set; }

        public string StudentName { get; set; }

        //[AlsoKnownAs("Enrollment_Course")]
        //public CourseModel Course { get; set; }

        //[AlsoKnownAs("Enrollment_Student")]
        //public StudentModel Student { get; set; }
    }
}