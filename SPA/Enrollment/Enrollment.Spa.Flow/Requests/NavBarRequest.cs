namespace Enrollment.Spa.Flow.Requests
{
    public class NavBarRequest
    {
        public int UserId { get; set; }
        public string? InitialModuleName { get; set; }
        public int TargetModule { get; set; }
    }
}
