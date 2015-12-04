using System.Collections.Generic;
using MathsCore.Interfaces;
using MathsCore.Sets;

namespace MathsCore.LinearAlgebra.DataMining.Clustering
{
    /// <summary>
    /// wraps a collection of metric sets with clustering functionality
    /// </summary>
    /// <typeparam name="T">the type of objects contained in the sets which must extend IMetric in itself</typeparam> 
    public class ClusterSet<T>
        where T : IMetric<T>
    {
        private Set<Set<T>> _clusters;
        //private IEnumerable<Set<Set<T>>> history = new List<Set<Set<T>>>();

        public ClusterSet()
        {
            _clusters = new Set<Set<T>>();
        }

        public ClusterSet(IEnumerable<T> sparseVectors)
        {
            _clusters = new Set<T>(sparseVectors).Singletons();
        }

        public void MergeClosest()
        {
            var closest = _clusters.Closest(_clusters);
            var set1 = closest.Item1.Item1;
            var set2 = closest.Item1.Item2;
            //add clone of clusters to history
            _clusters = _clusters - new Set<Set<T>> { set1, set2 };
            _clusters.Add(set1 | set2);
        }
    }
}