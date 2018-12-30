using System;
using System.Collections.Generic;
using System.Text;

namespace Enrollment.Forms.ViewModels.Input
{
    public class InputRowViewModel
    {
        public int Id { get; set; }

        #region RowData
        public string Title { get; set; }
        public bool ShowTitle { get; set; }
        public string ToolTipText { get; set; }
        #endregion RowData

        public ICollection<InputColumnViewModel> Columns { get; set; }
    }
}
