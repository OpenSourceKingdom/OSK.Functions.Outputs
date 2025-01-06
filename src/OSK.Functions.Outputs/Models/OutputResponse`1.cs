using System.Linq;
using OSK.Functions.Outputs.Abstractions;

namespace OSK.Functions.Outputs.Models
{
    public class OutputResponse<TValue>(IOutput<TValue>[] outputs,
        OutputStatusCode statusCode, OutputDetails? details)
        : IOutputResponse<TValue>
    {
        #region IOutputResponse

        public bool IsSuccessful => statusCode.IsSuccessful;

        IOutput[] IOutputResponse.Outputs => outputs.Cast<IOutput>().ToArray();

        public IOutput<TValue>[] Outputs => outputs;

        public OutputStatusCode StatusCode => statusCode;

        public OutputDetails? AdvancedDetails => details;

        public IOutputResponse<T> AsResponse<T>()
        {
            return new OutputResponse<T>(outputs.Select(output => output.AsOutput<T>()).ToArray(), StatusCode,
                details);
        }

        #endregion
    }
}
