using OSK.Functions.Outputs.Abstractions;
using OSK.Functions.Outputs.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OSK.Functions.Outputs.Internal.Services
{
    internal class OutputFactory : IOutputFactory
    {
        #region IOutputFactory

        public virtual IOutput Create(OutputStatusCode statusCode)
        {
            ValidateOutput(statusCode, null, null);
            return CreateOutput(statusCode, null, null);
        }

        public virtual IOutput Create(OutputStatusCode statusCode, IEnumerable<Error> errors)
        {
            ValidateOutput(statusCode, errors, null);
            return CreateOutput(statusCode, errors, null);
        }

        public virtual IOutput Create(OutputStatusCode statusCode, Exception ex)
        {
            ValidateOutput(statusCode, null, ex);
            return CreateOutput(statusCode, null, ex);
        }

        public virtual IOutput<TValue> Create<TValue>(TValue value, OutputStatusCode statusCode)
        {
            ValidateOutput(value, statusCode, null, null);
            return CreateOutput(value, statusCode, null, null);
        }

        public virtual IOutput<TValue> Create<TValue>(OutputStatusCode statusCode, IEnumerable<Error> errors)
        {
            ValidateOutput(statusCode, errors, null);
            return CreateOutput(default(TValue), statusCode, errors, null);
        }

        public virtual IOutput<TValue> Create<TValue>(OutputStatusCode statusCode, Exception ex)
        {
            ValidateOutput(statusCode, null, ex);
            return CreateOutput(default(TValue), statusCode, null, ex);
        }

        #endregion

        #region Helpers

        private IOutput CreateOutput(OutputStatusCode statusCode, IEnumerable<Error> errors,
            Exception ex)
        {
            ErrorInformation? errorInformation = null;
            if (errors != null && errors.Any())
            {
                errorInformation = new ErrorInformation(errors);
            }
            else if (ex != null)
            {
                errorInformation = new ErrorInformation(ex);
            }

            return new Output(statusCode, errorInformation);
        }

        private IOutput<TValue> CreateOutput<TValue>(TValue value, OutputStatusCode statusCode, 
            IEnumerable<Error> errors, Exception ex)
        {
            ErrorInformation? errorInformation = null;
            if (errors != null && errors.Any())
            {
                errorInformation = new ErrorInformation(errors);
            }
            else if (ex != null)
            {
                errorInformation = new ErrorInformation(ex);
            }

            return new Output<TValue>(value, statusCode, errorInformation);
        }

        private void ValidateOutput<TValue>(TValue value, OutputStatusCode statusCode, 
            IEnumerable<Error> errors, Exception ex)
        {
            ValidateOutput(statusCode, errors, ex);
            if (statusCode.IsSuccessCode && value is null)
            {
                throw new ArgumentNullException("A non-null value must be passed for a typed, successful, output.");
            }
        }

        private void ValidateOutput(OutputStatusCode statusCode, IEnumerable<Error> errors, Exception ex)
        {
            switch (statusCode.IsSuccessCode)
            {
                case true:
                    if ((errors != null && errors.Any()) || ex != null)
                    {
                        throw new InvalidOperationException("Unable to create a successful output that contains an error or exception.");
                    }
                    break;
                case false:
                    if ((errors == null || !errors.Any()) && ex == null)
                    {
                        throw new InvalidOperationException("Unable to create an error output that contains no error information.");
                    }
                    break;
            }
        }

        #endregion
    }
}
