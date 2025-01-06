using OSK.Hexagonal.MetaData;

namespace OSK.Functions.Outputs.Abstractions
{
    [HexagonalIntegration(HexagonalIntegrationType.LibraryProvided)]
    public interface IOutputResponse<TValue>: IOutputResponse
    {
        new IOutput<TValue>[] Outputs { get; }
    }
}
