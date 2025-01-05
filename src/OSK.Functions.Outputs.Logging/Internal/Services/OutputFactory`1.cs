using OSK.Functions.Outputs.Abstractions;
using OSK.Functions.Outputs.Internal.Services;
using OSK.Functions.Outputs.Logging.Abstractions;
using OSK.Functions.Outputs.Models;

namespace OSK.Functions.Outputs.Logging.Internal.Services
{
    internal partial class OutputFactory<TSource>: OutputFactory, IOutputFactory<TSource>
    {
        #region IOutputFactory

        internal override IOutput Create(OutputSpecificityCode resultSpecificityCode,
            ErrorInformation? errorInformation, string originationSource, OutputDetails? details = null)
        {
            var output = base.Create(resultSpecificityCode, errorInformation, originationSource, details);
            LogOutput(output);

            return output;
        }

        internal override IOutput<TValue> Create<TValue>(TValue value,
            OutputSpecificityCode resultSpecificityCode, ErrorInformation? errorInformation, string originationSource,
            OutputDetails? details = null)
        {
            var output = base.Create(value, resultSpecificityCode, errorInformation, originationSource, details);
            LogOutput(output);

            return output;
        }

        #endregion
    }
}
