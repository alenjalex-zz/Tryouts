using System;
using Microsoft.ML;
using Microsoft.ML.Trainers;
using Microsoft.ML.Transforms;

namespace HelloMl
{
    class Program
    {
        static void Main(string[] args)
        {
            // Creating a pipeline
            var pipeline = new LearningPipeline();

            var fileName = "iris-data.csv";
            pipeline.Add(new TextLoader<IrisData>(fileName, separator: ","));

            // Assign numeric values to the texts in Label column (4)
            pipeline.Add(new Dictionarizer("Label"));

            // Put all features into a vector
            pipeline.Add(new ColumnConcatenator("Features", "SepalLength", "SepalWidth", "PetalLength", "PetalWidth"));

            //Adding classifier
            pipeline.Add(new StochasticDualCoordinateAscentClassifier());

            pipeline.Add(new PredictedLabelColumnOriginalValueConverter
            {
                PredictedLabelColumn = "PredictedLabel"
            });

            var model = pipeline.Train<IrisData, IrisPrediction>();

            var prediction = model.Predict(new IrisData
            {
                SepalLength = 3.3f,
                SepalWidth = 1.6f,
                PetalLength = 0.2f,
                PetalWidth = 5.1f
            });

            System.Console.WriteLine($"Predicted flower type is : {prediction.PredictedLabels}");
        }
    }
}
