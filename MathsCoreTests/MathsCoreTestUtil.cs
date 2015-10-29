using System.Collections.Generic;
using CSharpExtensionsTests;
using MathsCore.Extensions.ContainerClasses;
using MathsCore.Interfaces;

namespace MathsCoreTests
{
    public static class MathsCoreTestUtil
    {
        public static void ShouldBeRecognisedAs(this IRecognisable recognisable, string description)
        {
            recognisable.Recognise().ShouldEqual(description);
        }

        #region dictionaries

        public static void ShouldBeEquivalentMappingTo<TKey, TValue>(this IDictionary<TKey, TValue> dictionary,
            IDictionary<TKey, TValue> other)
        {
            dictionary.IsEquivalentMappingTo(other).ShouldBeTrue();
        }

        #endregion
    }
}