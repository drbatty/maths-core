using System.Collections.Generic;
using CSharpExtensionsTests;
using MathsCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathsCoreTests
{
    [TestClass]
    public class PermutationTests
    {
        [TestMethod]
        public static void TestPermutationEquality()
        {
            new Permutation(new List<int> {1, 2, 0}).ShouldEqual(new Permutation(new List<int>{1, 2, 0}));
        }

        [TestMethod]
        public static void TestDefaultConstructor()
        {
            new Permutation().ShouldEqual(new Permutation(new List<int>()));
        }

        [TestMethod]
        public static void TestMultiplicationByIdentity()
        {
            (new Permutation(new List<int> {1, 2, 0}) * new Permutation(new List<int> {0, 1, 2})).ShouldEqual(new Permutation(new List<int> {1, 2, 0}));
        }

        [TestMethod]
        public static void TestGetHashCode()
        {
            var ints = new List<int> {1, 2, 0};
            new Permutation(ints).GetHashCode().ShouldEqual(ints.GetHashCode());
        }

        [TestMethod]
        public static void TestPower()
        {
            new Permutation(new List<int> {1, 2, 0}).Power(2).ShouldEqual(new Permutation(new List<int> {2, 0, 1}));
        }

        [TestMethod]
        public static void TestCycle()
        {
            Permutation.Cycle(3).ShouldEqual(new Permutation(new List<int> {1, 2, 0}));
        }

        [TestMethod]
        public static void TestToString()
        {
            new Permutation(new List<int> {1, 2, 0}).ToString().ShouldEqual("{0->1, 1->2, 2->0}");
        }
    }
}