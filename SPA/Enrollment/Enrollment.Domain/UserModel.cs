using LogicBuilder.Attributes;


namespace Enrollment.Domain.Entities
{
    public class UserModel : EntityModelBase
    {
		private int _userId;
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("User_UserId")]
		public int UserId
		{
			get { return _userId; }
			set
			{
				if (_userId == value)
					return;

				_userId = value;
				OnPropertyChanged();
			}
		}

		private string _userName;
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("User_UserName")]
		public string UserName
		{
			get { return _userName; }
			set
			{
				if (_userName == value)
					return;

				_userName = value;
				OnPropertyChanged();
			}
		}

		private PersonalModel _personal;
		[AlsoKnownAs("User_Personal")]
		public PersonalModel Personal
		{
			get { return _personal; }
			set
			{
				if (_personal == value)
					return;

				_personal = value;
				OnPropertyChanged();
			}
		}

		private AcademicModel _academic;
		[AlsoKnownAs("User_Academic")]
		public AcademicModel Academic
		{
			get { return _academic; }
			set
			{
				if (_academic == value)
					return;

				_academic = value;
				OnPropertyChanged();
			}
		}

		private AdmissionsModel _admissions;
		[AlsoKnownAs("User_Admissions")]
		public AdmissionsModel Admissions
		{
			get { return _admissions; }
			set
			{
				if (_admissions == value)
					return;

				_admissions = value;
				OnPropertyChanged();
			}
		}

		private CertificationModel _certification;
		[AlsoKnownAs("User_Certification")]
		public CertificationModel Certification
		{
			get { return _certification; }
			set
			{
				if (_certification == value)
					return;

				_certification = value;
				OnPropertyChanged();
			}
		}

		private ContactInfoModel _contactInfo;
		[AlsoKnownAs("User_ContactInfo")]
		public ContactInfoModel ContactInfo
		{
			get { return _contactInfo; }
			set
			{
				if (_contactInfo == value)
					return;

				_contactInfo = value;
				OnPropertyChanged();
			}
		}

		private MoreInfoModel _moreInfo;
		[AlsoKnownAs("User_MoreInfo")]
		public MoreInfoModel MoreInfo
		{
			get { return _moreInfo; }
			set
			{
				if (_moreInfo == value)
					return;

				_moreInfo = value;
				OnPropertyChanged();
			}
		}

		private ResidencyModel _residency;
		[AlsoKnownAs("User_Residency")]
		public ResidencyModel Residency
		{
			get { return _residency; }
			set
			{
				if (_residency == value)
					return;

				_residency = value;
				OnPropertyChanged();
			}
		}
    }
}