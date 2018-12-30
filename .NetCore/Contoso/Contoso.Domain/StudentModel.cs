using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LogicBuilder.Attributes;


namespace Contoso.Domain.Entities
{
    public class StudentModel : BaseModelClass
    {
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Student_ID")]
		public int ID { get; set; }

		[Required]
		[StringLength(50)]
		[Display(Name = "Last Name")]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Student_LastName")]
		public string LastName { get; set; }

		[Required]
		[StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
		[Display(Name = "First Name")]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Student_FirstName")]
		public string FirstName { get; set; }

        public string FullName { get; set; }

        [DataType(DataType.Date)]
		[DisplayFormat(DataFormatString  = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		[Display(Name = "Enrollment Date")]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Student_EnrollmentDate")]
		public System.DateTime EnrollmentDate { get; set; }

		[AlsoKnownAs("Student_Enrollments")]
		public ICollection<EnrollmentModel> Enrollments { get; set; }
    }
}