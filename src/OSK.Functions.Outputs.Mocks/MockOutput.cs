using OSK.Functions.Outputs.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace OSK.Functions.Outputs.Mocks
{
    public class MockOutput : IOutput
    {
        public OutputStatusCode Code { get; set; }

        public ErrorInformation? ErrorInformation { get; set; }

        public IOutput<TValue> AsType<TValue>()
        {
            return new MockOutput<TValue>()
            {
                Code = Code,
                ErrorInformation = ErrorInformation,
                Value = default
            };
        }
    }
}
