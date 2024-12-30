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

        public override IOutput Create(OutputInformation outputInformation)
        {
            var output = base.Create(outputInformation);
            LogOutput(output);

            return output;
        }

        public override IOutput<TValue> Create<TValue>(TValue value, OutputInformation outputInformation)
        {
            var output = base.Create(value, outputInformation);
            LogOutput(output);

            return output;
        }

        #endregion
    }
}
