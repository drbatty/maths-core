using System;
using System.Collections.Generic;
using System.Linq;
using CSharpExtensionsTests;
using MathsCore.Extensions.ContainerClasses;
using MathsCore.Sets;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathsCoreTests.Sets
{
    [TestClass]
    public class BinaryRelationExtensionsTests
    {
        [TestMethod]
        public void Test_images()
        {
            var binaryRelation =
                new List<string> {"a", "ab", "abcd"}.Select(str => new Tuple<string, int>(str, str.Length)).ToSet();
            binaryRelation.Image().ShouldEqual(1, 2, 4);
            binaryRelation.InverseImage().ShouldEqual("a", "ab", "abcd");
            binaryRelation.Image(new Set<string>{"ab", "abcd"}).ShouldEqual(2, 4);
            binaryRelation.InverseImage(new Set<int>{1, 2}).ShouldEqual("a", "ab");
            binaryRelation.Image("ab").ShouldEqual(2);
            binaryRelation.InverseImage(4).ShouldEqual("abcd");
            binaryRelation.IsMap().ShouldBeTrue();
            binaryRelation.IsSurjective(new Set<int> { 1, 2, 4 }).ShouldBeTrue();
            binaryRelation.IsSurjective(new Set<int> { 1, 2, 3, 4 }).ShouldBeFalse();
            binaryRelation.IsInjective().ShouldBeTrue();
            binaryRelation.IsBijective(new Set<int>{1, 2, 4}).ShouldBeTrue();
        }

        [TestMethod]
        public void Test_is_map_false()
        {
            var binaryRelation =
                new List<string> { "a", "ab", "abcd" }.Select(str => new Tuple<string, int>(str, str.Length)).ToSet();
            binaryRelation.Add(new Tuple<string, int>("ab", 3));
            binaryRelation.IsMap().ShouldBeFalse();
        }

        [TestMethod]
        public void Test_is_injective_false()
        {
            var binaryRelation =
                new List<string> { "a", "ab", "abcd" }.Select(str => new Tuple<string, int>(str, str.Length)).ToSet();
            binaryRelation.Add(new Tuple<string, int>("cd", 2));
            binaryRelation.IsInjective().ShouldBeFalse();
        }

        [TestMethod]
        public void Test_is_reflexive()
        {
            var binaryRelation = new Set<Tuple<int, int>> {new Tuple<int, int>(1, 1), new Tuple<int, int>(1, 2)};
            binaryRelation.IsReflexive().ShouldBeFalse();
            binaryRelation.Add(new Tuple<int, int>(2, 2));
            binaryRelation.IsReflexive().ShouldBeTrue();
            binaryRelation.IsEquivalenceRelation().ShouldBeFalse();
        }

        [TestMethod]
        public void Test_is_symmetric()
        {
            var binaryRelation = new Set<Tuple<int, int>> { new Tuple<int, int>(1, 1), new Tuple<int, int>(1, 2) };
            binaryRelation.IsSymmetric().ShouldBeFalse();
            binaryRelation.Add(new Tuple<int, int>(2, 1));
            binaryRelation.IsSymmetric().ShouldBeTrue();
            binaryRelation.IsEquivalenceRelation().ShouldBeFalse();
        }

        [TestMethod]
        public void Test_is_transitive()
        {
            var binaryRelation = new Set<Tuple<int, int>> { new Tuple<int, int>(1, 2), new Tuple<int, int>(2, 3) };
            binaryRelation.IsTransitive().ShouldBeFalse();
        }

        [TestMethod]
        public void Test_is_transitive_false()
        {
            var binaryRelation = new Set<Tuple<int, int>> { new Tuple<int, int>(1, 2), new Tuple<int, int>(2, 3), new Tuple<int, int>(1, 3) };
            binaryRelation.IsTransitive().ShouldBeTrue();
            binaryRelation.IsEquivalenceRelation().ShouldBeFalse();
        }

        [TestMethod]
        public void Test_is_equivalence_relation()
        {
            var binaryRelation = new Set<Tuple<int, int>>
            {
                new Tuple<int, int>(1, 2), new Tuple<int, int>(2, 3), new Tuple<int, int>(1, 3), new Tuple<int, int>(4, 4),
                new Tuple<int, int>(2, 1), new Tuple<int, int>(3, 2), new Tuple<int, int>(3, 1), new Tuple<int, int>(2, 2), 
                new Tuple<int, int>(3, 3), new Tuple<int, int>(1, 1)
            };
            binaryRelation.IsEquivalenceRelation().ShouldBeTrue();
        
        }
    }
}