using System;

namespace OSK.Functions.Outputs.Abstractions
{
    public readonly struct ErrorInformation
    {
        #region Variables

        public Exception Exception { get; }

        public Error? Error { get; }

        #endregion

        #region Constructors

        public ErrorInformation(Exception exception)
        {
            Exception = exception;
        }

        public ErrorInformation(Error error)
        {
            Error = error;
        }

        #endregion
    }
}
