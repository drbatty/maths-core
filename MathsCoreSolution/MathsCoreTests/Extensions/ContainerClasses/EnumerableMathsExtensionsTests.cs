using System;
using System.Collections.Generic;
using System.Linq;
using CSharpExtensions;
using CSharpExtensionsTests;
using MathsCore.Extensions.ContainerClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathsCoreTests.Extensions.ContainerClasses
{
    [TestClass]
    public class EnumerableMathsExtensionsTests
    {
        #region Combinations tests

        [TestMethod]
        public void CombinationsTestTrue()
        {
            0.ArrayUpto(1).Combinations(2, true).ShouldNumber(3);
            // result == {{0, 0}, {0, 1}, {1, 1}}
        }

        [TestMethod]
        public void CombinationsTestFalse()
        {
            0.ArrayUpto(1).Combinations(2).ShouldNumber(1);
            // result == {{0, 1}}
        }

        #endregion

        #region Maximizer tests

        [TestMethod]
        public void MaximizerTest()
        {
            Func<int, double> doubleVal = n => (double)n;
            1.Upto(10).Maximizer(doubleVal).ShouldEqual(new Tuple<int, double>(10, 10));
        }

        [TestMethod]
        public void MaximizerTestNull()
        {
            1.Upto(10).Maximizer(null).ShouldBeNull();
        }

        [TestMethod]
        public void MaximizerTestEmpty()
        {
            Func<int, double> doubleVal = n => n;
            new List<int>().Maximizer(doubleVal).ShouldBeNull();
        }

        #endregion

        #region MinPair tests

        [TestMethod]
        public void MinPairTest()
        {
            Func<int, int, double> sum = (m, n) => m * n;
            1.Upto(10).Min(1.Upto(10), sum).ShouldEqual(1);
        }

        [TestMethod]
        public void MinPairTestNull()
        {
            1.Upto(10).Min(1.Upto(10), null).ShouldEqual(default(double));
        }

        [TestMethod]
        public void MinPairTestEmpty1()
        {
            Func<int, int, double> sum = (m, n) => m * n;
            new List<int>().Min(1.Upto(10), sum).ShouldEqual(default(double));
        }

        [TestMethod]
        public void MinPairTestEmpty2()
        {
            Func<int, int, double> sum = (m, n) => m * n;
            1.Upto(10).Min(new List<int>(), sum).ShouldEqual(default(double));
        }

        #endregion

        #region Minimizers tests

        [TestMethod]
        public void MinimizersTest()
        {
            1.Upto(10).Minimizers(1.Upto(10), (d1, d2) => d1 * d2).Item1.ShouldEqual(new Tuple<int, int>(1, 1));
        }

        [TestMethod]
        public void MinimizersTest2()
        {
            1.Upto(10).Minimizers(1.Upto(10), (d1, d2) => d1 * d2).Item2.ShouldEqual(1.0);
        }

        [TestMethod]
        public void MinimizersTestEmpty1()
        {
            new List<int>().Minimizers(1.Upto(10), (d1, d2) => d1 * d2).ShouldBeNull();
        }

        [TestMethod]
        public void MinimizersTestEmpty2()
        {
            1.Upto(10).Minimizers(new List<int>(), (d1, d2) => d1 * d2).ShouldBeNull();
        }

        [TestMethod]
        public void MinimizersTestLambdaNull()
        {
            1.Upto(10).Minimizers(1.Upto(10), null).ShouldBeNull();
        }

        #endregion

        #region Minimizer tests

        [TestMethod]
        public void MinimizerTest()
        {
            var doubles = new List<double> { 1.0, 2.0, 3.0, 4.0, 5.0 };
            doubles.Minimizer(d => 5.0 - d).Item1.ShouldEqual(5.0);
        }

        [TestMethod]
        public void MinimizerTestMinVal()
        {
            var doubles = new List<double> { 1.0, 2.0, 3.0, 4.0, 5.0 };
            doubles.Minimizer(d => 5.0 - d).Item2.ShouldEqual(0.0);
        }

        [TestMethod]
        public void MinimizerTestNull()
        {
            var doubles = new List<double> { 1.0, 2.0, 3.0, 4.0, 5.0 };
            doubles.Minimizer(null);
        }

        #endregion

        #region frequencies tests

        [TestMethod]
        public void TestFrequenciesEmpty()
        {
            new List<int>().Frequencies().ShouldBeEmpty();
        }

        [TestMethod]
        public void TestFrequencies()
        {
            var ints = new List<int> { 1, 1, 1, 2, 2, 3 };
            var freqs = ints.Frequencies().ToList();
            freqs.ShouldNumber(3);
            freqs.ShouldContain(1.Pair(3));
            freqs.ShouldContain(2.Pair(2));
            freqs.ShouldContain(3.Pair(1));
        }

        #endregion
    }
}
