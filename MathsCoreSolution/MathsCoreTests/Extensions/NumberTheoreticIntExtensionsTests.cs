using CSharpExtensionsTests;
using MathsCore.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathsCoreTests.Extensions
{
    [TestClass]
    public class NumberTheoreticIntExtensionsTests
    {
        [TestMethod]
        public void TestIntegerDividesNonzero()
        {
            5.Divides(15).ShouldBeTrue();
        }

        [TestMethod]
        public void TestIntegerDividesZero()
        {
            0.Divides(15).ShouldBeFalse();
        }

        [TestMethod]
        public void TestProperFactors18()
        {
            18.ProperFactors().ShouldContainExactly(2, 3, 6, 9);
        }

        [TestMethod]
        public void TestProperFactors13()
        {
            13.ProperFactors().ShouldBeEmpty();
        }

        [TestMethod]
        public void factorials_should_give_expected_results()
        {
            0.Factorial().ShouldEqual(1);
            1.Factorial().ShouldEqual(1);
            2.Factorial().ShouldEqual(2);
            3.Factorial().ShouldEqual(6);
            4.Factorial().ShouldEqual(24);
            5.Factorial().ShouldEqual(120);
        }

        [TestMethod]
        public void ToBinary_should_give_expected_results()
        {
            7.ToBinary().ShouldEqual("00000000" + "00000000" + "00000000" + "00000111");
            0.ToBinary().ShouldEqual("00000000" + "00000000" + "00000000" + "00000000");
        }

        [TestMethod]
        public void Test_perfect_numbers()
        {
            6.IsPerfect().ShouldBeTrue();
            28.IsPerfect().ShouldBeTrue();
            496.IsPerfect().ShouldBeTrue();
            8128.IsPerfect().ShouldBeTrue();
        }

        [TestMethod]
        public void Test_primes()
        {
            11.IsPrime().ShouldBeTrue();
            41.IsPrime().ShouldBeTrue();
            97.IsPrime().ShouldBeTrue();
            48.IsPrime().ShouldBeFalse();
            1000.IsPrime().ShouldBeFalse();
            81.IsPrime().ShouldBeFalse();
        }

        [TestMethod]
        public void Test_τ()
        {
            1.τ().ShouldEqual(1);
            12.τ().ShouldEqual(6);
        }

        [TestMethod]
        public void Test_deficiency_and_abundance()
        {
            12.Deficiency().ShouldEqual(-4);
            12.Abundance().ShouldEqual(4);
            28.Deficiency().ShouldEqual(0);
            28.Abundance().ShouldEqual(0);
            12.IsDeficient().ShouldBeFalse();
            12.IsAbundant().ShouldBeTrue();
            28.IsAbundant().ShouldBeFalse();
            28.IsDeficient().ShouldBeFalse();
            16.IsAbundant().ShouldBeFalse();
            16.Abundance().ShouldEqual(-1);
        }

        [TestMethod]
        public void Test_primes_up_to()
        {
            20.PrimesUpTo().ShouldContainExactly(2,3,5,7,11,13,17,19);
        }

        [TestMethod]
        public void Test_least_common_multiple()
        {
            6.LeastCommonMultiple(10).ShouldEqual(30);
            3.LeastCommonMultiple(5).ShouldEqual(15);
            17.LeastCommonMultiple(17).ShouldEqual(17);
        }

        [TestMethod]
        public void Test_ϕ()
        {
            13.ϕ().ShouldEqual(12);
            14.ϕ().ShouldEqual(6);
            15.ϕ().ShouldEqual(8);
        }

        [TestMethod]
        public void Test_F()
        {
            16.F().ShouldEqual(8);
            37.F().ShouldEqual(37);
            48.F().ShouldEqual(11);
            77.F().ShouldEqual(18);
        }

        [TestMethod]
        public void Test_aliquot_sequence()
        {
            10.AliquotSequence(5).ShouldEqual(10, 8, 7, 1, 0);
        }

        [TestMethod]
        public void Test_modular_aliquot_sequence()
        {
            10.ModularAliquotSequence(3, 7).ShouldEqual(3, 1, 0);
        }

        [TestMethod]
        public void Test_modular_aliquot_sequence_lengths()
        {
            10.ModularAliquotSequenceLengths(7).ShouldEqual(0,1,2,2,3,2,7,2,3,4);
        }
    }
}