using System.Collections.Generic;
using System.Linq;
using CSharpExtensions.ContainerClasses;
using MathsCore.Extensions.ContainerClasses;

namespace MathsCore.LinearAlgebra
{
    public static class EnumerableVectorExtensions
    {
        public static Vector<T, double> ToVector<T>(this IEnumerable<T> enumerable)
        {
            var result = new Vector<T, double>();
            var list = enumerable.ToList();
            var contained = list.ToSet();
            contained.Each(c => result[c] = list.Count(e => e.Equals(c)));
            return result;
        }
    }
}