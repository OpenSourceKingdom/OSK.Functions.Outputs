using System;
using System.Collections.Generic;
using System.Text;
using OSK.Functions.Outputs.Abstractions;

namespace OSK.Functions.Outputs.Models
{
    public struct PaginatedOutput<TValue>(IList<TValue> values, long skip, long take, long? total)
        : IPaginatedOutput<TValue>
    {
        #region IPaginatedOutput

        public readonly long Skip => skip;

        public readonly long Take => take;

        public readonly long? Total => total;

        public readonly IList<TValue> Values => values;

        #endregion
    }
}
