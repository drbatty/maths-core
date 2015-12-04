using CSharpExtensions;
using CSharpExtensionsTests;
using CSharpExtensionsTests.Base;
using MathsCore.Exceptions;
using MathsCore.Extensions.ContainerClasses;
using MathsCore.FormalLanguages.Automata;
using MathsCore.Graph.Directed.Interfaces;
using MathsCore.Graph.DirectedLabelled;
using MathsCore.Sets;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathsCoreTests.FormalLanguages.Automata
{
    [TestClass]
    public class FiniteStateAutomatonTests : TestBase
    {
        private static readonly Set<int> States = 1.Upto(3).ToSet();
        const int StartState = 1;
        private static readonly Set<int> AcceptStates = 1.WrapInList().ToSet();

        private static FiniteStateAutomaton<int, int> Empty123Automaton()
        {
            var transitions = new Set<IDirectedEdge<int>>();
            return new FiniteStateAutomaton<int, int>(States, AcceptStates, transitions, StartState);
        }

        private static FiniteStateAutomaton<int, int> SingleEdgeAutomaton(int transitionLabel, int terminalVertex)
        {
            var transitions = new Set<IDirectedEdge<int>> 
            {
                new DirectedLabelledEdge<int, int>
                {
                    InitialVertex = 1,
                    Label = transitionLabel,
                    TerminalVertex = terminalVertex
                }
            };

            return new FiniteStateAutomaton<int, int>(States, AcceptStates, transitions, StartState);
        }

        [TestMethod]
        // ReSharper disable InconsistentNaming
        public void New_automaton_should_have_correct_initial_state()
        // ReSharper restore InconsistentNaming
        {
            Empty123Automaton().State.ShouldEqual(1);
        }

        [TestMethod]
        // ReSharper disable InconsistentNaming
        public void New_automaton_whose_initial_state_is_an_accept_state_should_be_in_an_accept_state()
        // ReSharper restore InconsistentNaming
        {
            Empty123Automaton().IsInAcceptState().ShouldBeTrue();
        }

        [TestMethod]
        // ReSharper disable InconsistentNaming
        public void New_automaton_with_no_valid_transitions_should_have_valid_transition_false_from_initial_state()
        // ReSharper restore InconsistentNaming
        {
            Empty123Automaton().ValidTransition(1).ShouldBeFalse();
        }

        [TestMethod]
        // ReSharper disable InconsistentNaming
        public void Single_edge_automaton_from_state_1_to_state_2_should_have_valid_transition_from_state_1_to_state_2()
        // ReSharper restore InconsistentNaming
        {
            SingleEdgeAutomaton(1, 1).ValidTransition(1).ShouldBeTrue();
        }

        [TestMethod]
        // ReSharper disable InconsistentNaming
        public void Single_edge_automaton_from_state_1_to_state_2_should_not_have_valid_transition_from_state_2_to_state_1()
        // ReSharper restore InconsistentNaming
        {
            SingleEdgeAutomaton(2, 1).ValidTransition(1).ShouldBeFalse();
        }

        [TestMethod]
        // ReSharper disable InconsistentNaming
        public void Single_edge_automaton_from_state_1_to_state_4_should_not_have_valid_transition_from_state_1_to_state_2()
        // ReSharper restore InconsistentNaming
        {
            SingleEdgeAutomaton(1, 4).ValidTransition(1).ShouldBeFalse();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidTransitionException))]
        // ReSharper disable InconsistentNaming
        public void Transist_should_throw_invalid_transition_exception_if_transition_is_not_valid()
        // ReSharper restore InconsistentNaming
        {
            Empty123Automaton().Transist(1);
            //ncrunch: no coverage start
        }
        //ncrunch: no coverage end

        [TestMethod]
        // ReSharper disable InconsistentNaming
        public void Transist_should_not_throw_exception_if_transition_is_valid()
        // ReSharper restore InconsistentNaming
        {
            SingleEdgeAutomaton(1, 1).Transist(1);
        }

        [TestMethod]
        // ReSharper disable InconsistentNaming
        public void History_should_contain_single_transition()
        // ReSharper restore InconsistentNaming
        {
            var fsa = SingleEdgeAutomaton(1, 1);
            fsa.Transist(1);
            fsa.History.ShouldContain(1);
        }

        [TestMethod]
        // ReSharper disable InconsistentNaming
        public void State_should_change_and_be_correct_after_transition()
        // ReSharper restore InconsistentNaming
        {
            var fsa = SingleEdgeAutomaton(1, 2);
            fsa.Transist(1);
            fsa.State.ShouldEqual(2);
        }

        [TestMethod]
        // ReSharper disable InconsistentNaming
        public void Automaton_whose_initial_state_is_the_only_accept_state_should_not_be_in_accept_state_after_transition_to_other_state()
        // ReSharper restore InconsistentNaming
        {
            var fsa = SingleEdgeAutomaton(1, 2);
            fsa.Transist(1);
            fsa.IsInAcceptState().ShouldBeFalse();
        }
    }
}