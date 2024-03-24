using System.Linq;

namespace OSK.Functions.Outputs.Abstractions
{
    public static class OutputExtensions
    {
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
