using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LogicBuilder.Attributes;


namespace Contoso.Domain.Entities
{
    public class CourseModel : EntityModelBase
    {
		private int _courseID;
		[Display(Name = "Number")]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Course_CourseID")]
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

		public string CourseIDString { get; set; }

		private string _title;
		[StringLength(50, MinimumLength = 3)]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Course_Title")]
		public string Title
		{
			get { return _title; }
			set
			{
				if (_title == value)
					return;

				_title = value;
				OnPropertyChanged();
			}
		}

		private int _credits;
		[Range(0, 5)]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Course_Credits")]
		public int Credits
		{
			get { return _credits; }
			set
			{
				if (_credits == value)
					return;

				_credits = value;
				OnPropertyChanged();
			}
		}

		private int _departmentID;
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Course_DepartmentID")]
		public int DepartmentID
		{
			get { return _departmentID; }
			set
			{
				if (_departmentID == value)
					return;

				_departmentID = value;
				OnPropertyChanged();
			}
		}

		private string _departmentName;
		[AlsoKnownAs("Course_DepartmentName")]
		public string DepartmentName
		{
			get { return _departmentName; }
			set
			{
				if (_departmentName == value)
					return;

				_departmentName = value;
				OnPropertyChanged();
			}
		}

		//private DepartmentModel _department;
		//[AlsoKnownAs("Course_Department")]
		//public DepartmentModel Department
		//{
		//	get { return _department; }
		//	set
		//	{
		//		if (_department == value)
		//			return;

		//		_department = value;
		//		OnPropertyChanged();
		//	}
		//}

		//private ICollection<EnrollmentModel> _enrollments;
		//[ListEditorControl(ListControlType.HashSetForm)]
		//[AlsoKnownAs("Course_Enrollments")]
		//public ICollection<EnrollmentModel> Enrollments
		//{
		//	get { return _enrollments; }
		//	set
		//	{
		//		if (_enrollments == value)
		//			return;

		//		_enrollments = value;
		//		OnPropertyChanged();
		//	}
		//}

		private ICollection<CourseAssignmentModel> _assignments;
		[ListEditorControl(ListControlType.HashSetForm)]
		[AlsoKnownAs("Course_Assignments")]
		public ICollection<CourseAssignmentModel> Assignments
		{
			get { return _assignments; }
			set
			{
				if (_assignments == value)
					return;

				_assignments = value;
				OnPropertyChanged();
			}
		}
    }
}