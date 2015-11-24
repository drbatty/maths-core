using CSharpExtensionsTests;
using MathsCore.Interfaces;
using MathsCore.Sets;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathsCoreTests.Sets
{
    [TestClass]
    public class MetricExtensionsTests
    {
        [TestMethod]
        public void TestClosestElementsOfSet()
        {
            var set1 = new Set<MetricInteger>
            {
                new MetricInteger(1),
                new MetricInteger(4),
                new MetricInteger(6),
                new MetricInteger(11)
            };
            var set2 = new Set<MetricInteger>
            {
                new MetricInteger(7),
                new MetricInteger(27),
                new MetricInteger(17),
                new MetricInteger(-5)
            };
            var closest = set1.Closest(set2);
            var elements = closest.Item1;
            var distance = closest.Item2;
            distance.ShouldEqual(1);
            elements.Item1.ShouldEqual(new MetricInteger(6));
            elements.Item2.ShouldEqual(new MetricInteger(7));
        }

        [TestMethod]
        public void TestClosestSets()
        {
            var sets1 = new Set<Set<MetricInteger>>
            {
                new Set<MetricInteger>{new MetricInteger(1), new MetricInteger(3)},
                new Set<MetricInteger>{new MetricInteger(5), new MetricInteger(6)}
            };
            var sets2 = new Set<Set<MetricInteger>>
            {
                new Set<MetricInteger>{new MetricInteger(7), new MetricInteger(9)},
                new Set<MetricInteger>{new MetricInteger(16), new MetricInteger(19)}
            };
            var closest = sets1.Closest(sets2);
            var elements = closest.Item1;
            var distance = closest.Item2;
            distance.ShouldEqual(1);
            elements.Item1.ShouldContain(new MetricInteger(5), new MetricInteger(6));
            elements.Item2.ShouldContain(new MetricInteger(7), new MetricInteger(9));
        }

        [TestMethod]
        public void TestToSimpleGraph()
        {
            var set1 = new Set<MetricInteger> {new MetricInteger(1), new MetricInteger(3), new MetricInteger(6)};
            var graph = set1.ToSimpleGraph(2);
            graph.Order().ShouldEqual(3);
            graph.Size().ShouldEqual(1);
            graph = set1.ToSimpleGraph(3);
            graph.Order().ShouldEqual(3);
            graph.Size().ShouldEqual(2);
            graph = set1.ToSimpleGraph(5);
            graph.Order().ShouldEqual(3);
            graph.Size().ShouldEqual(3);
        }
    }
}