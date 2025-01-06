using OSK.Functions.Outputs.Abstractions;
using OSK.Functions.Outputs.Internal.Services;
using OSK.Functions.Outputs.Logging.Abstractions;

namespace OSK.Functions.Outputs.Logging.Internal.Services
{
    internal partial class OutputFactory<TSource>: OutputFactory, IOutputFactory<TSource>
    {
        #region IOutputFactory

        public override void Validate(IOutput output)
        {
            base.Validate(output);
            LogOutput(output);
        }

        public override void Validate<TValue>(IOutput<TValue> output)
        {
            base.Validate(output);
            LogOutput(output);
        }

        #endregion
    }
}
