using System;
using System.Collections.Generic;
using System.Text;

namespace Contoso.Web.Flow.Options
{
    public class InitialOptions
    {
        #region Properties
        public string InitialModule { get; set; } = "initial";
        public int TargetModule { get; set; } = Cache.TargetModules.Home;
        #endregion Properties
    }
}
