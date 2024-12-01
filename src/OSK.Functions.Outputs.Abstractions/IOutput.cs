using OSK.Hexagonal.MetaData;

namespace OSK.Functions.Outputs.Abstractions
{
    /// <summary>
    /// A simple output object for a function
    /// </summary>
    [HexagonalPort(HexagonalPort.Primary)]
    public interface IOutput
    {
        bool IsSuccessful => Code.IsSuccessCode;

        /// <summary>
        /// Contains non-error related information for a response response
        /// </summary>
        OutputStatusCode Code { get; }

        /// <summary>
        /// Specific error information that is only set in the event a response from a function is unsuccessful
        /// </summary>
        ErrorInformation? ErrorInformation { get; }

        /// <summary>
        /// A helper method meant to help propogate error responses to the original caller. This method allows function stacks to push up the error information at the bottom
        /// </summary>
        /// <typeparam name="TValue">The type of object this output should represent</typeparam>
        /// <returns>A type casted output</returns>
        IOutput<TValue> AsType<TValue>();
    }
}
