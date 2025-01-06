using System.Linq;
using OSK.Functions.Outputs.Abstractions;

namespace OSK.Functions.Outputs.Models
{
    public class OutputResponse(IOutput[] outputs,
        OutputStatusCode statusCode, OutputDetails? details) 
        : IOutputResponse
    {
        #region IOutputResponse

        public bool IsSuccessful => statusCode.IsSuccessful;

        public IOutput[] Outputs => outputs;

        public OutputStatusCode StatusCode => statusCode;

        public OutputDetails? AdvancedDetails => details;

        public IOutputResponse<TValue> AsResponse<TValue>()
        {
            return new OutputResponse<TValue>(outputs.Select(output => output.AsOutput<TValue>()).ToArray(), statusCode, 
                details);
        }

        #endregion
    }
}
