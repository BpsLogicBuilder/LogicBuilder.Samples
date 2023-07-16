using Enrollment.Parameters.Expansions;
using LogicBuilder.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace Enrollment.Forms.Parameters.Common
{
    public class RequestDetailsParameters
    {
        public RequestDetailsParameters()
        {
        }

        public RequestDetailsParameters
        (
            [ParameterEditorControl(ParameterControlType.TypeAutoComplete)]
            [NameValue(AttributeNames.DEFAULTVALUE, "Enrollment.Domain.Entities")]
            [Comments("The model element type for the queryable i.e. T in IQueryable<T>. Click the function button and use the configured GetType function.  Use the Assembly qualified type name for the type argument.")]
            string modelType,

            [ParameterEditorControl(ParameterControlType.TypeAutoComplete)]
            [NameValue(AttributeNames.DEFAULTVALUE, "Enrollment.Data.Entities")]
            [Comments("The data element type for the queryable i.e. T in IQueryable<T>. Click the function button and use the configured GetType function.  Use the Assembly qualified type name for the type argument.")]
            string dataType,

            [ParameterEditorControl(ParameterControlType.TypeAutoComplete)]
            [NameValue(AttributeNames.DEFAULTVALUE, "Enrollment.Data.Entities")]
            [Comments("The model collection type being returned e.g. IEnumerable<StudentModel>. Click the function button and use the configured GetType function.  Auto complete to System.Collections.Generic.IEnumerable`1.  Finally right-click and select 'Add/Update Generic Arguments'")]
            string modelReturnType,

            [ParameterEditorControl(ParameterControlType.TypeAutoComplete)]
            [NameValue(AttributeNames.DEFAULTVALUE, "Enrollment.Data.Entities")]
            [Comments("The data collection type being returned e.g. IEnumerable<Student>. Click the function button and use the configured GetType function.  Auto complete to System.Collections.Generic.IEnumerable`1.  Finally right-click and select 'Add/Update Generic Arguments'")]
            string dataReturnType,

            string dataSourceUrl = "/api/List/GetList",

            [Comments("Defines and navigation properties to include in the edit model")]
            SelectExpandDefinitionParameters selectExpandDefinition = null
        )
        {
            ModelType = modelType;
            DataType = dataType;
            ModelReturnType = modelReturnType;
            DataReturnType = dataReturnType;
            DataSourceUrl = dataSourceUrl;
            SelectExpandDefinition = selectExpandDefinition;
        }

        public string ModelType { get; set; }
        public string DataType { get; set; }
        public string ModelReturnType { get; set; }
        public string DataReturnType { get; set; }
        public string DataSourceUrl { get; set; }
        public SelectExpandDefinitionParameters SelectExpandDefinition { get; set; }
    }
}