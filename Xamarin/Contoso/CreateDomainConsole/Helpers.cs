using Contoso.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Contoso.Domain
{
    internal static class Helpers
    {
        private static HashSet<Type> ValidGenericListTypes => new HashSet<Type>(new Type[]
        {
            typeof(List<>),
            typeof(IList<>),
            typeof(Collection<>),
            typeof(ICollection<>),
            typeof(IEnumerable<>)
        });

        public static bool IsValidList(this Type type)
        {
            return
            (
                (type.IsGenericType && ValidGenericListTypes.Contains(type.GetGenericTypeDefinition()))
                ||
                (type.IsArray && type.GetArrayRank() == 1)
            );
        }
    }
}
