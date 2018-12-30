using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LogicBuilder.Attributes;

namespace Enrollment.Domain.Entities
{
    public class StateLivedInModel : BaseModelClass
    {
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("StateLivedIn_StateLivedInId")]
		public int StateLivedInId { get; set; }

		[Required]
		[StringLength(25)]
		[Display(Name = "State:")]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("StateLivedIn_State")]
		public string State { get; set; }

		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("StateLivedIn_UserId")]
		public int UserId { get; set; }
    }
}