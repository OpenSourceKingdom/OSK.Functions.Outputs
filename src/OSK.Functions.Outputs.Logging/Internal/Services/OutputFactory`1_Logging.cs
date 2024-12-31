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
                LogSuccess(output.Details);
                return;
            }

            if (output.ErrorInformation.Value.Exception is null)
            {
                LogErrorInformation(output.Details, output.GetErrorString($"{Environment.NewLine}"));
            }
            else
            {
                LogExceptionInformation(output.Details, output.ErrorInformation.Value.Exception);
            }
        }

        [LoggerMessage(eventId: 1, LogLevel.Debug, "Successful output. Status: {code}")]
        private partial void LogSuccess(OutputDetails code);

        [LoggerMessage(eventId: 2, LogLevel.Error, "Output Failed. Status: {details} Reason: {errorMessage}")]
        private partial void LogErrorInformation(OutputDetails details, string errorMessage);

        [LoggerMessage(eventId: 3, LogLevel.Critical, "Output Exception. Status: {details}")]
        private  partial void LogExceptionInformation(OutputDetails details, Exception ex);

        #endregion
    }
}
