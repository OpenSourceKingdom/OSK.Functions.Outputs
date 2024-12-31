using System.Collections.Generic;

namespace OSK.Functions.Outputs.Abstractions
{
    /// <summary>
    /// An output that can help facilitate pagination
    /// </summary>
    /// <typeparam name="TValue">The type of object that the response enumerable contains</typeparam>
    public class PaginatedOutput<TValue>
    {
        /// <summary>
        /// The skip count associated to the response
        /// </summary>
        public long Skip { get; set; }

        /// <summary>
        /// The take count associated to the response. Note: it is assumed that the take is the size of the list, not necessarily a request.
        /// i.e. if a user requests take 10 but only 6 items are in the list, the take count should reflect 6 for the 6 actually taken
        /// </summary>
        public long Take { get; set; }

        /// <summary>
        /// An optional value that helps with pagination. This represents the total amount of items that are associated to the request.
        /// </summary>
        public long? Total { get; set; }

        public IList<TValue> Items { get; set; } = [];
    }
}
