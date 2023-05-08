using LogicBuilder.Attributes;

namespace Enrollment.Domain.Entities
{
    public class ContactInfoModel : BaseModelClass
    {
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("ContactInfo_UserId")]
		public int UserId { get; set; }

		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("ContactInfo_HasFormerName")]
		public bool HasFormerName { get; set; }

		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("ContactInfo_FormerFirstName")]
		public string FormerFirstName { get; set; }

		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("ContactInfo_FormerMiddleName")]
		public string FormerMiddleName { get; set; }

		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("ContactInfo_FormerLastName")]
		public string FormerLastName { get; set; }

		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("ContactInfo_DateOfBirth")]
		public System.DateTime DateOfBirth { get; set; }

		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("ContactInfo_SocialSecurityNumber")]
		public string SocialSecurityNumber { get; set; }

        public string ConfirmSocialSecurityNumber { get => SocialSecurityNumber; set { } }

		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("ContactInfo_Gender")]
		public string Gender { get; set; }

		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("ContactInfo_Race")]
		public string Race { get; set; }

		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("ContactInfo_Ethnicity")]
		public string Ethnicity { get; set; }

		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("ContactInfo_EnergencyContactFirstName")]
		public string EnergencyContactFirstName { get; set; }

		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("ContactInfo_EnergencyContactLastName")]
		public string EnergencyContactLastName { get; set; }

		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("ContactInfo_EnergencyContactRelationship")]
		public string EnergencyContactRelationship { get; set; }

		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("ContactInfo_EnergencyContactPhoneNumber")]
		public string EnergencyContactPhoneNumber { get; set; }
    }
}