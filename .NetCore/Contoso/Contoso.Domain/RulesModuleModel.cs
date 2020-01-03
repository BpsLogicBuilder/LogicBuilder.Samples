using System;
using System.Collections.Generic;
using System.Text;

namespace Contoso.Domain.Entities
{
    public class RulesModuleModel : BaseModelClass
    {
        public int RulesModuleId { get; set; }

        public string Name { get; set; }

        public string Application { get; set; }

        public byte[] RuleSetFile { get; set; }

        public byte[] ResourceSetFile { get; set; }

        public string LoggedInUserId { get; set; }

        public System.DateTime LastUpdated { get; set; }

        public string NamePlusApplication { get; set; }
    }
}
