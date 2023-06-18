namespace Enrollment.Domain.Entities
{
    public class RulesModuleModel
    {
        public string Name { get; set; }
        public byte[] RuleSetFile { get; set; }
        public byte[] ResourceSetFile { get; set; }
    }
}
