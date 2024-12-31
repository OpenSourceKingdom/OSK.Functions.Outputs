using System.Diagnostics.CodeAnalysis;
using OSK.Hexagonal.MetaData;

namespace OSK.Functions.Outputs.Abstractions
{
    [HexagonalIntegration(HexagonalIntegrationType.LibraryProvided)]
    public interface IOutput<TValue>: IOutput
    {
        [MemberNotNullWhen(true, nameof(Value))]
        public new bool IsSuccessful => Details.IsSuccessful;

        TValue? Value { get; }
    }
}
