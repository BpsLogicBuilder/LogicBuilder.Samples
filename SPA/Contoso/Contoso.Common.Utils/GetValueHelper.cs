using LogicBuilder.Attributes;
using System.Collections.Generic;

namespace Contoso.Common.Utils
{
    public static class GetValueHelper<TKey, TValue>
    {
        [AlsoKnownAs("GetValue")]
        [FunctionGroup(FunctionGroup.Standard)]
        public static TValue GetValue(IDictionary<TKey, TValue> dictionary, TKey key)
        {
            dictionary.TryGetValue(key, out TValue value);
            return value;
        }
    }
}
