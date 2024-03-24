using Microsoft.Extensions.Logging;
using OSK.Functions.Outputs.Abstractions;
using System;

namespace OSK.Functions.Outputs.Logging.Internal.Services
{
    internal partial class OutputFactory<TSource>
    {
        #region Variables

        private readonly ILogger<TSource> _logger;

        #endregion

        #region Constructors

        public OutputFactory(ILogger<TSource> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #endregion

        #region Logging
       
        private void LogOutput(IOutput output)
        {
            if (output.ErrorInformation.HasValue)
            {
                _logger.Log(
                    LogLevel.Error,
                    0,
                    output.ErrorInformation.Value.Exception,
                    "Output Failed. Status: {code} Reason: {errorMessage}",
                    output.Code, output.GetErrorString("\r\n"));
            }
            else
            {
                LogSuccess(output.Code);
            }
        }

        [LoggerMessage(eventId: 1, LogLevel.Information, "Successful output. Status: {code}")]
        private partial void LogSuccess(OutputStatusCode code);

        #endregion
    }
}
