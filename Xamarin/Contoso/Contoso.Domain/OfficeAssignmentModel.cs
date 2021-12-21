using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LogicBuilder.Attributes;


namespace Contoso.Domain.Entities
{
    public class OfficeAssignmentModel : EntityModelBase
    {
		private int _instructorID;
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("OfficeAssignment_InstructorID")]
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

		private string _location;
		[StringLength(50)]
		[Display(Name = "Office Location")]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("OfficeAssignment_Location")]
		public string Location
		{
			get { return _location; }
			set
			{
				if (_location == value)
					return;

				_location = value;
				OnPropertyChanged();
			}
		}

		//private InstructorModel _instructor;
		//[AlsoKnownAs("OfficeAssignment_Instructor")]
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
    }
}