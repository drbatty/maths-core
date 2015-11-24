using System.Collections.Generic;
using System.Linq;
using CSharpExtensions;
using CSharpExtensions.ContainerClasses;
using CSharpExtensionsTests;
using CSharpExtensionsTests.Base;
using MathsCore.Graph.Exceptions;
using MathsCore.LinearAlgebra;
using MathsCore.Sets;
using MathsCore.SimplicialComplex.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MathsCore.SimplicialComplex;

namespace MathsCoreTests.SimplicialComplex
{
    [TestClass]
    public class SimplicialComplexTests : TestBase
    {
        private static SimplicialComplex<string> SingleEdge
        {
            get
            {
                return new SimplicialComplex<string>(new List<Set<string>>
                {
                    new Set<string> {"a", "b"}
                });
            }
        }

        private static SimplicialComplex<string> EmptyComplex
        {
            get
            {
                return new SimplicialComplex<string>();
            }
        }

        private static SimplicialComplex<string> Singleton
        {
            get
            {
                return new SimplicialComplex<string>(new List<Set<string>>
                {
                    new Set<string> {"a"}
                });
            }
        }

        private static SimplicialComplex<string> TwoSingletons
        {
            get
            {
                return new SimplicialComplex<string>(new List<Set<string>>
                {
                    new Set<string> {"a"}, new Set<string>{"b"}
                });
            }
        }

        [TestMethod]
        public void New_simplicial_complex_with_default_constructor_should_have_size_and_order_0()
        {
            var graph = new SimplicialComplex<int>();
            graph.Size().ShouldEqual(0);
            graph.Order().ShouldEqual(0);
        }

        [TestMethod]
        public void New_simplicial_complex_with_default_constructor_should_have_simple_edge_count_0()
        {
            var graph = new SimplicialComplex<int>();
            graph.Edges().Count.ShouldEqual(graph.Size());
        }

        private static SimplicialComplex<int> TestGraph()
        {
            return new SimplicialComplex<int>(new List<Set<int>>
            {
                new Set<int> {1, 2},
                new Set<int> {2, 3}
            });
        }

        [TestMethod]
        public void Valency_should_return_correct_values()
        {
            var graph = TestGraph();
            graph.Degree(1).ShouldEqual(1);
            graph.Degree(2).ShouldEqual(2);
            graph.Degree(3).ShouldEqual(1);
        }

        [TestMethod]
        [ExpectedException(typeof(NonexistentVertexException<int>))]
        public void Valency_of_non_existent_vertex_should_throw_NonexistentVertexException()
        {
            new SimplicialComplex<int>().Degree(1);
            //ncrunch: no coverage start
        }
        //ncrunch: no coverage end

        [TestMethod]
        public void Star_should_have_correct_vertices_and_edges()
        {
            var graph = TestGraph();
            graph.Star(1).Vertices().ShouldContainExactly(1, 2);
            graph.Star(1).Edges().ShouldContainExactly(new Set<int> { 1, 2 });

            graph.Star(2).Vertices().ShouldContainExactly(1, 2, 3);
            graph.Star(2).Edges().ShouldContainExactly(new Set<int> { 1, 2 }, new Set<int> { 2, 3 });
        }

        [TestMethod]
        [ExpectedException(typeof(NonexistentVertexException<int>))]
        public void Star_of_non_existent_vertex_should_throw_NonexistentVertexException()
        {
            //ncrunch: no coverage start
            new SimplicialComplex<int>().Star(1);
        }
        //ncrunch: no coverage end
        [TestMethod]
        public void Path_graph_should_have_correct_order_size_vertices_and_edges()
        {
            var graph = SimpleGraphExtensions.PathGraph(5);
            graph.Order().ShouldEqual(5);
            graph.Size().ShouldEqual(4);
            graph.Vertices().ShouldContain("v_1", "v_2", "v_3", "v_4", "v_5");
            graph.Edges().ShouldContain(new Set<string> { "v_1", "v_2" }, new Set<string> { "v_2", "v_3" },
                new Set<string> { "v_3", "v_4" }, new Set<string> { "v_4", "v_5" });
        }

        [TestMethod]
        public void Edgeless_graph_should_have_correct_order_size_and_vertices()
        {
            var graph = SimpleGraphExtensions.EdgelessGraph(5);
            graph.Order().ShouldEqual(5);
            graph.Size().ShouldEqual(0);
            graph.Vertices().ShouldContain("v_1", "v_2", "v_3", "v_4", "v_5");
        }

        [TestMethod]
        public void Star_graph_should_have_correct_order_size_and_vertices()
        {
            var graph = SimpleGraphExtensions.StarGraph(5);
            graph.Order().ShouldEqual(6);
            graph.Size().ShouldEqual(5);
            graph.Vertices().ShouldContain("v_1", "v_2", "v_3", "v_4", "v_5", "v_6");
            graph.Edges().ShouldContain(
                new Set<string> { "v_1", "v_2" },
                new Set<string> { "v_1", "v_3" },
                new Set<string> { "v_1", "v_4" },
                new Set<string> { "v_1", "v_5" },
                new Set<string> { "v_1", "v_6" });
        }

        [TestMethod]
        public void Cycle_graph_should_have_correct_order_size_vertices_and_edges()
        {
            var graph = SimpleGraphExtensions.CycleGraph(5);
            graph.Order().ShouldEqual(5);
            graph.Size().ShouldEqual(5);
            graph.Vertices().ShouldContain("v_1", "v_2", "v_3", "v_4", "v_5");
            graph.Edges().ShouldContain(
                new Set<string> { "v_1", "v_2" },
                new Set<string> { "v_2", "v_3" },
                new Set<string> { "v_3", "v_4" },
                new Set<string> { "v_4", "v_5" },
                new Set<string> { "v_5", "v_1" });
        }

        [TestMethod]
        public void Complete_graph_should_have_correct_order_size_vertices_and_edges()
        {
            var graph = SimpleGraphExtensions.CompleteGraph(4);
            graph.Order().ShouldEqual(4);
            graph.Size().ShouldEqual(6);
            graph.Vertices().ShouldContain("v_1", "v_2", "v_3", "v_4");
            graph.Edges().ShouldContain(
                new Set<string> { "v_1", "v_2" },
                new Set<string> { "v_2", "v_3" },
                new Set<string> { "v_3", "v_4" },
                new Set<string> { "v_1", "v_3" },
                new Set<string> { "v_1", "v_4" },
                new Set<string> { "v_2", "v_4" });
        }

        [TestMethod]
        public void Complete_graph_of_order_1_should_have_one_vertex()
        {
            SimpleGraphExtensions.CompleteGraph(1).Order().ShouldEqual(1);
        }

        [TestMethod]
        public void Distance_sort_should_give_correct_results()
        {
            var graph = new SimplicialComplex<string>(new List<Set<string>>
            {
                new Set<string>{"a", "b"},
                new Set<string>{"b", "c"},
                new Set<string>{"c", "d"},
                new Set<string>{"d", "e"},
                new Set<string>{"e", "f"}
            });

            var sorted1 = graph.DistanceSort("f".WrapInList());
            var enumerable = sorted1 as string[] ?? sorted1.ToArray();
            enumerable.ShouldNumber(6);
            enumerable.ElementAt(0).ShouldEqual("f");
            enumerable.ElementAt(1).ShouldEqual("e");
            enumerable.ElementAt(2).ShouldEqual("d");
            enumerable.ElementAt(3).ShouldEqual("c");
            enumerable.ElementAt(4).ShouldEqual("b");
            enumerable.ElementAt(5).ShouldEqual("a");

            var sorted2 = graph.DistanceSort("c".WrapInList());
            var enumerable1 = sorted2 as string[] ?? sorted2.ToArray();
            enumerable1.ShouldNumber(6);
            enumerable1.First().ShouldEqual("c");
            var slice1 = enumerable1.Slice(1, 3);
            slice1.ShouldContain("b", "d");
            var slice2 = enumerable1.Slice(3, 5);
            slice2.ShouldContain("a", "e");
            enumerable1.ElementAt(5).ShouldEqual("f");

            var sorted3 = graph.DistanceSort(new List<string> { "a", "c", "d" });
            var sorted4 = sorted3 as string[] ?? sorted3.ToArray();
            sorted4.ShouldNumber(6);
            var slice = sorted4.Slice(0, 3);
            slice.ShouldContain("a", "c", "d");
        }

        [TestMethod]
        public void IsFace_should_be_true_for_initial_and_terminal_vertices_and_false_for_single_edge()
        {
            var graph = SingleEdge;
            graph.IsFace(new Set<string> { "a" }).ShouldBeTrue();
            graph.IsFace(new Set<string> { "b" }).ShouldBeTrue();
            graph.IsFace(new Set<string> { "a", "b" }).ShouldBeFalse();
        }

        [TestMethod]
        public void IsFace_should_be_false_for_nonexistent_vertex()
        {
            var graph = new SimplicialComplex<string>();
            graph.IsFace(new Set<string> { "a" }).ShouldBeFalse();
        }

        [TestMethod]
        public void The_empty_set_should_be_a_face_of_any_nonempty_graph_but_not_the_empty_graph()
        {
            EmptyComplex.IsFace(new Set<string>()).ShouldBeFalse();
            Singleton.IsFace(new Set<string>()).ShouldBeTrue();
        }

        [TestMethod]
        public void ToString_for_simplicial_complex_should_produce_expected_results()
        {
            EmptyComplex.ToString().ShouldEqual("{}");
            Singleton.ToString().ShouldEqual("{Δ{a}}");
            TwoSingletons.ToString().ShouldEqual("{Δ{a},Δ{b}}");
            SingleEdge.ToString().ShouldEqual("{Δ{a,b}}");
        }

        [TestMethod]
        public void Empty_complex_should_be_recognised()
        {
            EmptyComplex.ShouldBeRecognisedAs("The empty complex");
        }

        [TestMethod]
        public void Dimension_of_empty_complex_should_be_minus_one()
        {
            EmptyComplex.Dimension.ShouldEqual(-1);
        }

        [TestMethod]
        public void Dimension_of_a_set_of_singletons_should_be_zero()
        {
            Singleton.Dimension.ShouldEqual(0);
            TwoSingletons.Dimension.ShouldEqual(0);
        }

        [TestMethod]
        public void Dimension_of_a_graph_with_edges_should_be_one()
        {
            SingleEdge.Dimension.ShouldEqual(1);
        }

        [TestMethod]
        public void Single_edge_should_be_recognised_as_complete_graph_of_order_2()
        {
            SingleEdge.ShouldBeRecognisedAs("Complete graph of order 2");
        }

        [TestMethod]
        public void Complete_graphs_of_small_order_should_be_recognised()
        {
            SimpleGraphExtensions.CompleteGraph(3).ShouldBeRecognisedAs("Complete graph of order 3");
            SimpleGraphExtensions.CompleteGraph(4).ShouldBeRecognisedAs("Complete graph of order 4");
            SimpleGraphExtensions.CompleteGraph(5).ShouldBeRecognisedAs("Complete graph of order 5");
        }

        [TestMethod]
        public void Totally_disconnected_graphs_of_small_order_should_be_recognised()
        {
            SimpleGraphExtensions.EdgelessGraph(1).ShouldBeRecognisedAs("Complete graph of order 1");
            SimpleGraphExtensions.EdgelessGraph(2).ShouldBeRecognisedAs("Totally disconnected graph on 2 vertices");
            SimpleGraphExtensions.EdgelessGraph(3).ShouldBeRecognisedAs("Totally disconnected graph on 3 vertices");
            SimpleGraphExtensions.EdgelessGraph(4).ShouldBeRecognisedAs("Totally disconnected graph on 4 vertices");
        }

        [TestMethod]
        [ExpectedException(typeof(ComplexesAreNotDisjointException))]
        public void Disjoint_union_should_throw_exception_if_vertex_sets_meet()
        {
            SimpleGraphExtensions.CompleteGraph(2).DisjointUnion(SimpleGraphExtensions.CompleteGraph(3));
            //ncrunch: no coverage start
        }
        //ncrunch: no coverage end

        [TestMethod]
        public void Disjoint_union_should_have_sums_of_numbers_of_simplices()
        {
            var factor1 = new SimplicialComplex<string>(SimpleGraphExtensions.PathGraph(2).AddPrefix("#1"));
            var factor2 = new SimplicialComplex<string>(SimpleGraphExtensions.CompleteGraph(3).AddPrefix("#2"));
            var graph = factor1.DisjointUnion(factor2);
            graph.Order().ShouldEqual(factor1.Order() + factor2.Order());
            graph.Count.ShouldEqual(factor1.Count + factor2.Count); // i.e. (since it is a graph) the number of edges are the same
        }

        [TestMethod]
        public void The_following_two_graphs_should_have_the_same_degree_sequence()
        {
            var factor1 = new SimplicialComplex<string>(SimpleGraphExtensions.PathGraph(2).AddPrefix("#1"));
            var factor2 = new SimplicialComplex<string>(SimpleGraphExtensions.CompleteGraph(3).AddPrefix("#2"));
            var graph1 = factor1.DisjointUnion(factor2);
            var graph2 = SimpleGraphExtensions.PathGraph(5);
            graph1.DegreeSequence().Count().ShouldEqual(graph2.DegreeSequence().Count());
            graph1.DegreeSequence().EachIndex(i => graph1.DegreeSequence().ElementAt(i).ShouldEqual(graph2.DegreeSequence().ElementAt(i)));
        }

        [TestMethod]
        public void The_induced_subgraph_by_two_adjacent_edges_should_be_an_edge()
        {
            var i = SimpleGraphExtensions.CycleGraph(4).InducedSubcomplex(new Set<string> { "v_1", "v_2" });
            i.Size().ShouldEqual(1);
            i.Order().ShouldEqual(2);
            i.ShouldContain(new Set<string> { "v_1" }, new Set<string> { "v_2" }, new Set<string> { "v_1", "v_2" });
        }

        [TestMethod]
        public void The_induced_subgraph_by_two_adjacent_edges_should_be_a_pair_of_vertices()
        {
            var i = SimpleGraphExtensions.CycleGraph(4).InducedSubcomplex(new Set<string> { "v_1", "v_3" });
            i.Size().ShouldEqual(0);
            i.Order().ShouldEqual(2);
            i.ShouldContain(new Set<string> { "v_1" }, new Set<string> { "v_3" });
        }

        [TestMethod]
        public void Random_graph_should_construct_and_have_dimension_at_most_1()
        {
            SimpleGraphExtensions.RandomGraph(10, 0.5).Dimension.ShouldBeAtMost(1);
        }

        [TestMethod]
        public void Singletons_only_element_should_not_be_a_face()
        {
            Singleton.IsFace(new Set<string> { "a" }).ShouldBeFalse();
        }

        [TestMethod]
        public void Nonfaces_of_singleton_should_contain_1_element()
        {
            Singleton.NonFaces.ShouldNumber(1);
        }

        [TestMethod]
        public void Nonfaces_of_singleton_should_contain_2_elements()
        {
            SimpleGraphExtensions.EdgelessGraph(2).NonFaces.ShouldNumber(2);
        }

        [TestMethod]
        public void Complete_graphs_should_have_clique_graph_equal_to_a_point()
        {
            Singleton.CliqueGraph().Order().ShouldEqual(1);
            SimpleGraphExtensions.CompleteGraph(2).CliqueGraph().Order().ShouldEqual(1);
            SimpleGraphExtensions.CompleteGraph(3).CliqueGraph().Order().ShouldEqual(1);
            SimpleGraphExtensions.CompleteGraph(4).CliqueGraph().Order().ShouldEqual(1);
        }

        [TestMethod]
        public void Cartesian_product_of_p2_and_p3_should_have_6_vertices_and_7_edges()
        {
            var graph = SimpleGraphExtensions.PathGraph(2).GraphCartesianProduct(SimpleGraphExtensions.PathGraph(3));
            graph.Order().ShouldEqual(6);
            graph.Size().ShouldEqual(7);
        }

        [TestMethod]
        public void Petersen_Graph_should_have_expected_order_and_size()
        {
            var graph = SimpleGraphExtensions.PetersenGraph();
            graph.Order().ShouldEqual(10);
            graph.Size().ShouldEqual(15);
        }

        [TestMethod]
        public void Grid_Graph_should_have_expected_order_and_size()
        {
            var graph = SimpleGraphExtensions.GridGraph(2, 3);
            graph.Order().ShouldEqual(6);
            graph.Size().ShouldEqual(7);
        }

        [TestMethod]
        public void Ladder_Graph_should_have_expected_order_and_size()
        {
            var graph = SimpleGraphExtensions.LadderGraph(4);
            graph.Order().ShouldEqual(8);
            graph.Size().ShouldEqual(10);
        }

        [TestMethod]
        public void Prism_Graph_should_have_expected_order_and_size()
        {
            var graph = SimpleGraphExtensions.PrismGraph(3, 2);
            graph.Order().ShouldEqual(6);
            graph.Size().ShouldEqual(9);
        }

        [TestMethod]
        public void StackedBookGraph_should_have_expected_order_and_size()
        {
            var graph = SimpleGraphExtensions.StackedBookGraph(3, 3);
            graph.Order().ShouldEqual(12);
            graph.Size().ShouldEqual(17);
        }

        [TestMethod]
        public void BookGraph_should_have_expected_order_and_size()
        {
            var graph = SimpleGraphExtensions.BookGraph(3);
            graph.Order().ShouldEqual(8);
            graph.Size().ShouldEqual(10);
        }

        [TestMethod]
        public void Adjacency_matrix_should_be_symmetric()
        {
            SimpleGraphExtensions.CycleGraph(4).AdjacencyMatrix<int, string>().IsSymmetric().ShouldBeTrue();
        }

        [TestMethod]
        public void Adjacency_matrix_should_have_correct_entries()
        {
            var matrix = SimpleGraphExtensions.CycleGraph(4).AdjacencyMatrix<int, string>();
            matrix.GetEntry("v_1", "v_2").ShouldEqual(1);
            matrix.GetEntry("v_2", "v_3").ShouldEqual(1);
            matrix.GetEntry("v_3", "v_4").ShouldEqual(1);
            matrix.GetEntry("v_4", "v_1").ShouldEqual(1);
            matrix.GetEntry("v_1", "v_3").ShouldEqual(0);
            matrix.GetEntry("v_2", "v_4").ShouldEqual(0);
            matrix.GetEntry("v_1", "v_1").ShouldEqual(0);
            matrix.GetEntry("v_2", "v_2").ShouldEqual(0);
            matrix.GetEntry("v_3", "v_3").ShouldEqual(0);
            matrix.GetEntry("v_4", "v_4").ShouldEqual(0);
        }

        [TestMethod]
        [ExpectedException(typeof(NonexistentVertexException<string>))]
        public void TestDistanceSort_NonBasepoint()
        {
            var graph = SimpleGraphExtensions.CycleGraph(3);
            graph.DistanceSort("v_4".WrapInList());
            //ncrunch: no coverage start
        }
        //ncrunch: no coverage end

        [TestMethod]
        public void TestDistanceSort_Disconnected()
        {
            var graph = SimpleGraphExtensions.EdgelessGraph(3);
            var sorted = graph.DistanceSort("v_1".WrapInList());
            var enumerable = sorted as IList<string> ?? sorted.ToList();
            enumerable.First().ShouldEqual("v_1");
            enumerable.ShouldContain("v_2", "v_3");
        }

        [TestMethod]
        public void TestGraphTensorProduct()
        {
            var graph1 = SimpleGraphExtensions.CycleGraph(3);
            var graph2 = SimpleGraphExtensions.CycleGraph(4);
            graph1.GraphTensorProduct(graph2).Order().ShouldEqual(12);
            graph1.GraphTensorProduct(graph2).Size().ShouldEqual(48);
        }

        [TestMethod]
        public void TestBipartiteDoubleCover()
        {
            var graph = SimpleGraphExtensions.CycleGraph(4);
            var cover = graph.BipartiteDoubleCover();
            cover.Order().ShouldEqual(8);
            cover.Size().ShouldEqual(20);
        }
    }
}