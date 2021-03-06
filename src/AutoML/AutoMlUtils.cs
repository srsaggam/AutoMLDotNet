// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms;

namespace Microsoft.ML.Auto
{
    internal static class AutoMlUtils
    {
        public static Random Random = new Random();

        public static void Assert(bool boolVal, string message = null)
        {
            if(!boolVal)
            {
                message = message ?? "Assertion failed";
                throw new InvalidOperationException(message);
            }
        }

        public static IDataView Take(this IDataView data, int count)
        {
            // REVIEW: This should take an env as a parameter, not create one.
            var env = new MLContext();
            var take = SkipTakeFilter.Create(env, new SkipTakeFilter.TakeArguments { Count = count }, data);
            return new CacheDataView(env, data, Enumerable.Range(0, data.Schema.Count).ToArray());
        }

        public static IList<int> GetColumnIndexList(TextLoader.Range[] ranges)
        {
            var indexList = new List<int>();
            foreach(var range in ranges)
            {
                for(var i = range.Min; i <= range.Max; i++)
                {
                    indexList.Add(i);
                }
            }
            return indexList;
        }

        public static Schema.Column GetColumn(this IDataView data, string columnName)
        {
            var column = data.Schema.GetColumnOrNull(columnName);
            if(column == null)
            {
                throw new ArgumentException($"Column '{columnName}' not found in data.");
            }
            return column.Value;
        }
    }
}