using System.Collections.Generic;
using MathsCore.Sets;

namespace MathsCore.Extensions.ContainerClasses
{
    public static class EnumerableSetExtensions
    {
        /// <summary>
        /// converts an IEnumerable to a set
        /// </summary>
        /// <typeparam name="T">the type enumerated by the IEnumerable</typeparam>
        /// <param name="enumerable">the IEnumerable to convert</param>
        /// <returns>a set containing the same elements as the IEnumerable enumerates</returns>
        public static Set<T> ToSet<T>(this IEnumerable<T> enumerable)
        {
            return new Set<T>(enumerable);
        }
    }
}