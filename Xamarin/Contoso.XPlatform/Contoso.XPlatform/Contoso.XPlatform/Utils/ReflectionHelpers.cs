using System;
using System.Collections.Generic;
using System.Linq;

namespace Contoso.XPlatform.Utils
{
    internal static class ReflectionHelpers
    {
        /// <summary>
        /// Using typeof(IQueryable<>).MakeGenericType(elementType).AssemblyQualifiedName returns the type
        /// name for the Xamarin platform e.g.
        /// "System.Linq.IQueryable`1[[{elementType.AssemblyQualifiedName}]], System.Core, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e"
        /// which won't work on the server.
        /// </summary>
        /// <param name="elementType"></param>
        /// <returns></returns>
        internal static string GetIQueryableTypeString(this Type elementType)
            => typeof(IQueryable<>).MakeGenericType(elementType).AssemblyQualifiedName;
        //wrong about this
        //=> $"System.Linq.IQueryable`1[[{elementType.AssemblyQualifiedName}]], System.Linq.Expressions, Version=4.2.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

        /// <summary>
        /// Using typeof(IEnumerable<>).MakeGenericType(elementType).AssemblyQualifiedName returns the type
        /// name for the Xamarin platform which does work e.g.
        /// "System.Collections.Generic.IEnumerable`1[[{elementType.AssemblyQualifiedName}]], mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e"
        /// because the PublicKeyToken tokens are the same. This method added for completeness.
        /// </summary>
        /// <param name="elementType"></param>
        /// <returns></returns>
        internal static string GetIEnumerableTypeString(this Type elementType)
            => typeof(IEnumerable<>).MakeGenericType(elementType).AssemblyQualifiedName;
            //wrong about this
            //=> $"System.Collections.Generic.IEnumerable`1[[{elementType.AssemblyQualifiedName}]], System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e";
    }
}
