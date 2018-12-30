using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LogicBuilder.Attributes;


namespace Contoso.Domain.Entities
{
    public class InstructorModel : BaseModelClass
    {
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Instructor_ID")]
		public int ID { get; set; }

		[Required]
		[StringLength(50)]
		[Display(Name = "Last Name")]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Instructor_LastName")]
		public string LastName { get; set; }

		[Required]
		[StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
		[Display(Name = "First Name")]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Instructor_FirstName")]
		public string FirstName { get; set; }

        public string FullName { get; set; }

        [DataType(DataType.Date)]
		[DisplayFormat(DataFormatString  = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		[Display(Name = "Hire Date")]
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