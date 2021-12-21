using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LogicBuilder.Attributes;


namespace Contoso.Domain.Entities
{
    public class CourseAssignmentModel : EntityModelBase
    {
		private int _instructorID;
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("CourseAssignment_InstructorID")]
		public int InstructorID
		{
			get { return _instructorID; }
			set
			{
				if (_instructorID == value)
					return;

				_instructorID = value;
				OnPropertyChanged();
			}
		}

		private int _courseID;
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("CourseAssignment_CourseID")]
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

		private string _courseTitle;
		[AlsoKnownAs("CourseAssignment_CourseTitle")]
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

		private string _courseNumberAndTitle;
		[AlsoKnownAs("CourseAssignment_CourseNumberAndTitle")]
		public string CourseNumberAndTitle
		{
			get { return _courseNumberAndTitle; }
			set
			{
				if (_courseNumberAndTitle == value)
					return;

				_courseNumberAndTitle = value;
				OnPropertyChanged();
			}
		}

		private string _department;
		[AlsoKnownAs("CourseAssignment_Department")]
		public string Department
		{
			get { return _department; }
			set
			{
				if (_department == value)
					return;

				_department = value;
				OnPropertyChanged();
			}
		}

		//private InstructorModel _instructor;
		//[AlsoKnownAs("CourseAssignment_Instructor")]
		//public InstructorModel Instructor
		//{
		//	get { return _instructor; }
		//	set
		//	{
		//		if (_instructor == value)
		//			return;

		//		_instructor = value;
		//		OnPropertyChanged();
		//	}
		//}

		//private CourseModel _course;
		//[AlsoKnownAs("CourseAssignment_Course")]
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
	}
}