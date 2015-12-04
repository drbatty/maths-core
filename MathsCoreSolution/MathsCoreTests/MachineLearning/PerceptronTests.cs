using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathsCoreTests.MachineLearning
{
    [TestClass]
    public class PerceptronTests
    {
        /* [TestMethod]
         public void SingleLayerPerceptronShouldLearnNand()
         {
             var weights = new Vector<string, double>();

             var v1 = new Vector<string, double>();
             v1["x"] = 1;

             var v2 = new Vector<string, double>();
             v2["x"] = 1;
             v2["z"] = 1;

             var v3 = new Vector<string, double>();
             v3["x"] = 1;
             v3["y"] = 1;

             var v4 = new Vector<string, double>();
             v4["x"] = 1;
             v4["y"] = 1;
             v4["z"] = 1;

             var trainingSet = new List<Tuple<Vector<string, double>, double>>
             {
                 new Tuple<Vector<string, double>, double>(v1, 1), 
                 new Tuple<Vector<string, double>, double>(v2, 1),
                 new Tuple<Vector<string, double>, double>(v3, 1),
                 new Tuple<Vector<string, double>, double>(v4, 0)
             };

             var perceptron = new SingleLayerPerceptron
             {
                 Bias = 0,
                 Weights = weights,
                 LearningRate = 0.1,
                 Threshold = 0.5,
                 TrainingSet = trainingSet
             };

             perceptron.Learn();

             var learntWeights = perceptron.Weights;
             Assert.IsTrue(learntWeights["x"] == 0.8);
             Assert.IsTrue(learntWeights["y"] == -0.2);
             Assert.IsTrue(learntWeights["z"] == -0.1);
         }

     */
    }
}