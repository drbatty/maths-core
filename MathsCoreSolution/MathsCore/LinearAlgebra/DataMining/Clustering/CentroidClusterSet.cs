using System;
using System.Collections.Generic;
using CSharpExtensions.ContainerClasses;
using MathsCore.Extensions.ContainerClasses;
using MathsCore.Sets;

namespace MathsCore.LinearAlgebra.DataMining.Clustering
{
    public class CentroidClusterSet<T> : Set<CentroidCluster<T>>
    {
        public void AddVector(Vector<T, double> vector)
        {
            var minCluster = this.Minimizer(clusterSet => clusterSet.Centroid.Distance(vector));
            minCluster.Item1.Add(vector);
        }

        public CentroidClusterSet(IEnumerable<Vector<T, double>> sparseVectors)
        {
            new Set<Vector<T, double>>(sparseVectors).Singletons().Each(singleton => Add(new CentroidCluster<T>(singleton)));
        }

        public override string ToString()
        {
            return this.Inject(string.Empty, (acc, cluster) => acc + cluster.Count + " " + cluster.Centroid + Environment.NewLine);
        }
    }
}