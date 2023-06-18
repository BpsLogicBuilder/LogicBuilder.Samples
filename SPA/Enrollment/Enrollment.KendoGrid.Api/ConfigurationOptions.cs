namespace Enrollment.KendoGrid.Api
{
    public class ConfigurationOptions
    {
        #region Constants
        public const string DEFAULTBASEBSLURL = "http://localhost:53345/api";
        #endregion Constants

        #region Properties
        public string BaseBslUrl { get; set; } = DEFAULTBASEBSLURL;
        #endregion Properties
    }
}
