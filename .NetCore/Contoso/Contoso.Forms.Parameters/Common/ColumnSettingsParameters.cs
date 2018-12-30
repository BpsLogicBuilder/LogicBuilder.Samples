using LogicBuilder.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contoso.Forms.Parameters.Common
{
    public class ColumnSettingsParameters
    {
        public ColumnSettingsParameters()
        {
        }

        public ColumnSettingsParameters
        (
            [Comments("Update modelType first. Source property name from the target object. Use a period for nested fields i.e. foo.bar.")]
            [ParameterEditorControl(ParameterControlType.ParameterSourcedPropertyInput)]
            [NameValue(AttributeNames.PROPERTYSOURCEPARAMETER, "modelType")]
            string field,

            [Comments("Column title.")]
            string title,

            [Comments("text/numeric/boolean/date")]
            [Domain("text,numeric,boolean,date")]
            [ParameterEditorControl(ParameterControlType.DropDown)]
            string type,

            bool? groupable = null,
            int? width = null,
            string format = null,

            [Domain("text,numeric,boolean,date")]
            [ParameterEditorControl(ParameterControlType.DropDown)]
            string filter = null,

            [Comments("HTML template for the cell.")]
            CellTemplateParameters cellTemplate = null,

            [Comments("HTML template for the cell flor list content.")]
            CellListTemplateParameters cellListTemplate = null,

            [Comments("HTML template which can be applied when the grid's filterable type is 'row' or 'menu, row'.")]
            FilterTemplateParameters filterRowTemplate = null,

            [Comments("HTML template which can be applied when the grid's filterable type is 'menu' or 'menu, row'.")]
            FilterTemplateParameters filterMenuTemplate = null,

            [Comments("Group header template.")]
            AggregateTemplateParameters groupHeaderTemplate = null,

            [Comments("Group footer template.")]
            AggregateTemplateParameters groupFooterTemplate = null,

            [Comments("Grid footer template.")]
            AggregateTemplateParameters gridFooterTemplate = null,



            [ParameterEditorControl(ParameterControlType.ParameterSourceOnly)]
            [NameValue(AttributeNames.DEFAULTVALUE, "Contoso.Domain.Entities")]
            [Comments("Fully qualified class name for the model type.")]
            string modelType = null
        )
        {
            Field = field;
            Title = title;
            Type = type;
            Groupable = groupable;
            Width = width;
            Format = format;
            Filter = filter;
            CellTemplate = cellTemplate;
            CellListTemplate = cellListTemplate;
            FilterRowTemplate = filterRowTemplate;
            FilterMenuTemplate = filterMenuTemplate;
            GroupHeaderTemplate = groupHeaderTemplate;
            GroupFooterTemplate = groupFooterTemplate;
            GridFooterTemplate = gridFooterTemplate;
        }

        public string Field { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public bool? Groupable { get; set; }
        public int? Width { get; set; }
        public string Format { get; set; }
        public string Filter { get; set; }
        public CellTemplateParameters CellTemplate { get; set; }
        public CellListTemplateParameters CellListTemplate { get; set; }
        public FilterTemplateParameters FilterRowTemplate { get; set; }
        public FilterTemplateParameters FilterMenuTemplate { get; set; }
        public AggregateTemplateParameters GroupHeaderTemplate { get; set; }
        public AggregateTemplateParameters GroupFooterTemplate { get; set; }
        public AggregateTemplateParameters GridFooterTemplate { get; set; }
    }
}
