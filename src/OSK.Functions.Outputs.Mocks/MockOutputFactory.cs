using OSK.Functions.Outputs.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OSK.Functions.Outputs.Mocks
{
    public class MockOutputFactory : IOutputFactory
    {
        #region IOutputFactory

        public IOutput Create(OutputStatusCode statusCode)
        {
            return CreateMockOutput(statusCode, null, null);
        }

        public IOutput Create(OutputStatusCode statusCode, IEnumerable<Error> errors)
        {
            return CreateMockOutput(statusCode, null, errors);
        }

        public IOutput Create(OutputStatusCode statusCode, Exception ex)
        {
            return CreateMockOutput(statusCode, ex, null);
        }

        public IOutput<TValue> Create<TValue>(TValue value, OutputStatusCode statusCode)
        {
            return CreateMockOutput(value, statusCode, null, null);
        }

        public IOutput<TValue> Create<TValue>(OutputStatusCode statusCode, IEnumerable<Error> errors)
        {
            return CreateMockOutput(statusCode, null, errors).AsType<TValue>();
        }

        public IOutput<TValue> Create<TValue>(OutputStatusCode statusCode, Exception ex)
        {
            return CreateMockOutput(statusCode, ex, null).AsType<TValue>();
        }

        #endregion

        #region Helpers

        private MockOutput<T> CreateMockOutput<T>(T value, OutputStatusCode statusCode, Exception ex, IEnumerable<Error> errors)
        {
            ErrorInformation? errorInformation = null;
            if (ex != null)
            {
                errorInformation = new ErrorInformation(ex);
            }
            else if (errors != null && errors.Any())
            {
                errorInformation = new ErrorInformation(errors);
            }

            return new MockOutput<T>()
            {
                Code = statusCode,
                ErrorInformation = errorInformation,
                Value = value
            };
        }

        private MockOutput CreateMockOutput(OutputStatusCode statusCode, Exception ex, IEnumerable<Error> errors)
        {
            ErrorInformation? errorInformation = null;
            if (ex != null)
            {
                errorInformation = new ErrorInformation(ex);
            }
            else if (errors != null && errors.Any())
            {
                errorInformation = new ErrorInformation(errors);
            }

            return new MockOutput()
            {
                Code = statusCode,
                ErrorInformation = errorInformation
            };
        }

        #endregion
    }
}
