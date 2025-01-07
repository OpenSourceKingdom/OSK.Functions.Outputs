using OSK.Functions.Outputs.Abstractions;

namespace OSK.Functions.Outputs.Models
{
    public class Output: IOutput
    {
        #region Constructors

        internal Output(OutputStatusCode statusCode, ErrorInformation? errorInformation, OutputDetails? details)
        {
            StatusCode = statusCode;
            ErrorInformation = errorInformation;
            AdvancedDetails = details;
        }

        #endregion

        #region IOutput

        public OutputStatusCode StatusCode { get; }

        public ErrorInformation? ErrorInformation { get; }

        public OutputDetails? AdvancedDetails { get; }

        public virtual IOutput<TValue> AsOutput<TValue>()
        {
            return new Output<TValue>(this);
        }

        #endregion
    }
}
