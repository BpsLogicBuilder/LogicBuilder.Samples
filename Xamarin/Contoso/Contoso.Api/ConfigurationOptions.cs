using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contoso.Api
{
    public class ConfigurationOptions
    {
        #region Constants
        public const string DEFAULTBASEBSLURL = "http://localhost:55688/api";
        #endregion Constants

        #region Properties
        public string BaseBslUrl { get; set; }
        #endregion Properties
    }
}
