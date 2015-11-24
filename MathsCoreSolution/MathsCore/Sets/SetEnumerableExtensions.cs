using System.Collections.Generic;
using System.Linq;
using CSharpExtensions.ContainerClasses;
using MathsCore.Extensions.ContainerClasses;
using MathsCore.SimplicialComplex;

namespace MathsCore.Sets
{
    /// <summary>
    /// extensions which work on enumerables of sets
    /// </summary>
    public static class SetEnumerableExtensions
    {
        /// <summary>
        /// forms the union of a collection of sets
        /// </summary>
        /// <typeparam name="T">the type of object contained in the sets</typeparam>
        /// <param name="sets">the collection of sets whose union to form</param>
        /// <returns>the union of the given collection of sets</returns>
        public static Set<T> Union<T>(this IEnumerable<Set<T>> sets)
        // SelectMany may be quicker - also can use this for the set of vertices of a simplicial complex.
        {
            return sets.Inject(Set<T>.Empty, (p, q) => p | q);
        }

        /// <summary>
        /// forms the intersection of a collection of sets
        /// </summary>
        /// <typeparam name="T">the type of object contained in the sets</typeparam>
        /// <param name="sets">the collection of sets whose intersection to form</param>
        /// <returns>the intersection of the given collection of sets</returns>
        public static Set<T> Intersection<T>(this IEnumerable<Set<T>> sets)
        {
            var enumerable = sets as IList<Set<T>> ?? sets.ToList();
            return enumerable.None() ? Set<T>.Empty : enumerable.Inject(enumerable.First(), (p, q) => p & q);
        }

        public static Set<Set<TVertexType>> Skeleton<TVertexType>(this IEnumerable<Set<TVertexType>> complex,
            int n)
        {
            return complex.Where(s => s.Count() <= n).ToSet();
        }

        public static Set<Set<TVertexType>> Cells<TVertexType>(this IEnumerable<Set<TVertexType>> complex, int n)
        {
            return complex.Where(s => s.Count() == n).ToSet();
        }

        public static Set<TVertexType> Vertices<TVertexType>(this IEnumerable<Set<TVertexType>> complex)
        {
            return complex.Cells(1).SelectMany(c => c).ToSet();
        }

        public static Set<Set<TVertexType>> Edges<TVertexType>(this IEnumerable<Set<TVertexType>> complex)
        {
            return complex.Cells(2).ToSet();
        }

        public static int Order<TVertexType>(this IEnumerable<Set<TVertexType>> complex)
        {
            return complex.Vertices().Count;
        }

        public static int Size<TVertexType>(this IEnumerable<Set<TVertexType>> complex)
        {
            return complex.Edges().Count;
        }

        public static SimplicialComplex<Set<T>> IntersectionGraph<T>(this IEnumerable<Set<T>> sets) 
        {
            var result = new SimplicialComplex<Set<T>>();
            var enumerable = sets as IList<Set<T>> ?? sets.ToList();
            enumerable.Each(s => result.Add(new Set<Set<T>> { s }));
            enumerable.Each(s => enumerable.Each(t =>
            {
                if (s.Meets(t))
                    result.Add(new Set<Set<T>> { s, t });
            }));
            return result;
        }

        public static IEnumerable<Set<T>> KneserSet<T>(this Set<T> set, int n)
        {
            return set.PowerSet().Where(s => s.Count == n);
        }
    }
}
