using LogicBuilder.Attributes;
using System.Collections.Generic;


namespace Contoso.Domain.Entities
{
    public class DepartmentModel : BaseModelClass
    {
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Department_DepartmentID")]
		public int DepartmentID { get; set; }

		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Department_Name")]
		public string Name { get; set; }

		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Department_Budget")]
		public decimal Budget { get; set; }

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