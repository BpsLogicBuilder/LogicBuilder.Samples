﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Enrollment.XPlatform.Utils
{
    public static class ListHelper
    {
        public static bool ExistsInList(this Dictionary<string, object?> source, IEnumerable<Dictionary<string, object?>> sourceList, List<string> keyFields)
            => GetByKey(source, sourceList, keyFields).SingleOrDefault() != null;

        public static Dictionary<string, object?>? GetExistingEntry(this Dictionary<string, object?> source, IEnumerable<Dictionary<string, object?>> sourceList, List<string> keyFields)
            => GetByKey(source, sourceList, keyFields).SingleOrDefault();

        public static bool ListEquals(Type elementType, System.Collections.IEnumerable first, System.Collections.IEnumerable second)
        {
            MethodInfo methodInfo = (
                typeof(ListHelper).GetMethod
                (
                    nameof(_ListEquals), BindingFlags.NonPublic | BindingFlags.Static
                ) ?? throw new ArgumentException($"{nameof(_ListEquals)}: {{7C9C1784-FBB5-488A-8F34-30A69E10E216}}")
            ).MakeGenericMethod(elementType);


            return (bool)methodInfo.Invoke(null, new object[] { first, second })!;/*_ListEquals returns a boolean*/
        }

        private static bool _ListEquals<T>(IEnumerable<T> first, IEnumerable<T> second)
        {
            return first.SequenceEqual<T>(second);
        }

        private static IEnumerable<Dictionary<string, object?>> GetByKey(Dictionary<string, object?> source, IEnumerable<Dictionary<string, object?>> sourceList, List<string> keyFields)
        {
            if (sourceList?.Any() != true)
                return new List<Dictionary<string, object?>>();

            return keyFields.Aggregate
            (
                sourceList,
                (list, propertyName) => list.Where(item => item[propertyName]?.Equals(source[propertyName]) == true)
            );
        }
    }
}
