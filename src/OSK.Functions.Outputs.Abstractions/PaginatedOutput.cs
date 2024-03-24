using System.Collections.Generic;

namespace OSK.Functions.Outputs.Abstractions
{
    public class PaginatedOutput<TValue>
    {
        public long Skip { get; set; }

        public long Take { get; set; }

        public long? Total { get; set; }

        public IList<TValue> Items { get; set; }
    }
}
