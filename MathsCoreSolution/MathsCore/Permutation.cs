using System.Collections.Generic;
using System.Linq;
using CSharpExtensions;
using CSharpExtensions.ContainerClasses;

namespace MathsCore
{
    public class Permutation // rewrite in terms of relations and maps
    {
        public List<int> Images { get; set; }

        public Permutation()
        {
            Images = new List<int>();
        }

        public Permutation(List<int> images)
        {
            Images = images;
        }

        public static Permutation operator *(Permutation perm1, Permutation perm2)
        {
            var newImages = new List<int>();
            perm1.Images.Count.Do(i =>
                newImages.Add(perm2.Images[perm1.Images[i]]));

            return new Permutation(newImages);
        }

        #region equality members

        public override bool Equals(object other)
        {
            return (other is Permutation) && Equals((Permutation) other);
        }

        public bool Equals(Permutation other)
        {
            return Images.AllIndices(i => Images[i] == other.Images[i]);
        }

        public override int GetHashCode()
        {
            return (Images != null ? Images.GetHashCode() : 0);
        }

        #endregion

        public Permutation Power(int n)
        {
            var result = Identity(Images.Count);
            n.Times(() => result *= this);
            return result;
        }

        public static Permutation Cycle(int n)
        {
            var newImages = new List<int>();
            n.Do(i => newImages.Add((i + 1) % n));
            return new Permutation(newImages);
        }

        public static Permutation Identity(int n)
        {
            return new Permutation(0.Upto(n - 1).ToList());
        }

        public override string ToString()
        {
            return "{" + 0.Upto(Images.Count- 1).Select(i => i + "->" + Images[i]).SpacedAfterCommaSeparate() + "}";
        }
    }
}