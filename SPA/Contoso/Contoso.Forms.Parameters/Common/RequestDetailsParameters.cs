using Contoso.Parameters.Expansions;
using LogicBuilder.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace Contoso.Forms.Parameters.Common
{
    public class RequestDetailsParameters
    {
        public RequestDetailsParameters()
        {
        }

        public RequestDetailsParameters
        (
            [ParameterEditorControl(ParameterControlType.TypeAutoComplete)]
            [NameValue(AttributeNames.DEFAULTVALUE, "Contoso.Domain.Entities")]
            [Comments("The model element type for the queryable i.e. T in IQueryable<T>. Click the function button and use the configured GetType function.  Use the Assembly qualified type name for the type argument.")]
            string modelType,

            [ParameterEditorControl(ParameterControlType.TypeAutoComplete)]
            [NameValue(AttributeNames.DEFAULTVALUE, "Contoso.Data.Entities")]
            [Comments("The data element type for the queryable i.e. T in IQueryable<T>. Click the function button and use the configured GetType function.  Use the Assembly qualified type name for the type argument.")]
            string dataType,

            [ParameterEditorControl(ParameterControlType.TypeAutoComplete)]
            [NameValue(AttributeNames.DEFAULTVALUE, "Contoso.Data.Entities")]
            [Comments("The model collection type being returned e.g. IEnumerable<StudentModel>. Click the function button and use the configured GetType function.  Auto complete to System.Collections.Generic.IEnumerable`1.  Finally right-click and select 'Add/Update Generic Arguments'")]
            string modelReturnType,

            [ParameterEditorControl(ParameterControlType.TypeAutoComplete)]
            [NameValue(AttributeNames.DEFAULTVALUE, "Contoso.Data.Entities")]
            [Comments("The data collection type being returned e.g. IEnumerable<Student>. Click the function button and use the configured GetType function.  Auto complete to System.Collections.Generic.IEnumerable`1.  Finally right-click and select 'Add/Update Generic Arguments'")]
            string dataReturnType,

            string dataSourceUrl = "/api/Generic/GetData",
            string getUrl = "/api/Generic/GetSingle",
            string addUrl = "/api/Generic/Add",
            string updateUrl = "/api/Generic/Update",
            string deleteUrl = "/api/Generic/Delete",

            [ListEditorControl(ListControlType.HashSetForm)]
            [ParameterEditorControl(ParameterControlType.ParameterSourcedPropertyInput)]
            [NameValue(AttributeNames.PROPERTYSOURCEPARAMETER, "modelType")]
            string[] includes = null,

            [ListEditorControl(ListControlType.HashSetForm)]
            List<SelectParameters> selects = null,
            bool? distinct = null,

            [Comments("Defines and navigation properties to include in the edit model")]
            SelectExpandDefinitionParameters selectExpandDefinition = null
        )
        {
            ModelType = modelType;
            DataType = dataType;
            ModelReturnType = modelReturnType;
            DataReturnType = dataReturnType;
            DataSourceUrl = dataSourceUrl;
            GetUrl = getUrl;
            AddUrl = addUrl;
            UpdateUrl = updateUrl;
            DeleteUrl = deleteUrl;
            Includes = includes;
            Selects = selects?.ToDictionary(s => s.FieldName, s => s.SourceMember);
            Distinct = distinct;
            SelectExpandDefinition = selectExpandDefinition;
        }

        public string ModelType { get; set; }
        public string DataType { get; set; }
        public string ModelReturnType { get; set; }
        public string DataReturnType { get; set; }
        public string DataSourceUrl { get; set; }
        public string GetUrl { get; set; }
        public string AddUrl { get; set; }
        public string UpdateUrl { get; set; }
        public string DeleteUrl { get; set; }
        public string[] Includes { get; set; }
        public Dictionary<string, string> Selects { get; set; }
        public bool? Distinct { get; set; }
        public SelectExpandDefinitionParameters SelectExpandDefinition { get; set; }
    }
}