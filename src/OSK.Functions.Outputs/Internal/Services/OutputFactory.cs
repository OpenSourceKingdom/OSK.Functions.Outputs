using OSK.Functions.Outputs.Abstractions;
using OSK.Functions.Outputs.Models;
using System;

namespace OSK.Functions.Outputs.Internal.Services
{
    internal class OutputFactory : IOutputFactory
    {
        #region IOutputFactory

        public virtual IOutput Create(OutputInformation outputInformation)
        {
            if (outputInformation is null)
            {
                throw new ArgumentNullException(nameof(outputInformation));
            }

            var details = GetOutputDetails(outputInformation);
            ValidateOutput(details, outputInformation.ErrorInformation);
            return new Output(details, outputInformation.ErrorInformation);
        }

        public virtual IOutput<TValue> Create<TValue>(TValue value, OutputInformation outputInformation)
        {
            if (outputInformation is null)
            {
                throw new ArgumentNullException(nameof(outputInformation));
            }

            var details = GetOutputDetails(outputInformation);
            ValidateOutput(value, details, outputInformation.ErrorInformation);
            return new Output<TValue>(value, details, outputInformation.ErrorInformation);
        }

        #endregion

        #region Helpers

        private OutputDetails GetOutputDetails(OutputInformation outputInformation)
            => new OutputDetails(outputInformation.FunctionResult, outputInformation.ResultSpecificityCode, outputInformation.OriginationSource);
        
        private void ValidateOutput<TValue>(TValue value, OutputDetails details, ErrorInformation? errorInformation)
        {
            ValidateOutput(details, errorInformation);
            if (details.IsSuccessful && value is null)
            {
                throw new ArgumentNullException("A non-null value must be passed for a typed, successful, output.");
            }
        }

        private void ValidateOutput(OutputDetails details, ErrorInformation? errorInformation)
        {
            switch (details.Result)
            {
                case FunctionResult.Success:
                    if (errorInformation != null)
                    {
                        throw new InvalidOperationException("Unable to create a successful output that contains an error or exception.");
                    }
                    break;
                case FunctionResult.Error:
                case FunctionResult.Failed:
                case FunctionResult.Fault:
                    if (errorInformation is null)
                    {
                        throw new InvalidOperationException("Unable to create an error output that contains no error information.");
                    }
                    break;
            }
        }

        #endregion
    }
}
