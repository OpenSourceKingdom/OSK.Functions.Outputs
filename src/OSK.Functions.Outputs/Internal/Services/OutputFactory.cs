using OSK.Functions.Outputs.Abstractions;
using OSK.Functions.Outputs.Models;
using System;
using System.Runtime.CompilerServices;

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
            if (details.Result == FunctionResult.Success)
            {
                if (errorInformation != null)
                {
                    throw new InvalidOperationException("Unable to create a successful output that contains an error or exception.");
                }

                ValidateSpecificityForFunctionResult(details.SpecificityCode, 20, 29);
                return;
            }

            if (errorInformation is null)
            {
                throw new InvalidOperationException("Unable to create an error output that contains no error information.");
            }
            if (errorInformation.Value.Exception is null &&
                errorInformation.Value.Errors is null)
            {
                throw new InvalidOperationException("Unable to create an error output without valid error information set.");
            }

            ValidateSpecificityForFunctionResult(details.SpecificityCode, 30, 59);
        }

        #endregion

        #region Helpers

        private void ValidateSpecificityForFunctionResult(ResultSpecificityCode specificityCode, int minSpecificityCode,
            int maxSpecificityCode)
        {
            if (specificityCode is ResultSpecificityCode.None ||
                specificityCode is ResultSpecificityCode.SpecificityNotRecognized)
            {
                return;
            }
            var numericCode = (int)specificityCode;
            for (var magnitude = 0; magnitude < 1; magnitude++)
            {
                var multiple = (int)Math.Pow(10, magnitude);
                minSpecificityCode *= multiple;
                maxSpecificityCode *= multiple;

                if (numericCode >= minSpecificityCode && numericCode <= maxSpecificityCode)
                {
                    return;
                }
            }

            throw new InvalidOperationException("The specificity code provide could not be set with the related function result.");
        }

        #endregion
    }
}
