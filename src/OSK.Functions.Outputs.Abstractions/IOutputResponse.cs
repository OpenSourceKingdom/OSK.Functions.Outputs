using OSK.Hexagonal.MetaData;

namespace OSK.Functions.Outputs.Abstractions
{
    [HexagonalIntegration(HexagonalIntegrationType.LibraryProvided)]
    public interface IOutputResponse
    {
        bool IsSuccessful { get; }

        OutputStatusCode StatusCode { get; }

        OutputDetails? AdvancedDetails { get; }

        IOutput[] Outputs { get; }

        /// <summary>
        /// A helper method meant to help propogate error responses to the original caller. This method allows function stacks to push up the error information at the bottom
        /// </summary>
        /// <typeparam name="TValue">The type of object this output should represent</typeparam>
        /// <returns>A type casted response</returns>
        IOutputResponse<TValue> AsResponse<TValue>();
    }
}
