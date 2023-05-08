using LogicBuilder.Attributes;
using System.Collections.Generic;


namespace Contoso.Domain.Entities
{
    public class CourseModel : BaseModelClass
    {
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Course_CourseID")]
		public int CourseID { get; set; }

		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Course_Title")]
		public string Title { get; set; }

		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Course_Credits")]
		public int Credits { get; set; }

		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Course_DepartmentID")]
		public int DepartmentID { get; set; }

        public string DepartmentName { get; set; }
        //[AlsoKnownAs("Course_Department")]
        //public DepartmentModel Department { get; set; }

        //[AlsoKnownAs("Course_Enrollments")]
        //public ICollection<EnrollmentModel> Enrollments { get; set; }

        [ListEditorControl(ListControlType.HashSetForm)]
        [AlsoKnownAs("Course_Assignments")]
		public ICollection<CourseAssignmentModel> Assignments { get; set; }
    }
}