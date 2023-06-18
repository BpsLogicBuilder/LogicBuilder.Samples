using LogicBuilder.Attributes;

namespace Enrollment.Domain.Entities
{
    public class PersonModel : EntityModelBase
    {
        private int _iD;
        [VariableEditorControl(VariableControlType.SingleLineTextBox)]
        [AlsoKnownAs("Person_ID")]
        public int ID
        {
            get { return _iD; }
            set
            {
                if (_iD == value)
                    return;

                _iD = value;
                OnPropertyChanged();
            }
        }

        private string _lastName;
        [VariableEditorControl(VariableControlType.SingleLineTextBox)]
        [AlsoKnownAs("Person_LastName")]
        public string LastName
        {
            get { return _lastName; }
            set
            {
                if (_lastName == value)
                    return;

                _lastName = value;
                OnPropertyChanged();
            }
        }

        private string _firstName;
        [VariableEditorControl(VariableControlType.SingleLineTextBox)]
        [AlsoKnownAs("Person_FirstName")]
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (_firstName == value)
                    return;

                _firstName = value;
                OnPropertyChanged();
            }
        }

        [AlsoKnownAs("Person_FullName")]
        public string FullName { get; set; }

        private System.DateTime _dateOfBirth;
        [VariableEditorControl(VariableControlType.SingleLineTextBox)]
        [AlsoKnownAs("Person_DateOfBirth")]
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

        public string DateOfBirthString { get; set; }
    }
}
