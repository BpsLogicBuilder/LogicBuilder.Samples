using Contoso.Forms.Configuration.Navigation;
using System.Collections.Generic;

namespace Contoso.XPlatform.Flow.Cache
{
    public class FlowDataCache
    {
        public List<string> PersistentKeys { get; set; } = new List<string>();
        public Dictionary<string, object> Items { get; set; } = new Dictionary<string, object>();
        public NavigationBarDescriptor NavigationBar { get; set; } = new NavigationBarDescriptor();
    }
}
