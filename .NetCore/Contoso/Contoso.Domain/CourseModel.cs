using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LogicBuilder.Attributes;


namespace Contoso.Domain.Entities
{
    public class CourseModel : BaseModelClass
    {
		[Display(Name = "Number")]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Course_CourseID")]
		public int CourseID { get; set; }

		[StringLength(50, MinimumLength = 3)]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Course_Title")]
		public string Title { get; set; }

		[Range(0, 5)]
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