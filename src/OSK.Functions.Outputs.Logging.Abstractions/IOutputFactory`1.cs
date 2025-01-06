using OSK.Functions.Outputs.Abstractions;
using OSK.Hexagonal.MetaData;

namespace OSK.Functions.Outputs.Logging.Abstractions
{
    [HexagonalIntegration(HexagonalIntegrationType.LibraryProvided, HexagonalIntegrationType.ConsumerPointOfEntry)]
    public interface IOutputFactory<TSource> : IOutputFactory
    {
    }
}
