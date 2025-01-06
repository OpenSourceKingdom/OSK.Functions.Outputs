using OSK.Functions.Outputs.Abstractions;
using OSK.Functions.Outputs.Internal.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OSK.Functions.Outputs.Mocks
{
    public class MockOutputFactory : IOutputFactory, IOutputValidator
    {
        public IOutputResponseBuilder CreateOutput()
        {
            return new OutputResponseBuilder(this);
        }

        public IOutputResponseBuilder<TValue> CreateOutput<TValue>()
        {
            return new OutputResponseBuilder<TValue>(this);
        }

        public IPaginatedOutput<TValue> CreatePage<TValue>(IEnumerable<TValue> values, long skip, long take, long? total)
        {
            throw new NotImplementedException();
        }

        public void Validate(IOutput output)
        {
            // No logic necessary for mocks
        }

        public void Validate<TValue>(IOutput<TValue> output)
        {
            // No logic necessary for mocks
        }
    }
}
