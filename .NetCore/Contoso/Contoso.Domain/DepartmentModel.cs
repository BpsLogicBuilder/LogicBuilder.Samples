using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LogicBuilder.Attributes;


namespace Contoso.Domain.Entities
{
    public class DepartmentModel : BaseModelClass
    {
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Department_DepartmentID")]
		public int DepartmentID { get; set; }

		[StringLength(50, MinimumLength = 3)]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Department_Name")]
		public string Name { get; set; }

		[DataType(DataType.Currency)]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Department_Budget")]
		public decimal Budget { get; set; }

		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString  = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		[Display(Name = "Start Date")]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Department_StartDate")]
		public System.DateTime StartDate { get; set; }

		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Department_InstructorID")]
		public int? InstructorID { get; set; }

        [ListEditorControl(ListControlType.HashSetForm)]
        [AlsoKnownAs("Department_RowVersion")]
		public byte[] RowVersion { get; set; }

        public string AdministratorName { get; set; }
        //[AlsoKnownAs("Department_Administrator")]
        //public InstructorModel Administrator { get; set; }

        [AlsoKnownAs("Department_Courses")]
		public ICollection<CourseModel> Courses { get; set; }
    }
}