using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CSharpExtensions;
using CSharpExtensions.ContainerClasses;
using MathsCore.Extensions.ContainerClasses;
using MathsCore.LinearAlgebra.Interfaces;

namespace MathsCore.LinearAlgebra
{
    public class Vector<TBasis, TField> : IVector<TBasis, TField>
    {
        private readonly Dictionary<TBasis, TField> _dictionary;

        public Vector(IDictionary<TBasis, TField> dictionary)
            : this()
        {
            dictionary.WhereValues(v => v != Field.Zero(typeof(TField))).EachKey(key => _dictionary[key] = dictionary[key]);
        }

        public Vector()
        {
            _dictionary = new Dictionary<TBasis, TField>();
        }

        public Vector(TBasis singleton)
            : this()
        {
            _dictionary = new Dictionary<TBasis, TField>();
            this[singleton] = 1;
        }

        #region binary vector operators

        public static Vector<TBasis, TField> operator +(Vector<TBasis, TField> v1, Vector<TBasis, TField> v2)
        {
            dynamic dV1 = v1;
            dynamic dV2 = v2;
            var result = new Vector<TBasis, TField>();
            v1.Keys.Where(v2.ContainsKey).Each(key => result[key] = dV1[key] + dV2[key]);
            v1.Keys.Where(key => !v2.ContainsKey(key)).Each(key => result[key] = v1[key]);
            v2.Keys.Where(key => !v1.ContainsKey(key)).Each(key => result[key] = v2[key]);
            return result;
        }

        public static Vector<TBasis, TField> operator *(dynamic d, Vector<TBasis, TField> v)
        {
            var result = new Vector<TBasis, TField>();
            if (d == Field.Zero(typeof(TField)))
                return result;
            dynamic dV = v;
            var dD = d;
            v.Keys.Each(key => result[key] = dV[key] * dD);
            return result;
        }

        public static Vector<TBasis, TField> operator -(Vector<TBasis, TField> v1, Vector<TBasis, TField> v2)
        {
            return v1 + -1 * v2;
        }

        #endregion

        #region IDictionary<TBasis, TField> members

        public bool ContainsKey(TBasis key)
        {
            return _dictionary.ContainsKey(key);
        }

        public void Add(TBasis key, TField value)
        {
            if (value != Field.Zero(typeof(TField)))
            {
                _dictionary[key] = value;
                return;
            }
            Remove(key);
        }

        public bool Remove(TBasis key)
        {
            return _dictionary.Remove(key);
        }

        //ncrunch: no coverage start
        public bool TryGetValue(TBasis key, out TField value)
        {
            throw new NotImplementedException();
        }
        //ncrunch: no coverage end

        TField IDictionary<TBasis, TField>.this[TBasis key]
        {
            get
            {
                return ContainsKey(key) ? _dictionary[key] : Field.Zero(typeof(TField));
            }
            set { _dictionary[key] = value; }
        }

        public ICollection<TBasis> Keys
        {
            get { return _dictionary.Keys; }
        }

        public ICollection<TField> Values
        {
            get { return _dictionary.Values; }
        }

        public IEnumerator<KeyValuePair<TBasis, TField>> GetEnumerator()
        {
            return _dictionary.GetEnumerator();
        }

        public void Add(KeyValuePair<TBasis, TField> item)
        {
            if (item.Value != Field.Zero(typeof(TField)))
                _dictionary.Add(item.Key, item.Value);
        }

        public void Clear()
        {
            _dictionary.Clear();
        }

        public bool Contains(KeyValuePair<TBasis, TField> item)
        {
            return _dictionary.Contains(item.WrapInList());
        }

        //ncrunch: no coverage start
        public void CopyTo(KeyValuePair<TBasis, TField>[] array, int arrayIndex)
        {
            //_dictionary.CopyTo(array, arrayIndex);
        }
        //ncrunch: no coverage end

        public bool Remove(KeyValuePair<TBasis, TField> item)
        {
            return _dictionary.Remove(item.Key);
        }

        public int Count
        {
            get { return _dictionary.Count; }
        }

        public bool IsReadOnly { get; private set; }

        #endregion

        #region indexer

        public dynamic this[TBasis index]
        {
            set
            {
                var dValue = value;
                if (dValue != Field.Zero(dValue.GetType()))
                    _dictionary[index] = dValue;
            }
            get
            {
                if (_dictionary.ContainsKey(index))
                {
                    var returnvalue = _dictionary[index];
                    return returnvalue;
                }
                return Field.Zero(typeof(TField));
            }
        }

        #endregion

        #region equality override

        public override bool Equals(object other)
        {
            if (other.Isnt<Vector<TBasis, TField>>())
                return false;
            if ((Keys.Any(key => !((Vector<TBasis, TField>)other).ContainsKey(key)) ||
                ((Vector<TBasis, TField>)other).Keys.Cast<object>().Any(o => !ContainsKey((TBasis)o))))
                return false;

            return Keys.All(key => ((Vector<TBasis, TField>)other)[key].Equals(this[key]));
        }

        public override int GetHashCode()
        {
            return Keys.Select(key => this[key]).Aggregate(0, (current, dValue) => (int)(current + dValue));
        }

        //ncrunch: no coverage start
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        //ncrunch: no coverage end

        #endregion

        #region vector operations

        public bool IsZero()
        {
            return Keys.None();
        }

        public dynamic Dot(IVector<TBasis, TField> other)
        {
            var keys = Keys.Where(key => ContainsKey(key) && other.ContainsKey(key));
            var result = Field.Zero(typeof(TField));
            // ReSharper disable LoopCanBeConvertedToQuery - Resharper error - can't refactor using Linq
            foreach (var key in keys)
                // ReSharper restore LoopCanBeConvertedToQuery
                result += this[key] * other[key];
            return result;
        }

        public IVector<TBasis, TField> PointWise(IVector<TBasis, TField> other, Func<TField, TField, TField> λ)
        {
            var result = new Vector<TBasis, TField>();
            Keys.Where(other.ContainsKey).Each(k => result[k] = λ(this[k], other[k]));
            return result;
        }

        public dynamic DiceCoefficient(IVector<TBasis, TField> other)
        {
            return (2 * Dot(other)) / (Dot(this) + other.Dot(other));
        }

        public double Modulus()
        {
            return Math.Sqrt(Dot(this));
        }

        public IVector<TBasis, TField> Normalize()
        {
            // ReSharper disable CompareOfFloatsByEqualityOperator
            return Modulus() == 0 ? this : (1 / Modulus()) * this;
            // ReSharper restore CompareOfFloatsByEqualityOperator
        }

        #endregion

        public bool AtLeast(TField element, TField threshold)
        {
            dynamic dynEl = element;
            return dynEl >= threshold;
        }

        public IVector<TBasis, TField> Truncate(TField threshold)
        {
            return new Vector<TBasis, TField>(this.WhereValues(v => AtLeast(v, threshold)));
        }

        #region util



        public override string ToString()
        {
            return IsZero() ? "ZERO" : Keys.OrderBy(k => -this[k]).Inject(String.Empty,
                (acc, key) => acc + this[key].ToString() + "|" + key.ToString() + "> ");
        }

        #endregion

        #region IMetric<Vector<T,S>> implementation

        public double Distance(IVector<TBasis, TField> v)
        {
            return 1 - Normalize().Dot(v.Normalize());
        }

        #endregion
    }
}