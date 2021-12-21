using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LogicBuilder.Attributes;


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
		[Required]
		[StringLength(50)]
		[Display(Name = "Last Name")]
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
		[Required]
		[StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
		[Display(Name = "First Name")]
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
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString  = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		[Display(Name = "Enrollment Date")]
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