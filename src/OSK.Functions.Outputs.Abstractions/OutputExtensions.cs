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
        /// <returns>A single consolidated error string for the output</returns>
        public static string GetErrorString(this IOutput output)
        {
            if (output.ErrorInformation?.Exception is null
                || output.ErrorInformation?.Error is null)
            {
                return string.Empty;
            }

            return output.ErrorInformation.Value.Exception == null
                ? output.ErrorInformation.Value.Error.Value.Message
                : output.ErrorInformation.Value.Exception.Message;
        }
    }
}
