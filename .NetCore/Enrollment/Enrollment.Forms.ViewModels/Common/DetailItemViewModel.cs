using System.Collections.Generic;

namespace Enrollment.Forms.ViewModels.Common
{
    abstract public class DetailItemViewModel
    {
		abstract public DetailItemEnum DetailType { get; set; }
    }
}