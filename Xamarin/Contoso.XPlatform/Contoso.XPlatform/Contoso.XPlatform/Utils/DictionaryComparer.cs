using LogicBuilder.Expressions.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Contoso.XPlatform.Utils
{
    public class DictionaryComparer : IEqualityComparer<Dictionary<string, object>>
    {
        public bool Equals(Dictionary<string, object> x, Dictionary<string, object> y)
        {
            if (x == null && y == null)
                return true;

            if (x == null ^ y == null)
                return false;

            if (x.Count != y.Count)
                return false;

            if (x.Keys.Except(y.Keys).Any())
                return false;

            if (y.Keys.Except(x.Keys).Any())
                return false;

            foreach (var pair in x)
            {
                if (pair.Value == null)
                {
                    if (y[pair.Key] != null)
                        return false;
                    else
                        continue;
                }
                else if (pair.Value.GetType().IsLiteralType())
                {
                    if (!pair.Value.Equals(y[pair.Key]))
                        return false;
                }
                else if (pair.Value.GetType() == typeof(Dictionary<string, object>))
                {
                    if (!new DictionaryComparer().Equals((Dictionary<string, object>)pair.Value, (Dictionary<string, object>)y[pair.Key]))
                        return false;
                }
                else if (typeof(IEnumerable<Dictionary<string, object>>).IsAssignableFrom(pair.Value.GetType()))
                {
                    var xToHashSet = ((IEnumerable<Dictionary<string, object>>)pair.Value)
                        .ToHashSet(new DictionaryComparer());

                    if  (!xToHashSet.SetEquals((IEnumerable<Dictionary<string, object>>)y[pair.Key]))
                        return false;
                }
                else if (pair.Value.GetType().IsList())
                {
                    Type pairType = pair.Value.GetType();
                    Type elementType = pairType.GetUnderlyingElementType();
                    if (elementType.IsLiteralType())
                    {
                        if (!ListHelper.ListEquals(elementType, (System.Collections.IEnumerable)pair.Value, (System.Collections.IEnumerable)y[pair.Key]))
                            return false;
                    }
                }
                else
                {
                    throw new ArgumentException($"{nameof(pair)}: E7AFB1A2-E3A9-4EC0-9AAD-99E368A76870");
                }
            }

            return true;
        }

        public int GetHashCode(Dictionary<string, object> obj)
        {
            if (obj == null)
                return string.Empty.GetHashCode();

            return string.Join
            (
                "|", 
                obj.Keys.OrderBy(key => key)
            ).GetHashCode();
        }
    }
}
