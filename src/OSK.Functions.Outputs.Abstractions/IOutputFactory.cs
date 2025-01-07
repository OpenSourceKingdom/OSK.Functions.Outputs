using System.Collections.Generic;
using OSK.Hexagonal.MetaData;

namespace OSK.Functions.Outputs.Abstractions
{
    [HexagonalIntegration(HexagonalIntegrationType.LibraryProvided, HexagonalIntegrationType.ConsumerPointOfEntry)]
    public interface IOutputFactory
    {
        /// <summary>
        /// Provides a mechanism for building an <see cref="IOutputResponse"/>. This provides access to some extra data that can
        /// be added to an output response by the library, as well as an output aggregation response
        /// </summary>
        /// <returns>A response builder</returns>
        IOutputResponseBuilder BuildOutput();

        /// <summary>
        /// Provides a mechanism for building an <see cref="IOutputResponse{TValue}"/>. This provides access to some extra data that can
        /// be added to an output response by the library, as well as an output aggregation response
        /// </summary>
        /// <returns>A response builder</returns>
        IOutputResponseBuilder<TValue> BuildOutput<TValue>();

        /// <summary>
        /// Creates a basic output object.
        /// </summary>
        /// <param name="statusCode">The status code for the output</param>
        /// <param name="errorInformation">Error data associated to the output</param>
        /// <param name="advancedDetails">Extra data that is associated to the output for debug purposes</param>
        /// <returns>An output with the provided parameters</returns>
        IOutput CreateOutput(OutputStatusCode statusCode, ErrorInformation? errorInformation,
            OutputDetails? advancedDetails);

        /// <summary>
        /// Creates a basic output object.
        /// </summary>
        /// <param name="value">The value associated to the output</param>
        /// <param name="statusCode">The status code for the output</param>
        /// <param name="errorInformation">Error data associated to the output</param>
        /// <param name="advancedDetails">Extra data that is associated to the output for debug purposes</param>
        /// <returns>An output with the provided parameters</returns>
        IOutput<TValue> CreateOutput<TValue>(TValue value, OutputStatusCode statusCode, ErrorInformation? errorInformation,
            OutputDetails? advancedDetails);

        IPaginatedOutput<TValue> CreatePage<TValue>(IEnumerable<TValue> values, long skip, long take, long? total);
    }
}
