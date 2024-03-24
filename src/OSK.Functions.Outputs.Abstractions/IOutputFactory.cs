using System;
using System.Collections.Generic;

namespace OSK.Functions.Outputs.Abstractions
{
    public interface IOutputFactory
    {
        IOutput Create(OutputStatusCode statusCode);

        IOutput Create(OutputStatusCode statusCode, IEnumerable<Error> errors);

        IOutput Create(OutputStatusCode statusCode, Exception ex);

        IOutput<TValue> Create<TValue>(TValue value, OutputStatusCode statusCode);

        IOutput<TValue> Create<TValue>(OutputStatusCode statusCode, IEnumerable<Error> errors);

        IOutput<TValue> Create<TValue>(OutputStatusCode statusCode, Exception ex);
    }
}
