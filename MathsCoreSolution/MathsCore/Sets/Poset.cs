using System;

namespace MathsCore.Sets
{
    public abstract class Poset<T> : Set<T>, IComparable<T>
    {
        public abstract int CompareTo(T other);

        //axioms for poset:

        // a <= b means compareTo is in {0, 1}
        // a >= b means compareTo is in {0, -1}

        /*a ≤ a (reflexivity);
        if a ≤ b and b ≤ a then a = b (antisymmetry); 
        if a ≤ b and b ≤ c then a ≤ c (transitivity).
         */
    }
}