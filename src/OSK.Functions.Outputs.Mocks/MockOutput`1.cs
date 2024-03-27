using OSK.Functions.Outputs.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace OSK.Functions.Outputs.Mocks
{
    public class MockOutput<T> : MockOutput, IOutput<T>
    {
        public T Value { get; set; }
    }
}
