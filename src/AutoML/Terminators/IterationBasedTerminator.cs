// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Linq;

namespace Microsoft.ML.PipelineInference2
{
    public sealed class IterationBasedTerminator
    {
        private readonly int _numTotalIterations;

        public IterationBasedTerminator(int numTotalIterations)
        {
            _numTotalIterations = numTotalIterations;
        }

        public bool ShouldTerminate(int numPreviousIterations)
        {
            return numPreviousIterations >= _numTotalIterations;
        }

        public int RemainingIterations(int numPreviousIterations)
        {
            return _numTotalIterations - numPreviousIterations;
        }
    }
}
