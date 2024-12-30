using OSK.Functions.Outputs.Abstractions;
using OSK.Hexagonal.MetaData;

namespace OSK.Functions.Outputs.Logging.Abstractions
{
    [HexagonalIntegration(HexagonalIntegrationType.LibraryProvided)]
    public interface IOutputFactory<TSource> : IOutputFactory
    {
    }
}
