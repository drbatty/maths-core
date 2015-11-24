using System;
using CSharpExtensionsTests;
using MathsCore.Sets;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathsCoreTests.Sets
{
    [TestClass]
    public class MetricSetExtensionsTests
    {
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Distance_from_integer_to_empty_set_throws_exception()
        {
            new MetricInteger(1).Distance(new Set<MetricInteger>());
            //ncrunch: no coverage start
        }
        //ncrunch: no coverage end

        [TestMethod]
        public void Distance_to_set_of_integers_should_be_correct()
        {
            new MetricInteger(1).Distance(new Set<MetricInteger>{new MetricInteger(-1), new MetricInteger(5)}).ShouldEqual(2);
        }

        [TestMethod]
        public void Distance_from_set_of_integers_to_another_should_be_correct()
        {
            (new Set<MetricInteger>{new MetricInteger(-1), new MetricInteger(5)}).Distance(
                new Set<MetricInteger>{new MetricInteger(7), new MetricInteger(32)}
                ).ShouldEqual(2);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Distance_from_set_of_integers_to_empty_set_throws_exception()
        {
            new Set<MetricInteger> {new MetricInteger(-1), new MetricInteger(5)}.Distance(
                new Set<MetricInteger>());
            //ncrunch: no coverage start
        }
        //ncrunch: no coverage end
    }
}