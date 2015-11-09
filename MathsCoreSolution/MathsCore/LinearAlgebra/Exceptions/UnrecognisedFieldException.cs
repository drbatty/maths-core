using System;

namespace MathsCore.LinearAlgebra.Exceptions
{
    public class UnrecognisedFieldException : Exception
    {
        private readonly Type _type;

        public UnrecognisedFieldException(Type type)
        {
            _type = type;
        }

        public override string ToString()
        {
            return "the type " + _type + " is not a recognized mathematical field with a defined zero and unit.";
        }
    }
}