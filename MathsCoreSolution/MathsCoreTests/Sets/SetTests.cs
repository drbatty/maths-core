using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CSharpExtensions;
using CSharpExtensionsTests;
using MathsCore.Extensions.ContainerClasses;
using MathsCore.Sets;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathsCoreTests.Sets
{
    [TestClass]
    public class SetTests
    {
        [TestMethod]
        public void SetTestAddDuplicate()
        {
            var set = 1.Upto(2).ToSet();
            set.Add(2);
            set.ShouldNumber(2);
        }

        [TestMethod]
        public void SetTestAddNonDuplicate()
        {
            var set = 1.Upto(2).ToSet();
            set.Add(3);
            set.ShouldNumber(3);
        }

        [TestMethod]
        public void SetTestIntersect()
        {
            1.Upto(3).ToSet().Intersect(2.Upto(5).ToSet()).ShouldContainExactly(2, 3);
        }

        [TestMethod]
        public void SetTestUnion()
        {
            (1.Upto(3).ToSet() | 2.Upto(5).ToSet()).ShouldContainExactly(1, 2, 3, 4, 5);
        }

        [TestMethod]
        public void SetSubtraction()
        {
            (1.Upto(4).ToSet() - 3.Upto(4).ToSet()).ShouldContainExactly(1, 2);
        }

        [TestMethod]
        public void SetAddition()
        {
            (1.Upto(4).ToSet() + 3.Upto(6).ToSet()).ShouldContainExactly(1, 2, 5, 6);
        }

        [TestMethod]
        public void SetContainsTrue()
        {
            1.Upto(4).ToSet().ShouldContain(3.Upto(4).ToSet());
        }

        [TestMethod]
        public void SetContainsFalse()
        {
            3.Upto(4).ToSet().Contains(1.Upto(4).ToSet()).ShouldBeFalse();
        }

        [TestMethod]
        public void SetContainsOperatorTrue()
        {
            var setA = new Set<int> {1, 2, 3, 4};
            var setB = new Set<int> {3, 4};
            (setA > setB).ShouldBeTrue();
        }

        [TestMethod]
        public void SetContainsOperatorFalse()
        {
            var setA = new Set<int> {1, 2, 3, 4};
            var setB = new Set<int> {3, 4};
            (setB > setA).ShouldBeFalse();
        }

        [TestMethod]
        public void SetContainedInOperatorFalse()
        {
            (1.Upto(4).ToSet() < 3.Upto(4).ToSet()).ShouldBeFalse();
        }

        [TestMethod]
        public void SetContainedInOrEqualOperatorFalse()
        {
            (1.Upto(4).ToSet() <= 3.Upto(4).ToSet()).ShouldBeFalse();
        }

        [TestMethod]
        public void SetContainedInOperatorTrue()
        {
            (3.Upto(4).ToSet() < 1.Upto(4).ToSet()).ShouldBeTrue();
        }

        [TestMethod]
        public void SetContainedInOrEqualOperatorTrue()
        {
            (3.Upto(4).ToSet() <= 1.Upto(4).ToSet()).ShouldBeTrue();
        }

        [TestMethod]
        public void SetContainedInOperatorFalse2()
        {
            // ReSharper disable once EqualExpressionComparison
            (1.Upto(4).ToSet() < 1.Upto(4).ToSet()).ShouldBeFalse();
        }

        [TestMethod]
        public void SetContainedInOperatorFalse3()
        {
            // ReSharper disable once EqualExpressionComparison
            (1.Upto(4).ToSet() > 1.Upto(4).ToSet()).ShouldBeFalse();
        }

        [TestMethod]
        public void SetContainedInOrEqualOperatorTrue2()
        {
            // ReSharper disable once EqualExpressionComparison
            (1.Upto(4).ToSet() <= 1.Upto(4).ToSet()).ShouldBeTrue();
        }

        [TestMethod]
        public void SetContainedInOrEqualOperatorTrue3()
        {
            // ReSharper disable once EqualExpressionComparison
            (1.Upto(4).ToSet() >= 1.Upto(4).ToSet()).ShouldBeTrue();
        }

        [TestMethod]
        public void SetEqualityTestTrue()
        {
            new Set<int> {1, 2, 3, 4}.ShouldEqual(new Set<int> {1, 2, 3, 4});
        }

        [TestMethod]
        public void SetEqualityTestFalse1()
        {
            1.Upto(2).ToSet().ShouldNotEqual(1.Upto(3).ToSet());
        }

        [TestMethod]
        public void SetEqualityTestFalse2()
        {
            1.Upto(3).ToSet().ShouldNotEqual(1.Upto(2).ToSet());
        }

        [TestMethod]
        public void TestSetCast()
        {
            // ReSharper disable UnusedVariable
            var set = (Set<int>) (1.Upto(3).ToList());
            // ReSharper restore UnusedVariable
        }

        [TestMethod]
        public void SingletonsTest()
        {
            ((Set<int>) (1.Upto(3).ToList())).Singletons().ShouldNumber(3);
        }

        [TestMethod]
        public void SetEqualityOperatorTestFalse1()
        {
            (1.Upto(2).ToSet() == 1.Upto(3).ToSet()).ShouldBeFalse();
        }

        [TestMethod]
        public void SetEqualityOperatorTestFalse2()
        {
            (1.Upto(3).ToSet() == 1.Upto(2).ToSet()).ShouldBeFalse();
        }

        [TestMethod]
        public void SetEqualityOperatorTestTrue()
        {
            // ReSharper disable once EqualExpressionComparison
            (new Set<int> {1, 2, 3, 4} == new Set<int> {1, 2, 3, 4}).ShouldBeTrue();
        }

        [TestMethod]
        public void SetInequalityOperatorTestTrue1()
        {
            (1.Upto(2).ToSet() != 1.Upto(3).ToSet()).ShouldBeTrue();
        }

        [TestMethod]
        public void SetInequalityOperatorTestTrue2()
        {
            (1.Upto(3).ToSet() != 1.Upto(2).ToSet()).ShouldBeTrue();
        }

        [TestMethod]
        public void SetInequalityOperatorTestFalse()
        {
            // ReSharper disable once EqualExpressionComparison
            (new Set<int> {1, 2, 3, 4} != new Set<int> {1, 2, 3, 4}).ShouldBeFalse();
        }

        [TestMethod]
        public void SetTestIntersectOperator()
        {
            (1.Upto(3).ToSet() & 2.Upto(5).ToSet()).ShouldContainExactly(2, 3);
        }

        [TestMethod]
        public void SetTestUnionOperator()
        {
            (1.Upto(3).ToSet() | 2.Upto(5).ToSet()).ShouldContainExactly(1, 2, 3, 4, 5);
        }

        [TestMethod]
        public void Power_sets_should_have_correct_cardinality_2_to_the_power_of_n()
        {
            new Set<int>().PowerSet().ShouldNumber(1); // should be the set containing the empty set.
            new Set<int> {1}.PowerSet().ShouldNumber(2);
            new Set<int> {1, 2}.PowerSet().ShouldNumber(4);
            new Set<int> {1, 2, 3}.PowerSet().ShouldNumber(8);
            new Set<int> {1, 2, 3, 4}.PowerSet().ShouldNumber(16);
            new Set<int> {1, 2, 3, 4, 5}.PowerSet().ShouldNumber(32);
            new Set<int> {1, 2, 3, 4, 5, 6}.PowerSet().ShouldNumber(64);
            new Set<int> {1, 2, 3, 4, 5, 6, 7}.PowerSet().ShouldNumber(128);
        }

        [TestMethod]
        public void Cartesian_products_should_have_correct_cardinality()
        {
            new Set<int>().CartesianProduct(new Set<int>()).ShouldNumber(0);
            new Set<int> {1, 2}.CartesianProduct(new Set<int>()).ShouldNumber(0);
            new Set<int> {1}.CartesianProduct(new Set<int> {1}).ShouldNumber(1);
            new Set<int> {1}.CartesianProduct(new Set<int> {1, 2}).ShouldNumber(2);
            new Set<int> {1, 2}.CartesianProduct(new Set<int> {1}).ShouldNumber(2);
            new Set<int> {1, 2}.CartesianProduct(new Set<int> {1, 2, 3}).ShouldNumber(6);
        }

        [TestMethod]
        public void TestSetsMeet()
        {
            new Set<int>{1,2}.Meets(new Set<int>{2, 3}).ShouldBeTrue();
            new Set<int>().Meets(new Set<int>()).ShouldBeFalse();
            new Set<int>{1}.Meets(new Set<int>{2}).ShouldBeFalse();
        }

        [TestMethod]
        public void TestSetInequality_DifferentType()
        {
            // ReSharper disable once SuspiciousTypeConversion.Global
            new Set<int>().Equals(1).ShouldBeFalse();
        }

        [TestMethod]
        public void TestHashCode()
        {
            new Set<int>().GetHashCode().ShouldEqual(0);
            new Set<string> { "a" }.GetHashCode().ShouldEqual("a".GetHashCode());
        }

        [TestMethod]
        public void ToDictionaryTest()
        {
            new Set<string> { "abc", "d", "ef" }.ToDictionary(s => s.Length).Values.ShouldEqual(3, 1, 2);
        }

        [TestMethod]
        public void ToStringTest()
        {
            new Set<int>{1,2,3}.ToString().ShouldEqual("{1,2,3}");
        }

        [TestMethod]
        public void RemoveAtTest()
        {
            var set = new Set<int> { 1, 2, 3 };
            set.RemoveAt(1);
            set.ShouldEqual(1, 3);
        }

        [TestMethod]
        public void FilteredPowerSetTest()
        {
            var set = new Set<int> {1, 2, 3};
            set.PowerSet(x => x.Count() == 1).ShouldContain(new Set<int> {1}, new Set<int>{2}, new Set<int>{3});
        }

        [TestMethod]
        public void ClearTest()
        {
            var set = new Set<int> {1, 2, 3};
            set.Clear();
            set.ShouldBeEmpty();
        }

        [TestMethod]
        public void TestReadOnly()
        {
            new Set<int>().IsReadOnly.ShouldBeFalse();
        }

        [TestMethod]
        public void TestIndexer()
        {
            var set = new Set<int>{1, 2, 3};
            set[1].ShouldEqual(2);
            set[1] = 3;
            set[1].ShouldEqual(3);
        }

        [TestMethod]
        public void TestExplicitOperator()
        {
            var list = new List<int> {1, 2, 3};
            var set = (Set<int>) list;
            set.ShouldEqual(1, 2, 3);
        }

        [TestMethod]
        public void TestExplicitOperator2()
        {
            var list = new Collection<int> { 1, 2, 3 };
            var set = (Set<int>)list;
            set.ShouldEqual(1, 2, 3);
        }

        [TestMethod]
        public void TestRemove()
        {
            var set = new Set<int> {1, 2, 3};
            set.Remove(2);
            set.ShouldEqual(1, 3);
        }

        [TestMethod]
        public void TestAddWithParams()
        {
            new Set<int> {1, 2, 3, {4, 5}};
        }

        [TestMethod]
        public void TestEqualityNull()
        {
            Set<int> set1 = null;
            Set<int> set2 = null;
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            (set1 == set2).ShouldBeTrue();
            var set3 = new Set<int>();
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            (set1 == set3).ShouldBeFalse();
        }

        [TestMethod]
        public void TestInequalityNull()
        {
            Set<int> set1 = null;
            Set<int> set2 = null;
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            (set1 != set2).ShouldBeFalse();
            var set3 = new Set<int>();
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            (set1 != set3).ShouldBeTrue();
        }

        [TestMethod]
        public void TestEqualityOperator()
        {
            (new Set<int>{1} == new Set<int>{1}).ShouldBeTrue();
        }
    }
}