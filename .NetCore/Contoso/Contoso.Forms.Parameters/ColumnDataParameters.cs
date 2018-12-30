using LogicBuilder.Attributes;

namespace Contoso.Forms.Parameters
{
    public class ColumnDataParameters
    {
        public ColumnDataParameters(
            [NameValue(AttributeNames.DEFAULTVALUE, "Column Title")]
            string Title,
            [NameValue(AttributeNames.DEFAULTVALUE, "true")]
            bool ShowTitle,
            [NameValue(AttributeNames.DEFAULTVALUE, "col-md-6")]
            string ColumnShare,
            string ToolTipText)
        {
            this.Title = Title;
            this.ShowTitle = ShowTitle;
            this.ColumnShare = ColumnShare;
            this.ToolTipText = ToolTipText;
        }

        public ColumnDataParameters()
        {
        }

        public string Title { get; set; }
        public bool ShowTitle { get; set; }
        public string ColumnShare { get; set; }
        public string ToolTipText { get; set; }
    }
}
