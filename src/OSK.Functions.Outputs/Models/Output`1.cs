using OSK.Functions.Outputs.Abstractions;
using System;

namespace OSK.Functions.Outputs.Models
{
    public class Output<TValue> : Output, IOutput<TValue>
    {
        #region Constructors

        internal Output(TValue value, OutputStatusCode statusCode, ErrorInformation? errorInformation,
            OutputDetails? details)
            : base(statusCode, errorInformation, details)
        {
            Value = value;
        }

        internal Output(Output output)
            : base(output.StatusCode, output.ErrorInformation, output.AdvancedDetails)
        {
            Value = default;
        }

        #endregion

        #region IOutput

        public TValue Value { get; }

        public override IOutput<TType> AsOutput<TType>()
        {
            if (StatusCode.IsSuccessful)
            {
                // This cast should only be occurring on error cases
                throw new InvalidOperationException("Unable to cast a typed output that was successful.");
            }

            return base.AsOutput<TType>();
        }

        #endregion
    }
}
