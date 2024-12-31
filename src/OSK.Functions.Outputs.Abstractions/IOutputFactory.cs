using OSK.Hexagonal.MetaData;

namespace OSK.Functions.Outputs.Abstractions
{
    [HexagonalIntegration(HexagonalIntegrationType.LibraryProvided)]
    public interface IOutputFactory
    {
        IOutput Create(OutputInformation outputInformation);

        IOutput<TValue> Create<TValue>(TValue value, OutputInformation outputInformation);
    }
}
