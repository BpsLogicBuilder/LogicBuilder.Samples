using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LogicBuilder.Attributes;


namespace Contoso.Domain.Entities
{
    public class OfficeAssignmentModel : BaseModelClass
    {
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("OfficeAssignment_InstructorID")]
		public int InstructorID { get; set; }

		[StringLength(50)]
		[Display(Name = "Office Location")]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("OfficeAssignment_Location")]
		public string Location { get; set; }

		//[AlsoKnownAs("OfficeAssignment_Instructor")]
		//public InstructorModel Instructor { get; set; }
    }
}