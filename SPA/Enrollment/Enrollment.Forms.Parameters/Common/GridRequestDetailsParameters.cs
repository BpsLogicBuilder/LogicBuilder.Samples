using Enrollment.Parameters.Expansions;
using LogicBuilder.Attributes;

namespace Enrollment.Forms.Parameters.Common
{
    public class GridRequestDetailsParameters
    {
        public GridRequestDetailsParameters
        (
                [ParameterEditorControl(ParameterControlType.TypeAutoComplete)]
                [NameValue(AttributeNames.DEFAULTVALUE, "Enrollment.Domain.Entities")]
                [Comments("Fully qualified class name for the model type.")]
                string modelType,

                [ParameterEditorControl(ParameterControlType.TypeAutoComplete)]
                [NameValue(AttributeNames.DEFAULTVALUE, "Enrollment.Data.Entities")]
                [Comments("Fully qualified class name for the data type.")]
                string dataType,

                string dataSourceUrl = "/api/Generic/GetData",

                [Comments("Defines and navigation properties to include in the edit model")]
                SelectExpandDefinitionParameters selectExpandDefinition = null
            )
        {
            ModelType = modelType;
            DataType = dataType;
            DataSourceUrl = dataSourceUrl;
            SelectExpandDefinition = selectExpandDefinition;
        }

        public string ModelType { get; set; }
        public string DataType { get; set; }
        public string DataSourceUrl { get; set; }
        public SelectExpandDefinitionParameters SelectExpandDefinition { get; set; }
    }
}
