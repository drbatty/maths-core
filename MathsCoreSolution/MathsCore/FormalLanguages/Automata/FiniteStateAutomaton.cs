using System.Collections.Generic;
using MathsCore.Exceptions;
using MathsCore.Graph.Directed.Interfaces;
using MathsCore.Graph.DirectedLabelled;
using MathsCore.Interfaces;
using MathsCore.Sets;

namespace MathsCore.FormalLanguages.Automata
{
    /// <summary>
    /// generic finite state automaton class
    /// </summary>
    /// <typeparam name="TState">the type of the states</typeparam>
    /// <typeparam name="TTransition">the type of the transitions</typeparam>
    public class FiniteStateAutomaton<TState, TTransition> : DirectedLabelledGraph<TState, TTransition>,
        IStateful<TState, TTransition>
    {
        //typically StateType and TransitionEdgeType will be enums
        public Set<TState> AcceptStates { get; private set; }
        public TState StartState { get; private set; }
        public TState State { get; set; }
        public List<TTransition> History { get; private set; }

        public FiniteStateAutomaton(Set<TState> states, Set<TState> acceptStates,
            Set<IDirectedEdge<TState>> transitions,
            TState startState)
            : base(states, transitions)
        {
            AcceptStates = acceptStates;
            StartState = startState;
            State = startState;
            History = new List<TTransition>();
        }

        /// <summary>
        /// performs a transition from the current state with the given transition edge label if possible,
        /// otherwise throws an InvalidTransitionException
        /// </summary>
        /// <param name="transitionEdgeLabel"></param>
        public void Transist(TTransition transitionEdgeLabel)
        {
            if (!PerformTransition(transitionEdgeLabel))
                throw new InvalidTransitionException(State.ToString(), transitionEdgeLabel.ToString());
        }

        /// <summary>
        /// returns false if there is no transition edge label from the current state with the given label,
        /// otherwise transists the state and adds the transition edge label to the history.
        /// </summary>
        /// <param name="transitionEdgeLabel"></param>
        /// <returns></returns>
        protected bool PerformTransition(TTransition transitionEdgeLabel)
        {
            if (!ValidTransition(transitionEdgeLabel)) return false;
            State = TerminalVertex(State, transitionEdgeLabel);
            History.Add(transitionEdgeLabel);
            return true;
        }

        /// <summary>
        /// returns true if and only if the current vertex has a valid transition with the given edge label
        /// </summary>
        /// <param name="transitionEdgeLabel"></param>
        /// <returns>true if and only if the current vertex has a valid transition with the given edge label</returns>
        public bool ValidTransition(TTransition transitionEdgeLabel)
        {
            return this.HasOutEdges(State) && (HasOutEdge(State, transitionEdgeLabel) && TerminalVertex(State, transitionEdgeLabel) < Vertices);
        }

        /// <summary>
        /// return true if and only if the automaton is currently in an accept state
        /// </summary>
        /// <returns>true if and only if the automaton is currently in an accept state</returns>
        public bool IsInAcceptState()
        {
            return State < AcceptStates;
        }
    }
}
