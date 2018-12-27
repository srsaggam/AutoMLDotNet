﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.ML.PipelineInference2
{
    internal static class SweepableParams
    {
        public static readonly IEnumerable<SweepableParam> AveragePerceptron = AveragedLinearArgs
            .Concat(OnlineLinearArgs);

        public static readonly IEnumerable<SweepableParam> FastForest = TreeArgs;

        public static readonly IEnumerable<SweepableParam> FastTree = TreeArgs;

        public static readonly IEnumerable<SweepableParam> FastTreeTweedie = TreeArgs;

        public static readonly IEnumerable<SweepableParam> LightGbm = new SweepableParam[]
        {
            new SweepableDiscreteParam("NumBoostRound", new object[] { 10, 20, 50, 100, 150, 200 }),
            new SweepableFloatParam("LearningRate", 0.025f, 0.4f, isLogScale: true),
            new SweepableLongParam("NumLeaves", 2, 128, isLogScale: true, stepSize: 4),
            new SweepableDiscreteParam("MinDataPerLeaf", new object[] { 1, 10, 20, 50 }),
            new SweepableDiscreteParam("UseSoftmax", new object[] { true, false }),
            new SweepableDiscreteParam("UseCat", new object[] { true, false }),
            new SweepableDiscreteParam("UseMissing", new object[] { true, false }),
            new SweepableDiscreteParam("MinDataPerGroup", new object[] { 10, 50, 100, 200 }),
            new SweepableDiscreteParam("MaxCatThreshold", new object[] { 8, 16, 32, 64 }),
            new SweepableDiscreteParam("CatSmooth", new object[] { 1, 10, 20 }),
            new SweepableDiscreteParam("CatL2", new object[] { 0.1, 0.5, 1, 5, 10 }),

            // TreeBooster params
            new SweepableDiscreteParam("RegLambda", new object[] { 0f, 0.5f, 1f }),
            new SweepableDiscreteParam("RegAlpha", new object[] { 0f, 0.5f, 1f })
        };

        public static readonly IEnumerable<SweepableParam> LinearSvm = new SweepableParam[] {
            new SweepableFloatParam("Lambda", 0.00001f, 0.1f, 10, isLogScale: true),
            new SweepableDiscreteParam("PerformProjection", null, isBool: true),
            new SweepableDiscreteParam("NoBias", null, isBool: true)
        }.Concat(OnlineLinearArgs);

        public static readonly IEnumerable<SweepableParam> LogisticRegression = LbfgsArgs;

        public static readonly IEnumerable<SweepableParam> OnlineGradientDescent = AveragedLinearArgs;

        public static readonly IEnumerable<SweepableParam> PoissonRegression = LbfgsArgs;

        public static readonly IEnumerable<SweepableParam> Sdca = new SweepableParam[] {
            new SweepableDiscreteParam("L2Const", new object[] { "<Auto>", 1e-7f, 1e-6f, 1e-5f, 1e-4f, 1e-3f, 1e-2f }),
            new SweepableDiscreteParam("L1Threshold", new object[] { "<Auto>", 0f, 0.25f, 0.5f, 0.75f, 1f }),
            new SweepableDiscreteParam("ConvergenceTolerance", new object[] { 0.001f, 0.01f, 0.1f, 0.2f }),
            new SweepableDiscreteParam("MaxIterations", new object[] { "<Auto>", 10, 20, 100 }),
            new SweepableDiscreteParam("Shuffle", null, isBool: true),
            new SweepableDiscreteParam("BiasLearningRate", new object[] { 0.0f, 0.01f, 0.1f, 1f })
        };

        public static readonly IEnumerable<SweepableParam> OrdinaryLeastSquares = new SweepableParam[] {
            new SweepableDiscreteParam("L2Weight", new object[] { 1e-6f, 0.1f, 1f })
        };

        public static readonly IEnumerable<SweepableParam> Sgd = new SweepableParam[] {
            new SweepableDiscreteParam("L2Const", new object[] { 1e-7f, 5e-7f, 1e-6f, 5e-6f, 1e-5f }),
            new SweepableDiscreteParam("ConvergenceTolerance", new object[] { 1e-2f, 1e-3f, 1e-4f, 1e-5f }),
            new SweepableDiscreteParam("MaxIterations", new object[] { 1, 5, 10, 20 }),
            new SweepableDiscreteParam("Shuffle", null, isBool: true),
        };

        public static readonly IEnumerable<SweepableParam> SymSgd = new SweepableParam[] {
            new SweepableDiscreteParam("NumberOfIterations", new object[] { 1, 5, 10, 20, 30, 40, 50 }),
            new SweepableDiscreteParam("LearningRate", new object[] { "<Auto>", 1e1f, 1e0f, 1e-1f, 1e-2f, 1e-3f }),
            new SweepableDiscreteParam("L2Regularization", new object[] { 0.0f, 1e-5f, 1e-5f, 1e-6f, 1e-7f }),
            new SweepableDiscreteParam("UpdateFrequency", new object[] { "<Auto>", 5, 20 })
        };

        private static readonly IEnumerable<SweepableParam> AveragedLinearArgs =
            new SweepableParam[]
            {
                new SweepableDiscreteParam("LearningRate", new object[] { 0.01, 0.1, 0.5, 1.0 }),
                new SweepableDiscreteParam("DecreaseLearningRate", new object[] { false, true }),
                new SweepableFloatParam("L2RegularizerWeight", 0.0f, 0.4f),
            };

        private static readonly IEnumerable<SweepableParam> OnlineLinearArgs =
            new SweepableParam[]
            {
                new SweepableLongParam("NumIterations", 1, 100, stepSize: 10, isLogScale: true),
                new SweepableFloatParam("InitWtsDiameter", 0.0f, 1.0f, numSteps: 5),
                new SweepableDiscreteParam("Shuffle", new object[] { false, true }),
            };

        private static readonly IEnumerable<SweepableParam> TreeArgs =
           new SweepableParam[]
           {
                new SweepableLongParam("NumLeaves", 2, 128, isLogScale: true, stepSize: 4),
                new SweepableDiscreteParam("MinDocumentsInLeafs", new object[] { 1, 10, 50 }),
                new SweepableDiscreteParam("NumTrees", new object[] { 20, 100, 500 }),
                new SweepableFloatParam("LearningRates", 0.025f, 0.4f, isLogScale: true),
                new SweepableFloatParam("Shrinkage", 0.025f, 4f, isLogScale: true),
           };

        private static readonly IEnumerable<SweepableParam> LbfgsArgs =
            new SweepableParam[] {
                new SweepableFloatParam("L2Weight", 0.0f, 1.0f, numSteps: 4),
                new SweepableFloatParam("L1Weight", 0.0f, 1.0f, numSteps: 4),
                new SweepableDiscreteParam("OptTol", new object[] { 1e-4f, 1e-7f }),
                new SweepableDiscreteParam("MemorySize", new object[] { 5, 20, 50 }),
                new SweepableLongParam("MaxIterations", 1, int.MaxValue),
                new SweepableFloatParam("InitWtsDiameter", 0.0f, 1.0f, numSteps: 5),
                new SweepableDiscreteParam("DenseOptimizer", new object[] { false, true }),
            };
    }
}