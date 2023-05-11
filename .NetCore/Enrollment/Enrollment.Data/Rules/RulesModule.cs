using System;

namespace Enrollment.Data.Rules
{
    public class RulesModule : BaseDataClass
    {
        public int RulesModuleId { get; set; }

        public string Name { get; set; }

        public string Application { get; set; }

        public byte[] RuleSetFile { get; set; }

        public byte[] ResourceSetFile { get; set; }

        public string LoggedInUserId { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}
