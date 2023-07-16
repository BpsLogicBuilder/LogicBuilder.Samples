namespace Contoso.Spa.Flow.Options
{
    public class InitialOptions
    {
        public string InitialModule { get; set; } = "initial";
        public int TargetModule { get; set; } = Cache.TargetModules.Home;
    }
}
