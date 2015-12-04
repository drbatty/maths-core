using System.Collections.Generic;
using CSharpExtensionsTests;
using MathsCore.Statistics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathsCoreTests.Statistics
{
    [TestClass]
    public class StatisticsTests
    {
        [TestMethod]
        public void TestTTest()
        {
            var data1 = new List<double> { 1, 2, 5, 7, 10 };
            var data2 = new List<double> { 5, 2, 10, 7, 1 };

            data1.TTest(data2).ShouldEqual(0);
        }
    }
}