using System;
using System.Collections.Generic;
using System.Linq;

namespace OSK.Functions.Outputs.Abstractions
{
    public struct ErrorInformation
    {
        #region Variables

        public Exception Exception { get; set; }

        public IReadOnlyCollection<Error> Errors { get; set; }

        #endregion

        #region Constructors

        public ErrorInformation(Exception exception)
        {
            Exception = exception;
            Errors = Enumerable.Empty<Error>().ToList();
        }

        public ErrorInformation(IEnumerable<Error> errors)
        {
            Errors = errors.ToList();
            Exception = null;
        }


        #endregion
    }
}
