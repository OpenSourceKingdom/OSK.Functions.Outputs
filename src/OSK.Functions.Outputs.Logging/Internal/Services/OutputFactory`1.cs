using OSK.Functions.Outputs.Abstractions;
using OSK.Functions.Outputs.Internal.Services;
using OSK.Functions.Outputs.Logging.Abstractions;
using System;
using System.Collections.Generic;

namespace OSK.Functions.Outputs.Logging.Internal.Services
{
    internal partial class OutputFactory<TSource>: OutputFactory, IOutputFactory<TSource>
    {
        #region IOutputFactory

        public override IOutput Create(OutputStatusCode statusCode)
        {
            var output = base.Create(statusCode);
            if (!output.IsSuccessful)
            {
                LogOutput(output);
            }

            return output;
        }

        public override IOutput Create(OutputStatusCode statusCode, IEnumerable<Error> errors)
        {
            var output = base.Create(statusCode, errors);
            if (!output.IsSuccessful)
            {
                LogOutput(output);
            }

            return output;
        }

        public override IOutput Create(OutputStatusCode statusCode, Exception ex)
        {
            var output = base.Create(statusCode, ex);
            if (!output.IsSuccessful)
            {
                LogOutput(output);
            }

            return output;
        }

        public override IOutput<TValue> Create<TValue>(TValue value, OutputStatusCode statusCode)
        {
            var output = base.Create(value, statusCode);
            if (!output.IsSuccessful)
            {
                LogOutput(output);
            }

            return output;
        }

        #endregion
    }
}
