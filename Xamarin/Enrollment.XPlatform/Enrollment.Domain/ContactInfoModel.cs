using LogicBuilder.Attributes;
using System.ComponentModel.DataAnnotations;


namespace Enrollment.Domain.Entities
{
    public class ContactInfoModel : EntityModelBase
    {
        private int _userId;
        [VariableEditorControl(VariableControlType.SingleLineTextBox)]
        [AlsoKnownAs("ContactInfo_UserId")]
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

        private UserModel _user;
        [AlsoKnownAs("ContactInfo_User")]
        public UserModel User
        {
            get { return _user; }
            set
            {
                if (_user == value)
                    return;

                _user = value;
                OnPropertyChanged();
            }
        }

        private bool _hasFormerName;
        [Display(Name = "Could your records be under another name?")]
        [Required]
        [VariableEditorControl(VariableControlType.SingleLineTextBox)]
        [AlsoKnownAs("ContactInfo_HasFormerName")]
        public bool HasFormerName
        {
            get { return _hasFormerName; }
            set
            {
                if (_hasFormerName == value)
                    return;

                _hasFormerName = value;
                OnPropertyChanged();
            }
        }

        private string _formerFirstName;
        [Display(Name = "Former First Name:")]
        [StringLength(30)]
        [VariableEditorControl(VariableControlType.SingleLineTextBox)]
        [AlsoKnownAs("ContactInfo_FormerFirstName")]
        public string FormerFirstName
        {
            get { return _formerFirstName; }
            set
            {
                if (_formerFirstName == value)
                    return;

                _formerFirstName = value;
                OnPropertyChanged();
            }
        }

        private string _formerMiddleName;
        [Display(Name = "Former Middle Name:")]
        [StringLength(30)]
        [VariableEditorControl(VariableControlType.SingleLineTextBox)]
        [AlsoKnownAs("ContactInfo_FormerMiddleName")]
        public string FormerMiddleName
        {
            get { return _formerMiddleName; }
            set
            {
                if (_formerMiddleName == value)
                    return;

                _formerMiddleName = value;
                OnPropertyChanged();
            }
        }

        private string _formerLastName;
        [Display(Name = "Former Last Name:")]
        [StringLength(30)]
        [VariableEditorControl(VariableControlType.SingleLineTextBox)]
        [AlsoKnownAs("ContactInfo_FormerLastName")]
        public string FormerLastName
        {
            get { return _formerLastName; }
            set
            {
                if (_formerLastName == value)
                    return;

                _formerLastName = value;
                OnPropertyChanged();
            }
        }

        private System.DateTime _dateOfBirth;
        [Display(Name = "Date Of Birth:")]
        [Required]
        [VariableEditorControl(VariableControlType.SingleLineTextBox)]
        [AlsoKnownAs("ContactInfo_DateOfBirth")]
        public System.DateTime DateOfBirth
        {
            get { return _dateOfBirth; }
            set
            {
                if (_dateOfBirth == value)
                    return;

                _dateOfBirth = value;
                OnPropertyChanged();
            }
        }

        private string _socialSecurityNumber;
        [Display(Name = "Social Security #:")]
        [Required]
        [RegularExpression(@"^\d{3}-\d{2}-\d{4}$", MatchTimeoutInMilliseconds = 2000)]
        [StringLength(11)]
        [VariableEditorControl(VariableControlType.SingleLineTextBox)]
        [AlsoKnownAs("ContactInfo_SocialSecurityNumber")]
        public string SocialSecurityNumber
        {
            get { return _socialSecurityNumber; }
            set
            {
                if (_socialSecurityNumber == value)
                    return;

                _socialSecurityNumber = value;
                OnPropertyChanged();
            }
        }

        private string _confirmSocialSecurityNumber;
        public string ConfirmSocialSecurityNumber
        {
            get
            {
                return _confirmSocialSecurityNumber;
            }

            set
            {
                _confirmSocialSecurityNumber = value;
                OnPropertyChanged();
            }
        }

        private string _gender;
        [Display(Name = "Gender:")]
        [Required]
        [StringLength(1)]
        [VariableEditorControl(VariableControlType.SingleLineTextBox)]
        [AlsoKnownAs("ContactInfo_Gender")]
        public string Gender
        {
            get { return _gender; }
            set
            {
                if (_gender == value)
                    return;

                _gender = value;
                OnPropertyChanged();
            }
        }

        private string _race;
        [Display(Name = "Race:")]
        [Required]
        [StringLength(3)]
        [VariableEditorControl(VariableControlType.SingleLineTextBox)]
        [AlsoKnownAs("ContactInfo_Race")]
        public string Race
        {
            get { return _race; }
            set
            {
                if (_race == value)
                    return;

                _race = value;
                OnPropertyChanged();
            }
        }

        private string _ethnicity;
        [Display(Name = "Ethnicity:")]
        [Required]
        [StringLength(3)]
        [VariableEditorControl(VariableControlType.SingleLineTextBox)]
        [AlsoKnownAs("ContactInfo_Ethnicity")]
        public string Ethnicity
        {
            get { return _ethnicity; }
            set
            {
                if (_ethnicity == value)
                    return;

                _ethnicity = value;
                OnPropertyChanged();
            }
        }

        private string _energencyContactFirstName;
        [Display(Name = "Energency Contact First Name:")]
        [Required]
        [StringLength(30)]
        [VariableEditorControl(VariableControlType.SingleLineTextBox)]
        [AlsoKnownAs("ContactInfo_EnergencyContactFirstName")]
        public string EnergencyContactFirstName
        {
            get { return _energencyContactFirstName; }
            set
            {
                if (_energencyContactFirstName == value)
                    return;

                _energencyContactFirstName = value;
                OnPropertyChanged();
            }
        }

        private string _energencyContactLastName;
        [Display(Name = "Energency Contact Last Name:")]
        [Required]
        [StringLength(30)]
        [VariableEditorControl(VariableControlType.SingleLineTextBox)]
        [AlsoKnownAs("ContactInfo_EnergencyContactLastName")]
        public string EnergencyContactLastName
        {
            get { return _energencyContactLastName; }
            set
            {
                if (_energencyContactLastName == value)
                    return;

                _energencyContactLastName = value;
                OnPropertyChanged();
            }
        }

        private string _energencyContactRelationship;
        [Display(Name = "Energency Contact Relationship:")]
        [Required]
        [StringLength(30)]
        [VariableEditorControl(VariableControlType.SingleLineTextBox)]
        [AlsoKnownAs("ContactInfo_EnergencyContactRelationship")]
        public string EnergencyContactRelationship
        {
            get { return _energencyContactRelationship; }
            set
            {
                if (_energencyContactRelationship == value)
                    return;

                _energencyContactRelationship = value;
                OnPropertyChanged();
            }
        }

        private string _energencyContactPhoneNumber;
        [Display(Name = "Energency Contact Phone #:")]
        [RegularExpression(@"^\d{3}-\d{3}-\d{4}$", MatchTimeoutInMilliseconds = 2000)]
        [Required]
        [StringLength(12)]
        [VariableEditorControl(VariableControlType.SingleLineTextBox)]
        [AlsoKnownAs("ContactInfo_EnergencyContactPhoneNumber")]
        public string EnergencyContactPhoneNumber
        {
            get { return _energencyContactPhoneNumber; }
            set
            {
                if (_energencyContactPhoneNumber == value)
                    return;

                _energencyContactPhoneNumber = value;
                OnPropertyChanged();
            }
        }
    }
}