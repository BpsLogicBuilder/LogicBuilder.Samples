namespace Enrollment.Api
{
    public class ConfigurationOptions
    {
        #region Constants
        public const string DEFAULTBASEBSLURL = "http://localhost:61619/api";
        #endregion Constants

        #region Properties
        public string BaseBslUrl { get; set; }
        #endregion Properties
    }
}
