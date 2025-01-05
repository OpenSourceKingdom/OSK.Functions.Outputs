﻿using System.Collections.Generic;

namespace OSK.Functions.Outputs.Abstractions
{
    public interface IOutput
    {
        bool IsSuccessful => StatusCode.IsSuccessful;

        OutputStatusCode StatusCode { get; }

        ErrorInformation? ErrorInformation { get; }

        OutputDetails? AdvancedDetails { get; }

        /// <summary>
        /// A helper method meant to help propogate error responses to the original caller. This method allows function stacks to push up the error information at the bottom
        /// </summary>
        /// <typeparam name="TValue">The type of object this output should represent</typeparam>
        /// <returns>A type casted response</returns>
        IOutput<TValue> AsOutput<TValue>();
    }
}
