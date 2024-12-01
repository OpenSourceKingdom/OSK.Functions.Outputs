using OSK.Hexagonal.MetaData;

namespace OSK.Functions.Outputs.Abstractions
{
    [HexagonalPort(HexagonalPort.Primary)]
    public interface IOutput<TValue>: IOutput
    {
        TValue Value { get; }
    }
}
