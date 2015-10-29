using System;
using System.Collections.Generic;
using System.Linq;
using CSharpExtensions.ContainerClasses;
using MathsCore.Sets;

namespace MathsCore.Extensions.ContainerClasses
{
    public static class DictionaryMappingExtensions
    {
        #region restriction, image and preimage

        public static IDictionary<TKey, TValue> Restrict<TKey, TValue>(this IDictionary<TKey, TValue> dictionary,
            Set<TKey> keys)
        {
            return (keys & dictionary.Keys.ToSet()).ToDictionary(k => k, k => dictionary[k]);
        }

        public static Set<TValue> Image<TKey, TValue>(this IDictionary<TKey, TValue> dictionary,
            Set<TKey> keys)
        {
            return dictionary.Restrict(keys).Values.ToSet();
        }

        public static Set<TKey> Preimage<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, Set<TValue> values)
        {
            return dictionary.Keys.Where(k => dictionary[k] < values).ToSet();
        }

        public static IDictionary<TKey, TValue> Restrict<TKey, TValue>(this IDictionary<TKey, TValue> dictionary,
            Set<TValue> values)
        {
            return dictionary.Restrict(dictionary.Preimage(values));
        }

        #endregion

        #region filtering

        public static IDictionary<TKey, TValue> WhereKeys<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, Func<TKey, bool> π)
        {
            return dictionary.Restrict(dictionary.Keys.Where(π).ToSet());
        }

        public static IDictionary<TKey, TValue> WhereValues<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, Func<TValue, bool> π)
        {
            return dictionary.Restrict(dictionary.Values.Where(π).ToSet());
        }

        #endregion

        #region equality and equivalence

        public static bool IsEquivalentMappingTo<TKey, TValue>(this IDictionary<TKey, TValue> dictionary,
            IDictionary<TKey, TValue> other)
        {
            return dictionary.Keys.ToSet() == other.Keys.ToSet() &&
                   dictionary.AllKeyValues((key, value) => other[key].Equals(value));
        }

        #endregion
    }
}