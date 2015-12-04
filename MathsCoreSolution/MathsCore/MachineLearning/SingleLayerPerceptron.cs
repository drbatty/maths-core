using System;
using System.Collections.Generic;
using MathsCore.LinearAlgebra;

namespace MathsCore.MachineLearning
{
    public class SingleLayerPerceptron
    {
        public double Bias { get; set; }

        public double LearningRate { get; set; }

        public double Threshold { get; set; }

        public List<Tuple<Vector<string, double>, double>> TrainingSet { get; set; }

        public Vector<string, double> Weights { get; set; }

        public int Evaluate(Vector<string, double> x)
        {
            return Weights.Dot(x) + Bias > 0 ? 1 : 0;
        }

        public void Learn()
        {
            for (var i = 0; i < 10; i++)
            {
                var errorCount = 0;
                Console.WriteLine();

                foreach (var tuple in TrainingSet)
                {
                    Console.WriteLine("weights: " + Weights);

                    var result = tuple.Item1.Dot(Weights) > Threshold ? 1 : 0;
                    var error = tuple.Item2 - result;
                    if (error == 0) continue;
                    errorCount += 1;
                    Weights += LearningRate * error * tuple.Item1;
                }

                if (errorCount == 0)
                    break;
            }
        }
    }
}