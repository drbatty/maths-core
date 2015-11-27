using CSharpExtensionsTests;
using MathsCore.Graph.Directed;
using MathsCore.Graph.Directed.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathsCoreTests.Graph.Directed
{
    [TestClass]
    public class DirectedEdgeTests
    {
        [TestMethod]
        public void Initial_vertex_should_be_correct()
        {
            var edge = new DirectedEdge<int>(1, 2);
            edge.InitialVertex.ShouldEqual(1);
        }

        [TestMethod]
        public void Terminal_vertex_should_be_correct()
        {
            var edge = new DirectedEdge<int>(1, 2);
            edge.TerminalVertex.ShouldEqual(2);
        }

        [TestMethod]
        public void Adjacency_should_work_correctly()
        {
            var edge = new DirectedEdge<int>(1, 2);
            edge.AdjacentTo(1).ShouldBeTrue();
            edge.AdjacentTo(2).ShouldBeTrue();
        }
    }
}