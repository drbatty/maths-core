using System.Collections.Generic;
using CSharpExtensionsTests;
using MathsCore.Sets;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathsCoreTests.Sets
{
    [TestClass]
    public class SetEnumerableExtensionsTests
    {
        [TestMethod]
        public void UnionShouldGiveCorrectResults()
        {
            new List<Set<int>> {new Set<int>{1, 2, 3}, new Set<int>{2, 3, 4}, new Set<int>{3, 4, 5}}
                .Union().ShouldContainExactly(1, 2, 3, 4, 5);
        }

        [TestMethod]
        public void IntersectionShouldGiveCorrectResults()
        {
            new List<Set<int>> {new Set<int>{ 1, 2, 3 }, new Set<int> {2, 3, 4}, new Set<int> {3, 4, 5}}
                .Intersection().ShouldContainExactly(3);
            new List<Set<int>>().Intersection().ShouldBeEmpty();
        }
    }
}