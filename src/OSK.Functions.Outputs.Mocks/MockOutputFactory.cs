using OSK.Functions.Outputs.Abstractions;
using System;
using System.Collections.Generic;

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
            throw new NotImplementedException();
        }

        public IOutput<TValue> CreateOutput<TValue>(TValue value, OutputStatusCode statusCode, ErrorInformation? errorInformation,
            OutputDetails? advancedDetails)
        {
            throw new NotImplementedException();
        }

        public IPaginatedOutput<TValue> CreatePage<TValue>(IEnumerable<TValue> values, long skip, long take, long? total)
        {
            throw new NotImplementedException();
        }
    }
}
