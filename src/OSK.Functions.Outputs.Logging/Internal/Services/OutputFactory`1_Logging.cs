using Microsoft.Extensions.Logging;
using OSK.Functions.Outputs.Abstractions;
using System;

namespace OSK.Functions.Outputs.Logging.Internal.Services
{
    internal partial class OutputFactory<TSource>(ILogger<TSource> logger)
    {
        #region Logging
       
        private void LogOutput(IOutput output)
        {
            if (output.IsSuccessful)
            {
                LogSuccess(output.StatusCode);
                return;
            }

            if (output.ErrorInformation.Value.Exception is null)
            {
                LogErrorInformation(output.StatusCode, output.GetErrorString());
            }
            else
            {
                LogExceptionInformation(output.StatusCode, output.ErrorInformation.Value.Exception);
            }
        }

        [LoggerMessage(eventId: 1, LogLevel.Debug, "Successful output. Status: {statusCode}")]
        private partial void LogSuccess(OutputStatusCode statusCode);

        [LoggerMessage(eventId: 2, LogLevel.Error, "Output Failed. Status: {statusCode} Reason: {errorMessage}")]
        private partial void LogErrorInformation(OutputStatusCode statusCode, string errorMessage);

        [LoggerMessage(eventId: 3, LogLevel.Critical, "Output Exception. Status: {statusCode}")]
        private  partial void LogExceptionInformation(OutputStatusCode statusCode, Exception ex);

        #endregion
    }
}
