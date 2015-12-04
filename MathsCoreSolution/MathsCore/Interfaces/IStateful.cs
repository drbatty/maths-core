namespace MathsCore.Interfaces
{
    /// <summary>
    /// generic interface representing the statefulness of an automaton, e.g. a finite state automaton
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    /// <typeparam name="TTransition"></typeparam>
    public interface IStateful<out TState, in TTransition>
    {
        void Transist(TTransition transitionEdgeLabel);
        TState State { get; }
    }
}