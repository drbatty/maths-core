using CSharpExtensions;
using CSharpExtensionsTests;
using MathsCore.Extensions.ContainerClasses;
using MathsCore.Graph.Directed;
using MathsCore.Graph.Directed.Interfaces;
using MathsCore.Sets;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathsCoreTests.Graph.Directed
{
    [TestClass]
    public class DirectedGraphTests
    {
        [TestMethod]
        public void TestDirectedGraphDefaultConstructorVertices()
        {
            var graph = new DirectedGraph<int>();
            graph.Vertices.ShouldNumber(0);
        }

        [TestMethod]
        public void TestDirectedGraphDefaultConstructorEdges()
        {
            var graph = new DirectedGraph<int>();
            graph.Edges.ShouldNumber(0);
        }

        [TestMethod]
        public void TestDirectedGraphEmptyHasEdgeFalse()
        {
            var graph = new DirectedGraph<int>();
            graph.HasEdge(0, 0).ShouldBeFalse();
        }

        [TestMethod]
        public void TestDirectedGraphEmptyHasEdgeTrue()
        {
            var vertices = 1.Upto(3).ToSet();
            var edges = new Set<IDirectedEdge<int>> { new DirectedEdge<int>(1, 2) };
            var graph = new DirectedGraph<int>(vertices, edges);
            graph.HasEdge(1, 2).ShouldBeTrue();
        }

        [TestMethod]
        public void Graph_with_one_vertex_has_order_1()
        {
            var vertices = 1.WrapInList().ToSet();
            var edges = new Set<IDirectedEdge<int>>();
            var graph = new DirectedGraph<int>(vertices, edges);
            graph.Order().ShouldEqual(1);
        }

        [TestMethod]
        public void Graph_with_one_edge_has_size_1()
        {
            var vertices = 1.Upto(2).ToSet();
            var edges = new Set<IDirectedEdge<int>> { new DirectedEdge<int>(1, 2) };
            var graph = new DirectedGraph<int>(vertices, edges);
            graph.Size().ShouldEqual(1);
        }
    }
}
