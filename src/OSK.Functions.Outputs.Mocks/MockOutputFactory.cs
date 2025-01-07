using OSK.Functions.Outputs.Abstractions;
using OSK.Functions.Outputs.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OSK.Functions.Outputs.Mocks
{
    public class MockOutputFactory : IOutputFactory
    {
        public IOutputResponseBuilder BuildResponse()
        {
            return new OutputResponseBuilder(this);
        }

        public IOutputResponseBuilder<TValue> BuildResponse<TValue>()
        {
            return new OutputResponseBuilder<TValue>(this);
        }

        public IOutput CreateOutput(OutputStatusCode statusCode, ErrorInformation? errorInformation, OutputDetails? advancedDetails)
        {
            return new Output(statusCode, errorInformation, advancedDetails);
        }

        public IOutput<TValue> CreateOutput<TValue>(TValue value, OutputStatusCode statusCode, ErrorInformation? errorInformation,
            OutputDetails? advancedDetails)
        {
            return new Output<TValue>(value, statusCode, errorInformation, advancedDetails);
        }

        public IPaginatedOutput<TValue> CreatePage<TValue>(IEnumerable<TValue> values, long skip, long take, long? total)
        {
            return new PaginatedOutput<TValue>(values.ToList(), skip, take, total);
        }
    }
}
