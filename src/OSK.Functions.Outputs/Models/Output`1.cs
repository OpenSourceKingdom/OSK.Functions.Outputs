using OSK.Functions.Outputs.Abstractions;
using System;

namespace OSK.Functions.Outputs.Models
{
    public class Output<TValue> : Output, IOutput<TValue>
    {
        #region Constructors

        internal Output(TValue value, OutputDetails statusCode, ErrorInformation? errorInformation)
            : base(statusCode, errorInformation)
        {
            Value = value;
        }

        internal Output(Output output)
            : base(output.Details, output.ErrorInformation)
        {
            Value = default;
        }

        #endregion

        #region IOutput

        public TValue Value { get; }

        public override IOutput<TType> AsOutput<TType>()
        {
            if (Details.IsSuccessful)
            {
                // Prevent a loss of the typed value due to boxing/dropping it for the type change.
                // This is mainly used for passing errors back up to callers
                throw new InvalidOperationException("Unable to cast a typed output that was successful.");
            }

            return base.AsOutput<TType>();
        }

        #endregion
    }
}
