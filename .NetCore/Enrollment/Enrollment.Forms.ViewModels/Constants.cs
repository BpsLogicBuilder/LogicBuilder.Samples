using System;
using System.Collections.Generic;
using System.Text;

namespace Enrollment.Forms.ViewModels
{
    [Flags]
    public enum ViewModelResult
    {
        Success = 0,
        Errors = 1,
        ValidationMessages = 2,
        Warnings = 4,
        Information = 8
    }
}
