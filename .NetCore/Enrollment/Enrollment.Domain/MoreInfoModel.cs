using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LogicBuilder.Attributes;

namespace Enrollment.Domain.Entities
{
    public class MoreInfoModel : BaseModelClass
    {
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("MoreInfo_UserId")]
		public int UserId { get; set; }

		[Display(Name = "What is your most important reason for attending?")]
		[Required]
		[StringLength(4)]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("MoreInfo_ReasonForAttending")]
		public string ReasonForAttending { get; set; }

		[Display(Name = "What is your overall educational goal?")]
		[Required]
		[StringLength(4)]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("MoreInfo_OverallEducationalGoal")]
		public string OverallEducationalGoal { get; set; }

		[Display(Name = "Military Information:")]
		[Required]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("MoreInfo_IsVeteran")]
		public bool IsVeteran { get; set; }

		[Display(Name = "Military Status:")]
		[StringLength(4)]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("MoreInfo_MilitaryStatus")]
		public string MilitaryStatus { get; set; }

		[Display(Name = "Veteran Type:")]
		[StringLength(4)]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("MoreInfo_MilitaryBranch")]
		public string MilitaryBranch { get; set; }

		[Display(Name = "Branch Served:")]
		[StringLength(4)]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("MoreInfo_VeteranType")]
		public string VeteranType { get; set; }

		[Display(Name = "Applicable Government Benefits:")]
		[StringLength(10)]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("MoreInfo_GovernmentBenefits")]
		public string GovernmentBenefits { get; set; }
    }
}