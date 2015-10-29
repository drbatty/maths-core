using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CSharpExtensions;
using CSharpExtensions.ContainerClasses;
using MathsCore.Extensions.ContainerClasses;

namespace MathsCore.Sets
{
    public class Set<T> : ICollection<T>
    {
        protected readonly List<T> Elements = new List<T>();

        #region constructors

        public Set()
        {

        }

        public Set(IEnumerable<T> iEnumerable)
        {
            iEnumerable.Each(Add);
        }

        public static Set<T> Empty = new Set<T>();

        #endregion

        #region indexing operator

        public T this[int index]
        {
            get { return Elements[index]; }
            set { Elements[index] = value; }
        }

        #endregion

        #region explicit cast operators

        public static explicit operator Set<T>(Collection<T> collection)
        {
            return new Set<T>(collection);
        }

        public static explicit operator Set<T>(List<T> list)
        {
            return new Set<T>(list);
        }

        #endregion

        #region ICollection<T> Members;

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            return Elements.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        //ncrunch: no coverage start

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Elements.GetEnumerator();
        }

        //ncrunch: no coverage end

        #endregion

        public virtual void Add(T t)
        {
            if (!Elements.Contains(t))
                Elements.Add(t);
        }

        public void Clear()
        {
            Elements.Clear();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            Elements.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return Elements.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(T item)
        {
            return Elements.Remove(item);
        }

        #endregion

        #region extra add methods

        public void Add(params T[] ts)
        {
            ts.Each(Add);
        }

        public void Add(ICollection<T> other)
        {
            other.Each(Add);
        }

        #endregion

        #region containment methods

        public bool Contains(T t)
        {
            return Elements.Contains(t);
        }

        public bool Contains(Set<T> other)
        {
            return other.All(Contains);
        }

        #endregion

        #region union and intersection

        public Set<T> Intersect(Set<T> other)
        {
            return new Set<T>(this.Where(other.Contains));
        }

        public Set<T> Union(Set<T> other)
        {
            return new Set<T> { this, other };
        }

        public bool Meets(Set<T> other)
        {
            return Intersect(other).Any();
        }

        #endregion

        #region union | and intersection & operators

        public static Set<T> operator &(Set<T> set1, Set<T> set2)
        {
            return set1.Intersect(set2);
        }

        public static Set<T> operator |(Set<T> set1, Set<T> set2)
        {
            return set1.Union(set2);
        }

        #endregion

        #region set binary operators - and +

        public static Set<T> operator -(Set<T> set1, Set<T> set2)
        {
            return new Set<T>(set1.Where(t => !set2.Contains(t)));
        }

        public static Set<T> operator +(Set<T> set1, Set<T> set2)
        {
            return set1.Union(set2) - set1.Intersect(set2);
        }

        #endregion

        #region comparison operators

        public static bool operator >(Set<T> set1, Set<T> set2)
        {
            return set1.Contains(set2) && !(set2.Contains(set1));
        }

        public static bool operator <(Set<T> set1, Set<T> set2)
        {
            return set2.Contains(set1) && !(set1.Contains(set2));
        }

        public static bool operator >=(Set<T> set1, Set<T> set2)
        {
            return set1.Contains(set2);
        }

        public static bool operator <=(Set<T> set1, Set<T> set2)
        {
            return set2.Contains(set1);
        }

        public static bool operator <(T t, Set<T> set) // substitute for "is an element of" notation
        {
            return set.Contains(t);
        }

        // ncrunch: no coverage start

        public static bool operator >(T t, Set<T> set) // doesn't make much sense but forced to define it
        {
            return new Set<T> { t }.Contains(set);
        }

        // ncrunch: no coverage end

        public static bool operator ==(Set<T> set1, Set<T> set2)
        {
            if ((object)set1 == null)
                return (object)set2 == null;
            return set1.Equals(set2);
        }

        public static bool operator !=(Set<T> set1, Set<T> set2)
        {
            if ((object)set1 == null)
                return (object)set2 != null;
            return !set1.Equals(set2);
        }

        #endregion

        #region operations on sets of sets

        public Set<Set<T>> Singletons()
        {
            var singletons = new Set<Set<T>>();
            this.Each(t => singletons.Add(new Set<T> { t }));
            return singletons;
        }

        #endregion

        #region list delegation

        public void RemoveAt(int index)
        {
            Elements.RemoveAt(index);
        }

        #endregion

        #region power set

        public Set<Set<T>> PowerSet() // todo currently this is hopelessly inefficient - can it be improved?
        {
            var result = new Set<Set<T>> { this };
            if (this.None())
                return result;
            this.Each(element => result |= new Set<Set<T>> { this.Exclude(element).ToSet().PowerSet() });
            return result;
        }

        public Set<Set<T>> PowerSet(Func<Set<T>, bool> filter) // nb may be more efficient to apply filter first rather than create whole power set, e.g. skeletons
        {
            return PowerSet().Where(filter).ToSet();
        }

        // todo make more efficient, e.g. exclude filter from top or build up sets from smallest set first

        #endregion

        public Set<Tuple<T, TOther>> CartesianProduct<TOther>(Set<TOther> other)
        {
            return this.SelectMany(s => other.Select(t => new Tuple<T, TOther>(s, t))).ToSet();
        }

        public override bool Equals(object other)
        {
            if (other.Isnt<Set<T>>())
                return false;
            return this <= (Set<T>)other && this >= (Set<T>)other;
        }

        public override int GetHashCode()
        {
            return this.Aggregate(0, (acc, t) => acc + t.GetHashCode());
        }

        public IDictionary<T, TValue> ToDictionary<TValue>(Func<T, TValue> valueGenerator)
        {
            var result = new Dictionary<T, TValue>();
            this.Each(element => result[element] = valueGenerator(element));
            return result;
        }

        #region util

        public override string ToString()
        {
            return "{" + this.CommaSeparate() + "}";
        }

        #endregion

    }
}