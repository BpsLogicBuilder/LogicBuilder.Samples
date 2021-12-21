using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LogicBuilder.Attributes;


namespace Contoso.Domain.Entities
{
    public class DepartmentModel : EntityModelBase
    {
		private int _departmentID;
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Department_DepartmentID")]
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

		private string _name;
		[StringLength(50, MinimumLength = 3)]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Department_Name")]
		public string Name
		{
			get { return _name; }
			set
			{
				if (_name == value)
					return;

				_name = value;
				OnPropertyChanged();
			}
		}

		private decimal _budget;
		[DataType(DataType.Currency)]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Department_Budget")]
		public decimal Budget
		{
			get { return _budget; }
			set
			{
				if (_budget == value)
					return;

				_budget = value;
				OnPropertyChanged();
			}
		}

		public string BudgetString { get; set; }

		private System.DateTime _startDate;
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString  = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		[Display(Name = "Start Date")]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Department_StartDate")]
		public System.DateTime StartDate
		{
			get { return _startDate; }
			set
			{
				if (_startDate == value)
					return;

				_startDate = value;
				OnPropertyChanged();
			}
		}

		public string StartDateString { get; set; }

		private int? _instructorID;
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Department_InstructorID")]
		public int? InstructorID
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

		private byte[] _rowVersion;
		[ListEditorControl(ListControlType.HashSetForm)]
		[AlsoKnownAs("Department_RowVersion")]
		public byte[] RowVersion
		{
			get { return _rowVersion; }
			set
			{
				if (_rowVersion == value)
					return;

				_rowVersion = value;
				OnPropertyChanged();
			}
		}

		private string _administratorName;
		[AlsoKnownAs("Department_AdministratorName")]
		public string AdministratorName
		{
			get { return _administratorName; }
			set
			{
				if (_administratorName == value)
					return;

				_administratorName = value;
				OnPropertyChanged();
			}
		}

		//private InstructorModel _administrator;
		//[AlsoKnownAs("Department_Administrator")]
		//public InstructorModel Administrator
		//{
		//	get { return _administrator; }
		//	set
		//	{
		//		if (_administrator == value)
		//			return;

		//		_administrator = value;
		//		OnPropertyChanged();
		//	}
		//}

		private ICollection<CourseModel> _courses;
		[ListEditorControl(ListControlType.HashSetForm)]
		[AlsoKnownAs("Department_Courses")]
		public ICollection<CourseModel> Courses
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
    }
}