using System.Collections.Generic;

namespace Contoso.Forms.View.Input
{
    public class InputColumnView
    {
        public int Id { get; set; }

        #region ColumnData
        public string Title { get; set; }
        public bool ShowTitle { get; set; }
        public string ColumnShare { get; set; }
        public string ToolTipText { get; set; }
        #endregion ColumnData

        public ICollection<BaseInputView> Questions { get; set; }
        public ICollection<InputRowView> Rows { get; set; }
    }
}
