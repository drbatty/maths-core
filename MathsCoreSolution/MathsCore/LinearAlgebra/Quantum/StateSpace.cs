using System;

namespace MathsCore.LinearAlgebra.Quantum
{
    public class StateSpace
    {
        public Vector<int, ComplexNumber> StateVector { get; set; }

        public void SetStateVector(int n)
        {
            StateVector = new Vector<int, ComplexNumber> { { n, new ComplexNumber(1, 0) } };
        }

        public void Apply(Vector<Tuple<int, int>, ComplexNumber> matrix)
        {
            StateVector = matrix.Product(StateVector);
        }

        /*public static string KroneckerProduct(string s1, string s2)
        {
            var result = string.Empty;
            foreach (var t1 in s1)
                foreach (var t2 in s2)
                {
                    int digit1;
                    int digit2;
                    if (int.TryParse(t1.ToS(), out digit1) && 
                        int.TryParse(t2.ToS(), out digit2))
                        result += digit1*digit2;
                }
            return result;
        }*/
    }
}