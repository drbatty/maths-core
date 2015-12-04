using System.Collections.Generic;
using CSharpExtensions.ContainerClasses;
using MathsCore.Sets;

namespace MathsCore.LinearAlgebra.DataMining.Clustering
{
    public class CentroidCluster<T> : Set<Vector<T, double>>
    {
        public Vector<T, double> Centroid { get; set; }

        public CentroidCluster(IEnumerable<Vector<T, double>> setVectors)
        {
            setVectors.Each(Add);
        }

        public override void Add(Vector<T, double> vector)
        {
            IncorporateIntoCentroid(vector);
            base.Add(vector);
        }

        private void IncorporateIntoCentroid(Vector<T, double> vector)
        {
            if (this.None())
            {
                Centroid = vector;
                return;
            }
            Centroid = (1 / ((double)Count + 1)) * ((Count * Centroid) + vector);
        }

        public void CalculateCentroid()
        {
            if (this.None())
                Centroid = null;
            Centroid = this.Inject(new Vector<T, double>(), (acc, v) => acc + v);
        }
    }
}