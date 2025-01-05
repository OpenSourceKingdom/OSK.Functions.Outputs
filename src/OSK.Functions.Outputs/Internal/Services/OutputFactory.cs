using OSK.Functions.Outputs.Abstractions;
using OSK.Functions.Outputs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace OSK.Functions.Outputs.Internal.Services
{
    internal class OutputFactory : IOutputFactory
    {
        #region IOutputFactory

        public IOutputResponseBuilder CreateOutput()
        {
            return new OutputResponseBuilder(this);
        }

        public IOutputResponseBuilder<TValue> CreateOutput<TValue>()
        {
            return new OutputBuilder<TValue>(this);
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

        internal virtual IOutput Create(OutputSpecificityCode resultSpecificityCode,
            ErrorInformation? errorInformation, string originationSource, OutputDetails? details = null)
        {
            var statusCode = new OutputStatusCode(resultSpecificityCode, originationSource);

            ValidateOutput(statusCode, errorInformation);
            return new Output(statusCode, errorInformation, details);
        }

        internal virtual IOutput<TValue> Create<TValue>(TValue value, 
            OutputSpecificityCode resultSpecificityCode, ErrorInformation? errorInformation, string originationSource, 
            OutputDetails? details = null)
        {
            var statusCode = new OutputStatusCode(resultSpecificityCode, originationSource);

            ValidateOutput(value, statusCode, errorInformation);
            return new Output<TValue>(value, statusCode, errorInformation, details);
        }
        
        private void ValidateOutput<TValue>(TValue value, OutputStatusCode details, ErrorInformation? errorInformation)
        {
            ValidateOutput(details, errorInformation);
            if (details.IsSuccessful && value is null)
            {
                throw new ArgumentNullException("A non-null value must be passed for a typed, successful, output.");
            }
        }

        private void ValidateOutput(OutputStatusCode statusCode, ErrorInformation? errorInformation)
        {
            if (statusCode.IsSuccessful)
            {
                if (errorInformation != null)
                {
                    throw new InvalidOperationException("Unable to create a successful output that contains an error or exception.");
                }
                return;
            }

            if (errorInformation is null)
            {
                throw new InvalidOperationException("Unable to create an error output that contains no error information.");
            }
            if (errorInformation.Value.Exception is null &&
                errorInformation.Value.Error is null)
            {
                throw new InvalidOperationException("Unable to create an error output without valid error information set.");
            }

        }

        #endregion
    }
}
