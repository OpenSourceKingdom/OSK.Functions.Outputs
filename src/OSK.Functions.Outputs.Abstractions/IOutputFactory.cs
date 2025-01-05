using System.Collections.Generic;
using OSK.Hexagonal.MetaData;

namespace OSK.Functions.Outputs.Abstractions
{
    [HexagonalIntegration(HexagonalIntegrationType.LibraryProvided)]
    public interface IOutputFactory
    {
        IOutputResponseBuilder CreateOutput();

        IOutputResponseBuilder<TValue> CreateOutput<TValue>();

        IPaginatedOutput<TValue> CreatePage<TValue>(IEnumerable<TValue> values, long skip, long take, long? total);
    }
}
