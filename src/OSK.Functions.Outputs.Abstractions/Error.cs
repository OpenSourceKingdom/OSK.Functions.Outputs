﻿namespace OSK.Functions.Outputs.Abstractions
{
    public struct Error
    {
        #region Variables

        public string Message { get; }

        #endregion

        #region Constructors

        public Error(string message)
        {
            Message = message;
        }

        #endregion
    }
}
