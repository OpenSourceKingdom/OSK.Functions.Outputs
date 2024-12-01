using System.Linq;

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
            if (output.ErrorInformation == null)
            {
                return string.Empty;
            }

            return output.ErrorInformation.Value.Exception == null
                ? string.Join(separator, output.ErrorInformation.Value.Errors.Select(e => e.Message))
                : output.ErrorInformation.Value.Exception.Message;
        }
    }
}
