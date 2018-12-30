using System;
using System.Collections.Generic;
using System.Text;

namespace Contoso.Web.Flow.ScreenSettings
{
    public class FlowState
    {
        public string Driver { get; set; } = string.Empty;
        public string Selection { get; set; } = string.Empty;
        public List<string> CallingModuleDriverStack { get; set; } = new List<string>();
        public List<string> CallingModuleStack { get; set; } = new List<string>();
        public string ModuleBeginName { get; set; } = string.Empty;
        public string ModuleEndName { get; set; } = string.Empty;
        public bool DialogClosed { get; set; }
    }
}
