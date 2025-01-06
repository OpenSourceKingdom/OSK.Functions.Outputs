using System;
using System.Collections.Generic;
using System.Diagnostics;
using OSK.Functions.Outputs.Abstractions;
using OSK.Functions.Outputs.Models;

namespace OSK.Functions.Outputs
{
    public class OutputResponseBuilder(IOutputValidator validator) : IOutputResponseBuilder
    {
        #region Variables

        private string _originationSource = OutputStatusCode.DefaultSource;
        private Stopwatch _stopWatch;
        private bool _includeTimeStamp;

        private readonly IList<IOutput> _outputs = [];

        #endregion

        #region IOutputBuilder

        public IOutputResponseBuilder WithRunTimeMetric()
        {
            _stopWatch = Stopwatch.StartNew();
            return this;
        }

        public IOutputResponseBuilder WithTimeStamp()
        {
            _includeTimeStamp = true;
            return this;
        }

        public IOutputResponseBuilder WithOrigination(string originationSource)
        {
            _originationSource = originationSource;
            return this;
        }

        public IOutputResponseBuilder AddException(Exception exception)
        {
            if (exception is null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            var output = new Output(new OutputStatusCode(OutputSpecificityCode.UnknownError, _originationSource),
                new ErrorInformation(exception), GetDetails());
            validator.Validate(output);

            _outputs.Add(output);
            _stopWatch = null;

            return this;
        }

        public IOutputResponseBuilder AddError(string error, OutputSpecificityCode specificityCode)
        {
            var output = new Output(new OutputStatusCode(specificityCode, _originationSource), new ErrorInformation(new Error(error)),
                GetDetails());
            validator.Validate(output);

            _outputs.Add(output);
            _stopWatch = null;

            return this;
        }

        public IOutputResponseBuilder AddSuccess(OutputSpecificityCode specificityCode = OutputSpecificityCode.Success)
        {
            var output = new Output(new OutputStatusCode(specificityCode, _originationSource), null, GetDetails());
            validator.Validate(output);

            _outputs.Add(output);
            _stopWatch = null;

            return this;
        }

        public IOutputResponse BuildResponse()
        {
            if (_outputs.Count == 0)
            {
                throw new InvalidOperationException("No outputs have been added to build");
            }
            if (_outputs.Count == 1)
            {
                return new OutputResponse([_outputs[0]], _outputs[0].StatusCode, _outputs[0].AdvancedDetails);
            }

            double? totalRuntime = null;
            DateTime? outputTimeStamp = null;

            var statusOptions = new int[] { 0, 0, 0 };
            foreach (var output in _outputs)
            {
                if (output.AdvancedDetails.HasValue)
                {
                    totalRuntime = output.AdvancedDetails.Value.RunTimeInMilliseconds.HasValue
                        ? totalRuntime.GetValueOrDefault() + output.AdvancedDetails.Value.RunTimeInMilliseconds
                        : totalRuntime;
                    outputTimeStamp = output.AdvancedDetails.Value.CompletionTime.HasValue
                        ? outputTimeStamp.GetValueOrDefault() < output.AdvancedDetails.Value.CompletionTime
                            ? output.AdvancedDetails.Value.CompletionTime
                            : outputTimeStamp
                        : outputTimeStamp;
                }

                if (output.IsSuccessful)
                {
                    statusOptions[0] = 1;
                }
                else if (output.ErrorInformation?.Error is not null)
                {
                    statusOptions[1] = 1;
                }
                else if (output.ErrorInformation?.Exception is not null)
                {
                    statusOptions[2] = 1;
                }
            }

            OutputDetails? details = totalRuntime.HasValue || outputTimeStamp.HasValue
                ? new OutputDetails(totalRuntime, outputTimeStamp)
                : null;

            return new OutputResponse([.. _outputs],
                new OutputStatusCode(OutputSpecificityCode.MultipleOutputs, _originationSource),
                details);
        }

        #endregion

        #region Helpers

        private OutputDetails? GetDetails()
        {
            return _stopWatch is not null || _includeTimeStamp
                ? new OutputDetails(_stopWatch?.ElapsedMilliseconds, _includeTimeStamp ? DateTime.UtcNow : null)
                : null;
        }

        #endregion
    }
}
