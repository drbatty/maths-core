using System.Collections.Generic;
using System.Linq;
using MathsCore.Extensions.ContainerClasses;

namespace MathsCore.Sets
{
    public static class StringSetExtensions
    {
        public static Set<string> AddPrefix(this IEnumerable<string> strings, string prefix)
        {
            return strings.Select(s => s + prefix).ToSet();
        }

        public static IEnumerable<Set<string>> AddPrefix(this IEnumerable<IEnumerable<string>> stringSets, string prefix)
        {
            return stringSets.Select(s => AddPrefix(s, prefix)).ToSet();
        }
    }
}