using System;
using System.Collections.Generic;
using System.Text;

namespace Enrollment.Web.Flow.Options
{
    public class ApplicationOptions
    {
        public ApplicationOptions()
        {
            this.ApplicationName = DEFAULTAPPLICATIONNAME;
        }

        #region Constants
        public const string DEFAULTAPPLICATIONNAME = "App01";
        #endregion Constants

        #region Properties
        public string ApplicationName { get; set; }
        #endregion Properties
    }
}
