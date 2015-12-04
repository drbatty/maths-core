using System;
using MathsCore.LinearAlgebra;
using MathsCore.Sets;

namespace MathsCore.Extensions
{
    public static class RandomExtensions
    {
        public static Vector<TBasis, double> NextVector<TBasis>(this Random random, Set<TBasis> enumerable)
        {
            return new Vector<TBasis, double>(enumerable.ToDictionary(item => random.NextDouble()));
        }
    }
}