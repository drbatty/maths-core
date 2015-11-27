using CSharpExtensions;
using CSharpExtensionsTests;
using MathsCore.Extensions.ContainerClasses;
using MathsCore.Graph.Directed.Interfaces;
using MathsCore.Graph.DirectedLabelled;
using MathsCore.Graph.DirectedLabelled.Interfaces;
using MathsCore.Sets;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathsCoreTests.Graph.DirectedLabelled
{
    [TestClass]
    public class DirectedLabelledGraphTests
    {
        private static IDirectedLabelledGraph<int, int> TestGraph()
        {
            var vertices = 1.Upto(4).ToSet();
            var edges = new Set<IDirectedEdge<int>>   
            {
                new DirectedLabelledEdge<int, int>{ InitialVertex = 1, Label = 1, TerminalVertex = 2}, 
                new DirectedLabelledEdge<int, int>{ InitialVertex = 1, Label = 2, TerminalVertex = 3}, 
                new DirectedLabelledEdge<int, int>{ InitialVertex = 1, Label = 3, TerminalVertex = 4} 
            };

            return new DirectedLabelledGraph<int, int>(vertices, edges);
        }


        [TestMethod]
        public void HasOutEdgesTestTrue()
        {
            TestGraph().HasOutEdges(1).ShouldBeTrue();
        }

        [TestMethod]
        public void HasOutEdgesFalse()
        {
            TestGraph().HasOutEdges(2).ShouldBeFalse();
        }

        [TestMethod]
        public void HasOutEdgeTestTrue()
        {
            TestGraph().HasOutEdge(1, 3).ShouldBeTrue();
        }

        [TestMethod]
        public void HasOutEdgeTestFalse()
        {
            TestGraph().HasOutEdge(1, 5).ShouldBeFalse();
        }

        [TestMethod]
        public void TerminalVertexTestNotNull()
        {
            TestGraph().TerminalVertex(1, 2).ShouldEqual(3);
        }

        [TestMethod]
        public void TerminalVertexTestNull()
        {
            TestGraph().TerminalVertex(1, 5).ShouldEqual(0);
        }
    }
}
