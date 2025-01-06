using OSK.Functions.Outputs.Abstractions;
using OSK.Functions.Outputs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace OSK.Functions.Outputs.Internal.Services
{
    internal class OutputFactory : IOutputFactory, IOutputValidator
    {
        #region IOutputFactory

        public IOutputResponseBuilder CreateOutput()
        {
            return new OutputResponseBuilder(this);
        }

        public IOutputResponseBuilder<TValue> CreateOutput<TValue>()
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

        #region IOutputValidator

        public virtual void Validate(IOutput output)
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

        public virtual void Validate<TValue>(IOutput<TValue> output)
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
