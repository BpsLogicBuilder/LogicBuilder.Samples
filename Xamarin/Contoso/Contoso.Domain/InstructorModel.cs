using LogicBuilder.Attributes;
using System.Collections.Generic;


namespace Contoso.Domain.Entities
{
    public class InstructorModel : EntityModelBase
    {
		private int _iD;
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Instructor_ID")]
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
		[AlsoKnownAs("Instructor_LastName")]
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
		[AlsoKnownAs("Instructor_FirstName")]
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

		[AlsoKnownAs("Instructor_FullName")]
		public string FullName { get; set; }

		private System.DateTime _hireDate;
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Instructor_HireDate")]
		public System.DateTime HireDate
		{
			get { return _hireDate; }
			set
			{
				if (_hireDate == value)
					return;

				_hireDate = value;
				OnPropertyChanged();
			}
		}

		public string HireDateString { get; set; }

		private ICollection<CourseAssignmentModel> _courses;
		[ListEditorControl(ListControlType.HashSetForm)]
		[AlsoKnownAs("Instructor_Courses")]
		public ICollection<CourseAssignmentModel> Courses
		{
			get { return _courses; }
			set
			{
				if (_courses == value)
					return;

				_courses = value;
				OnPropertyChanged();
			}
		}

		private OfficeAssignmentModel _officeAssignment;
		[AlsoKnownAs("Instructor_OfficeAssignment")]
		public OfficeAssignmentModel OfficeAssignment
		{
			get { return _officeAssignment; }
			set
			{
				if (_officeAssignment == value)
					return;

				_officeAssignment = value;
				OnPropertyChanged();
			}
		}
    }
}