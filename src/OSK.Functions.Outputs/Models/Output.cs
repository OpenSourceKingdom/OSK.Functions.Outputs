using OSK.Functions.Outputs.Abstractions;

namespace OSK.Functions.Outputs.Models
{
    public class Output : IOutput
    {
        #region Constructors

        internal Output(OutputDetails details, ErrorInformation? errorInformation)
        {
            Details = details;
            ErrorInformation = errorInformation;
        }

        #endregion

        #region IOutput

        public OutputDetails Details { get; }

        public ErrorInformation? ErrorInformation { get; }

        public virtual IOutput<TValue> AsOutput<TValue>()
        {
            return new Output<TValue>(this);
        }

        #endregion
    }
}
