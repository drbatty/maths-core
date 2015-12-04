using System;

namespace MathsCore.Exceptions
{
    public class InvalidTransitionException : Exception
    {
        private readonly string _transitionName;
        private readonly string _stateName;

        public InvalidTransitionException(string stateName, string transitionName)
        {
            _stateName = stateName;
            _transitionName = transitionName;
        }

        public override string Message
        {
            get
            {
                return "invalid transition error - the transition " + _transitionName + " is not valid from state " + _stateName;
            }
        }
    }
}