using System;
using System.Linq;
using System.Net;

namespace OSK.Functions.Outputs.Abstractions
{
    public static class OutputExtensions
    {
        /// <summary>
        /// This will generate an error string based on the error information; i.e. consilidating multiple errors into a single error string, exception message into one, etc. 
        /// </summary>
        /// <param name="output">The output object to generate an error string for</param>
        /// <param name="separator">The delimeter between error messages, if multiple exist on the output</param>
        /// <returns>A single consolidated error string for the output</returns>
        public static string GetErrorString(this IOutput output, string separator = ",")
        {
            if (output.ErrorInformation?.Exception is null
                || output.ErrorInformation?.Errors is null)
            {
                return string.Empty;
            }

            return output.ErrorInformation.Value.Exception == null
                ? string.Join(separator, output.ErrorInformation.Value.Errors.Select(e => e.Message))
                : output.ErrorInformation.Value.Exception.Message;
        }

        /// <summary>
        /// Attempts to map an output to a <see cref="HttpStatusCode"/>. This function may not be supported for all outputs. 
        /// For any output conversion not immediately supported, an exception will be thrown.
        /// </summary>
        /// <param name="output">The output to convert to an HttpStatusCode</param>
        /// <returns>The supported conversion to <see cref="HttpStatusCode"/> or <see cref="HttpStatusCode.NotImplemented"/> if not supported.</returns>
        public static HttpStatusCode AsStatusCode(this IOutput output)
        {
            return output.Details.SpecificityCode switch
            {
                ResultSpecificityCode.Created => HttpStatusCode.Created,
                ResultSpecificityCode.Updated => HttpStatusCode.OK,
                ResultSpecificityCode.MultipleResults => HttpStatusCode.MultiStatus,
                ResultSpecificityCode.Accepted => HttpStatusCode.Accepted,
                ResultSpecificityCode.Deleted => HttpStatusCode.NoContent,
                ResultSpecificityCode.BadGateway => HttpStatusCode.BadGateway,
                ResultSpecificityCode.DuplicateData => HttpStatusCode.Conflict,
                ResultSpecificityCode.ResourceNotFound => HttpStatusCode.NotFound,
                ResultSpecificityCode.NotAuthenticated => HttpStatusCode.Unauthorized,
                ResultSpecificityCode.InsufficientPermissions => HttpStatusCode.Forbidden,
                ResultSpecificityCode.InvalidData => HttpStatusCode.BadRequest,
                ResultSpecificityCode.ServiceUnavailable => HttpStatusCode.ServiceUnavailable,
                ResultSpecificityCode.ServerError => HttpStatusCode.InternalServerError,
                ResultSpecificityCode.Locked => HttpStatusCode.Locked,
                ResultSpecificityCode.MethodNotImplemented => HttpStatusCode.NotImplemented,
                _ => throw new InvalidCastException($"Unable to cast a specificity code of {output.Details.SpecificityCode} to an HttpStatusCode.")
            };
        }
    }
}
