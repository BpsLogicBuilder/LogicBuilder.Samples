using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LogicBuilder.Attributes;


namespace Contoso.Domain.Entities
{
    public class EnrollmentModel : EntityModelBase
    {
		private int _enrollmentID;
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Enrollment_EnrollmentID")]
		public int EnrollmentID
		{
			get { return _enrollmentID; }
			set
			{
				if (_enrollmentID == value)
					return;

				_enrollmentID = value;
				OnPropertyChanged();
			}
		}

		private int _courseID;
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Enrollment_CourseID")]
		public int CourseID
		{
			get { return _courseID; }
			set
			{
				if (_courseID == value)
					return;

				_courseID = value;
				OnPropertyChanged();
			}
		}

		private int _studentID;
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Enrollment_StudentID")]
		public int StudentID
		{
			get { return _studentID; }
			set
			{
				if (_studentID == value)
					return;

				_studentID = value;
				OnPropertyChanged();
			}
		}

		private Grade? _grade;
		[DisplayFormat(NullDisplayText = "No grade")]
		[AlsoKnownAs("Enrollment_Grade")]
		public Grade? Grade
		{
			get { return _grade; }
			set
			{
				if (_grade == value)
					return;

				_grade = value;
				OnPropertyChanged();
			}
		}

		private string _gradeLetter;
		[AlsoKnownAs("Enrollment_GradeLetter")]
		public string GradeLetter
		{
			get { return _gradeLetter; }
			set
			{
				if (_gradeLetter == value)
					return;

				_gradeLetter = value;
				OnPropertyChanged();
			}
		}

		private string _courseTitle;
		[AlsoKnownAs("Enrollment_CourseTitle")]
		public string CourseTitle
		{
			get { return _courseTitle; }
			set
			{
				if (_courseTitle == value)
					return;

				_courseTitle = value;
				OnPropertyChanged();
			}
		}

		private string _studentName;
		[AlsoKnownAs("Enrollment_StudentName")]
		public string StudentName
		{
			get { return _studentName; }
			set
			{
				if (_studentName == value)
					return;

				_studentName = value;
				OnPropertyChanged();
			}
		}

		//private CourseModel _course;
		//[AlsoKnownAs("Enrollment_Course")]
		//public CourseModel Course
		//{
		//	get { return _course; }
		//	set
		//	{
		//		if (_course == value)
		//			return;

		//		_course = value;
		//		OnPropertyChanged();
		//	}
		//}

		//private StudentModel _student;
		//[AlsoKnownAs("Enrollment_Student")]
		//public StudentModel Student
		//{
		//	get { return _student; }
		//	set
		//	{
		//		if (_student == value)
		//			return;

		//		_student = value;
		//		OnPropertyChanged();
		//	}
		//}
	}
}