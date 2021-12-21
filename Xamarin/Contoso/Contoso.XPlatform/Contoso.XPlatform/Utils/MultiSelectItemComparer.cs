using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Contoso.XPlatform.Utils
{
    public class MultiSelectItemComparer<TElement> : IEqualityComparer<TElement>
    {
        public MultiSelectItemComparer(List<string> keyFields)
        {
            this.keyFields = keyFields;
        }

        readonly List<string> keyFields;

        public bool Equals(TElement x, TElement y)
        {
            return this.keyFields.Aggregate
            (
                true,
                (isEqual, next) =>
                {
                    if (!isEqual) return false;

                    PropertyInfo propertyInfo = typeof(TElement).GetProperty(next);
                    return propertyInfo.GetValue(x)?.Equals(propertyInfo.GetValue(y)) == true;
                }
            );
        }

        public int GetHashCode(TElement obj)
            => typeof(TElement).GetProperty(keyFields[0]).GetValue(obj).GetHashCode();
    }
}
