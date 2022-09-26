using System;
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

        public bool Equals(TElement? x, TElement? y)
        {
            return this.keyFields.Aggregate
            (
                true,
                (isEqual, next) =>
                {
                    if (!isEqual) return false;

                    PropertyInfo? propertyInfo = typeof(TElement).GetProperty(next);
                    if (propertyInfo == null)
                        throw new ArgumentException($"{next}: {{272E232C-D7FE-4B4F-8E49-C7B046350635}}");

                    return propertyInfo.GetValue(x)?.Equals(propertyInfo.GetValue(y)) == true;
                }
            );
        }

        public int GetHashCode(TElement obj)
        {
            PropertyInfo? propertyInfo = typeof(TElement).GetProperty(keyFields[0]);
            if (propertyInfo == null)
                throw new ArgumentException($"{keyFields[0]}: {{6DEE0142-0000-43A4-A82F-039268B463D6}}");

            object? propertyValue = propertyInfo.GetValue(obj);
            if (propertyValue == null)
                throw new ArgumentException($"{keyFields[0]}: {{1B7BC4BB-563F-4050-A5C3-4862686DBB1D}}");

            return propertyValue.GetHashCode();
        }
    }
}
