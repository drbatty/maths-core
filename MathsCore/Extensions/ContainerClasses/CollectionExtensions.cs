using System.Collections.Generic;

namespace MathsCore.Extensions.ContainerClasses
{
    public static class CollectionExtensions
    {
        public static void AddRange<T>(this ICollection<T> destination, IEnumerable<T> source)
        {
            foreach (var item in source)
                destination.Add(item);
        }
    }
}