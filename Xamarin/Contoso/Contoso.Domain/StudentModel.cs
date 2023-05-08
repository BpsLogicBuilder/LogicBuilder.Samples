using LogicBuilder.Attributes;
using System.Collections.Generic;


namespace Contoso.Domain.Entities
{
    public class StudentModel : EntityModelBase
    {
		private int _iD;
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Student_ID")]
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
		[AlsoKnownAs("Student_LastName")]
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
		[AlsoKnownAs("Student_FirstName")]
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

		[AlsoKnownAs("Student_FullName")]
		public string FullName { get; set; }

		private System.DateTime _enrollmentDate;
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Student_EnrollmentDate")]
		public System.DateTime EnrollmentDate
		{
			get { return _enrollmentDate; }
			set
			{
				if (_enrollmentDate == value)
					return;

				_enrollmentDate = value;
				OnPropertyChanged();
			}
		}

		public string EnrollmentDateString { get; set; }

		private ICollection<EnrollmentModel> _enrollments;
		[ListEditorControl(ListControlType.HashSetForm)]
		[AlsoKnownAs("Student_Enrollments")]
		public ICollection<EnrollmentModel> Enrollments
		{
			get { return _enrollments; }
			set
			{
				if (_enrollments == value)
					return;

				_enrollments = value;
				OnPropertyChanged();
			}
		}
    }
}