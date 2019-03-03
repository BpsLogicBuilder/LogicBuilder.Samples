using LogicBuilder.Attributes;

namespace CheckMySymptoms.Forms.Parameters
{
    public class RowDataParameters
    {
        public RowDataParameters(
            [NameValue(AttributeNames.DEFAULTVALUE, "Row Title")]
            string Title,
            [NameValue(AttributeNames.DEFAULTVALUE, "true")]
            bool ShowTitle,
            string ToolTipText)
        {
            this.Title = Title;
            this.ShowTitle = ShowTitle;
            this.ToolTipText = ToolTipText;
        }

        public RowDataParameters()
        {
        }

        public string Title { get; set; }
        public bool ShowTitle { get; set; }
        public string ToolTipText { get; set; }
    }
}
