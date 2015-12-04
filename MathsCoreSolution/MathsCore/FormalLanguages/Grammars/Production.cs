namespace MathsCore.FormalLanguages.Grammars
{
    public class Production
    {
        public string Lhs;
        public string Rhs;

        public override string ToString()
        {
            return Lhs + "->" + Rhs;
        }
    }
}