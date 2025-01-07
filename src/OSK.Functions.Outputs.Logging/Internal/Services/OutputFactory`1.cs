using OSK.Functions.Outputs.Abstractions;
using OSK.Functions.Outputs.Internal.Services;
using OSK.Functions.Outputs.Logging.Abstractions;

namespace OSK.Functions.Outputs.Logging.Internal.Services
{
    internal partial class OutputFactory<TSource>: OutputFactory, IOutputFactory<TSource>
    {
        #region IOutputFactory

        public override IOutput CreateOutput(OutputStatusCode statusCode, ErrorInformation? errorInformation,
            OutputDetails? advancedDetails)
        {
            var output = base.CreateOutput(statusCode, errorInformation, advancedDetails);
            LogOutput(output);
            return output;
        }

        public override IOutput<TValue> CreateOutput<TValue>(TValue value, OutputStatusCode statusCode, 
            ErrorInformation? errorInformation, OutputDetails? advancedDetails)
        {
            var output = base.CreateOutput(value, statusCode, errorInformation, advancedDetails);
            LogOutput(output);
            return output;
        }

        #endregion
    }
}
