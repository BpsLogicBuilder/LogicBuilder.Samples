using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LogicBuilder.Attributes;

namespace Enrollment.Domain.Entities
{
    public class PersonalModel : BaseModelClass
    {
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Personal_UserId")]
		public int UserId { get; set; }

		[Display(Name = "First Name:")]
		[Required]
		[StringLength(30)]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Personal_FirstName")]
		public string FirstName { get; set; }

		[Display(Name = "Middle Name:")]
		[StringLength(20)]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Personal_MiddleName")]
		public string MiddleName { get; set; }

		[Display(Name = "Last Name:")]
		[Required]
		[StringLength(30)]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Personal_LastName")]
		public string LastName { get; set; }

		[Display(Name = "Suffix:")]
		[StringLength(6)]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Personal_Suffix")]
		public string Suffix { get; set; }

		[Display(Name = "Mailing Address1:")]
		[Required]
		[StringLength(30)]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Personal_Address1")]
		public string Address1 { get; set; }

		[Display(Name = "Mailing Address2:")]
		[StringLength(30)]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Personal_Address2")]
		public string Address2 { get; set; }

		[Display(Name = "City:")]
		[Required]
		[StringLength(30)]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Personal_City")]
		public string City { get; set; }

		[Required]
		[StringLength(25)]
		[Display(Name = "State:")]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Personal_State")]
		public string State { get; set; }

		[Display(Name = "County:")]
		[StringLength(25)]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Personal_County")]
		public string County { get; set; }

		[Display(Name = "Zip Code (00000):")]
		[Required]
		[StringLength(5)]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Personal_ZipCode")]
		public string ZipCode { get; set; }

		[Display(Name = "Cell Phone:")]
		[StringLength(15)]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Personal_CellPhone")]
		public string CellPhone { get; set; }

		[Display(Name = "Other Phone:")]
		[StringLength(15)]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Personal_OtherPhone")]
		public string OtherPhone { get; set; }

		[Display(Name = "Primary Email):")]
		[StringLength(40)]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Personal_PrimaryEmail")]
		public string PrimaryEmail { get; set; }
    }
}