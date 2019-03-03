using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMySymptoms.Forms.View.Input
{
    public class InputRowView
    {
        public int Id { get; set; }

        #region RowData
        public string Title { get; set; }
        public bool ShowTitle { get; set; }
        public string ToolTipText { get; set; }
        #endregion RowData

        public ICollection<InputColumnView> Columns { get; set; }
    }
}
