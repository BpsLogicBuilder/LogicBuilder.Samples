using LogicBuilder.Attributes;

namespace Enrollment.Forms.Parameters.SearchForm
{
    public class SearchFilterParameters : SearchFilterParametersBase
    {
		public SearchFilterParameters
		(
			[ParameterEditorControl(ParameterControlType.ParameterSourcedPropertyInput)]
			[NameValue(AttributeNames.PROPERTYSOURCEPARAMETER, "fieldTypeSource")]
			[Comments("Update fieldTypeSource first. This property being searched.")]
			string field,

			[ParameterEditorControl(ParameterControlType.ParameterSourceOnly)]
			[Comments("Fully qualified class name for the model type.")]
			string fieldTypeSource = "Enrollment.Domain.Entities"
		)
		{
			Field = field;
		}

		public string Field { get; set; }
    }
}