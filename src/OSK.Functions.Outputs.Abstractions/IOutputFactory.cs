using System;
using System.Collections.Generic;
using OSK.Hexagonal.MetaData;

namespace OSK.Functions.Outputs.Abstractions
{
    [HexagonalPort(HexagonalPort.Primary)]
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
