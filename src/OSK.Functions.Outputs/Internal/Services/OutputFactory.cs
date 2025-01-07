using OSK.Functions.Outputs.Abstractions;
using OSK.Functions.Outputs.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OSK.Functions.Outputs.Internal.Services
{
    internal class OutputFactory : IOutputFactory
    {
        #region IOutputFactory

        public virtual IOutput CreateOutput(OutputStatusCode statusCode, ErrorInformation? errorInformation, OutputDetails? advancedDetails)
        {
            var output = new Output(statusCode, errorInformation, advancedDetails);
            Validate(output);

            return output;
        }

        public virtual IOutput<TValue> CreateOutput<TValue>(TValue value, OutputStatusCode statusCode, ErrorInformation? errorInformation, 
            OutputDetails? advancedDetails)
        {
            var output = new Output<TValue>(value, statusCode, errorInformation, advancedDetails);
            Validate(output);

            return output;
        }

        public IOutputResponseBuilder BuildOutput()
        {
            return new OutputResponseBuilder(this);
        }

        public IOutputResponseBuilder<TValue> BuildOutput<TValue>()
        {
            return new OutputResponseBuilder<TValue>(this);
        }

        public IPaginatedOutput<TValue> CreatePage<TValue>(IEnumerable<TValue> values, long skip, long take, long? total)
        {
            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }
            if (skip < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(skip));
            }
            if (take < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(take));
            }

            return new PaginatedOutput<TValue>(values.ToList(), skip, take, total);
        }

        #endregion

        #region Helpers

        internal void Validate(IOutput output)
        {
            if (output.StatusCode.IsSuccessful)
            {
                if (output.ErrorInformation != null)
                {
                    throw new InvalidOperationException("Unable to create a successful output that contains an error or exception.");
                }
                return;
            }

            if (output.ErrorInformation is null)
            {
                throw new InvalidOperationException("Unable to create an error output that contains no error information.");
            }
            if (output.ErrorInformation.Value.Exception is null &&
                output.ErrorInformation.Value.Error is null)
            {
                throw new InvalidOperationException("Unable to create an error output without valid error information set.");
            }
        }

        internal void Validate<TValue>(IOutput<TValue> output)
        {
            Validate((IOutput)output);
            if (output.IsSuccessful && output.Value is null)
            {
                throw new ArgumentNullException("A non-null value must be passed for a typed, successful, output.");
            }
        }

        #endregion
    }
}
