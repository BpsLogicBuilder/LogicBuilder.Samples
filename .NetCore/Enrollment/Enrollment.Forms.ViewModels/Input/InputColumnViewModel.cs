using System;
using System.Collections.Generic;
using System.Text;

namespace Enrollment.Forms.ViewModels.Input
{
    public class InputColumnViewModel
    {
        public int Id { get; set; }

        #region ColumnData
        public string Title { get; set; }
        public bool ShowTitle { get; set; }
        public string ColumnShare { get; set; }
        public string ToolTipText { get; set; }
        #endregion ColumnData

        public ICollection<BaseInputViewModel> Questions { get; set; }
        public ICollection<InputRowViewModel> Rows { get; set; }
    }
}
