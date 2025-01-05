using OSK.Functions.Outputs.Abstractions;

namespace OSK.Functions.Outputs.Models
{
    public class Output(OutputStatusCode statusCode, ErrorInformation? errorInformation, OutputDetails? details) 
        : IOutput
    {
        #region IOutput

        public OutputStatusCode StatusCode => statusCode;

        public ErrorInformation? ErrorInformation => errorInformation;

        public OutputDetails? AdvancedDetails => details;

        public virtual IOutput<TValue> AsOutput<TValue>()
        {
            return new Output<TValue>(this);
        }

        #endregion
    }
}
