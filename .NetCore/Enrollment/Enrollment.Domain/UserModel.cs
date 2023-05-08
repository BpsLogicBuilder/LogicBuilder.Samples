using LogicBuilder.Attributes;

namespace Enrollment.Domain.Entities
{
    public class UserModel : BaseModelClass
    {
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("User_UserId")]
		public int UserId { get; set; }

		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("User_UserName")]
		public string UserName { get; set; }

		[AlsoKnownAs("User_Personal")]
		public PersonalModel Personal { get; set; }

		[AlsoKnownAs("User_Academic")]
		public AcademicModel Academic { get; set; }

		[AlsoKnownAs("User_Admissions")]
		public AdmissionsModel Admissions { get; set; }

		[AlsoKnownAs("User_Certification")]
		public CertificationModel Certification { get; set; }

		[AlsoKnownAs("User_ContactInfo")]
		public ContactInfoModel ContactInfo { get; set; }

		[AlsoKnownAs("User_MoreInfo")]
		public MoreInfoModel MoreInfo { get; set; }

		[AlsoKnownAs("User_Residency")]
		public ResidencyModel Residency { get; set; }
    }
}