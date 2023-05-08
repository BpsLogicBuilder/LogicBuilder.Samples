using LogicBuilder.Attributes;

namespace Enrollment.Domain.Entities
{
    public class MoreInfoModel : BaseModelClass
    {
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("MoreInfo_UserId")]
		public int UserId { get; set; }

		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("MoreInfo_ReasonForAttending")]
		public string ReasonForAttending { get; set; }

		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("MoreInfo_OverallEducationalGoal")]
		public string OverallEducationalGoal { get; set; }

		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("MoreInfo_IsVeteran")]
		public bool IsVeteran { get; set; }

		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("MoreInfo_MilitaryStatus")]
		public string MilitaryStatus { get; set; }

		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("MoreInfo_MilitaryBranch")]
		public string MilitaryBranch { get; set; }

		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("MoreInfo_VeteranType")]
		public string VeteranType { get; set; }

		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("MoreInfo_GovernmentBenefits")]
		public string GovernmentBenefits { get; set; }
    }
}