using System;
using System.Linq;
using CSharpExtensions;
using CSharpExtensions.ContainerClasses;
using MathsCore.Extensions.ContainerClasses;

namespace MathsCore.Sets
{
    public static class BinaryRelationExtensions
    {
        public static Set<S> Image<T, S>(this Set<Tuple<T, S>> pairs)
        {
            return pairs.Select(pair => pair.Item2).ToSet();
        }

        public static Set<T> InverseImage<T, S>(this Set<Tuple<T, S>> pairs)
        {
            return pairs.Select(pair => pair.Item1).ToSet();
        }

        public static Set<S> Image<T, S>(this Set<Tuple<T, S>> pairs, Set<T> ts)
        {
            return pairs.Where(pair => ts.Contains(pair.Item1)).Select(pair => pair.Item2).ToSet();
        }

        public static Set<T> InverseImage<T, S>(this Set<Tuple<T, S>> pairs, Set<S> ss)
        {
            return pairs.Where(pair => ss.Contains(pair.Item2)).Select(pair => pair.Item1).ToSet();
        }

        public static Set<S> Image<T, S>(this Set<Tuple<T, S>> pairs, T t)
        {
            return pairs.Image(t.WrapInList().ToSet());
        }

        public static Set<T> InverseImage<T, S>(this Set<Tuple<T, S>> pairs, S s)
        {
            return pairs.InverseImage(s.WrapInList().ToSet());
        }

        public static bool IsMap<T, S>(this Set<Tuple<T, S>> pairs)
        {
            return pairs.All(pair => pairs.Image(pair.Item1).Count == 1);
        }

        public static bool IsEquivalenceRelation<T>(this Set<Tuple<T, T>> pairs)
        {
            return pairs.IsReflexive() && pairs.IsSymmetric() && pairs.IsTransitive();
        }

        public static bool IsReflexive<T>(this Set<Tuple<T, T>> pairs)
        {
            return pairs.Select(pair => pair.Item1).Union(pairs.Select(pair => pair.Item2)).All(t => pairs.Contains(new Tuple<T, T>(t, t)));
        }

        public static bool IsSymmetric<T>(this Set<Tuple<T, T>> pairs)
        {
            return pairs.All(pair => pairs.Contains(new Tuple<T, T>(pair.Item2, pair.Item1)));
        }

        public static bool IsTransitive<T>(this Set<Tuple<T, T>> pairs)
        {
            return pairs.AllPairs((pair1, pair2) => !pair1.Item2.Equals(pair2.Item1) || pairs.Contains(new Tuple<T, T>(pair1.Item1, pair2.Item2)));
        }

        public static bool IsSurjective<T, S>(this Set<Tuple<T, S>> pairs, Set<S> codomain)
        {
            return IsMap(pairs) && pairs.Image().Contains(codomain);
        }

        public static bool IsInjective<T, S>(this Set<Tuple<T, S>> pairs)
        {
            return IsMap(pairs) && pairs.Image().All(s => pairs.InverseImage(s).Count == 1);
        }

        public static bool IsBijective<T, S>(this Set<Tuple<T, S>> pairs, Set<S> codomain)
        {
            return pairs.IsSurjective(codomain) && pairs.IsInjective();
        }
    }
}