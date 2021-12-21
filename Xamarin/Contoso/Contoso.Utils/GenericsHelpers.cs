using LogicBuilder.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Contoso.Utils
{
    public static class GenericsHelpers<T>
    {
        [AlsoKnownAs("ToList")]
        [FunctionGroup(FunctionGroup.Standard)]
        public static List<T> ToList(IEnumerable<T> enumerable) => enumerable.ToList();

        [AlsoKnownAs("Single")]
        [FunctionGroup(FunctionGroup.Standard)]
        public static T Single(IEnumerable<T> enumerable) => enumerable.Single();

        [AlsoKnownAs("SingleOrDefault")]
        [FunctionGroup(FunctionGroup.Standard)]
        public static T SingleOrDefault(IEnumerable<T> enumerable) => enumerable.SingleOrDefault();

        [AlsoKnownAs("GetItemAtIndex")]
        [FunctionGroup(FunctionGroup.Standard)]
        public static T GetItemAtIndex(IEnumerable<T> enumerable, int index) => enumerable.ElementAt(index);

        [AlsoKnownAs("AddItem")]
        [FunctionGroup(FunctionGroup.Standard)]
        public static void AddItem(ICollection<T> collection, T item) => collection.Add(item);

        [AlsoKnownAs("CreateInstance")]
        public static T CreateInstance() => Activator.CreateInstance<T>();

        [AlsoKnownAs("IsDefault")]
        public static bool IsDefault(T anyObject) => anyObject.Equals(default(T));

        [AlsoKnownAs("Any")]
        [FunctionGroup(FunctionGroup.Standard)]
        public static bool Any(IEnumerable<T> enumerable) => enumerable?.Any() == true;
    }
}
