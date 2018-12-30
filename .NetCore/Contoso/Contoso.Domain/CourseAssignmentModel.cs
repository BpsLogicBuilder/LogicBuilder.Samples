using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LogicBuilder.Attributes;


namespace Contoso.Domain.Entities
{
    public class CourseAssignmentModel : BaseModelClass
    {
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("CourseAssignment_InstructorID")]
		public int InstructorID { get; set; }

		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("CourseAssignment_CourseID")]
		public int CourseID { get; set; }

        public string CourseTitle { get; set; }

        public string CourseNumberAndTitle { get; set; }

        public string Department { get; set; }
        //[AlsoKnownAs("CourseAssignment.Instructor")]
        //public InstructorModel Instructor { get; set; }

        //[AlsoKnownAs("CourseAssignment.Course")]
        //public CourseModel Course { get; set; }
    }
}