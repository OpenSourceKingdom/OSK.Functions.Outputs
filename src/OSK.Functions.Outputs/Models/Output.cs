using OSK.Functions.Outputs.Abstractions;

namespace OSK.Functions.Outputs.Models
{
    public class Output : IOutput
    {
        #region Constructors

        internal Output(OutputStatusCode statusCode, ErrorInformation? errorInformation)
        {
            Code = statusCode;
            ErrorInformation = errorInformation;
        }

        #endregion

        #region IOutput

        public OutputStatusCode Code { get; }

        public ErrorInformation? ErrorInformation { get; }

        public virtual IOutput<TValue> AsType<TValue>()
        {
            return new Output<TValue>(this);
        }

        #endregion
    }
}
