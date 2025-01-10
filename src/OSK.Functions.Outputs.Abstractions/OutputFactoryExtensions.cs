using System;

namespace OSK.Functions.Outputs.Abstractions
{
    public static class OutputFactoryExtensions
    {
        #region Success

        public static IOutput Succeed(this IOutputFactory factory, OutputSpecificityCode specificityCode = OutputSpecificityCode.Success)
        {
            return factory.CreateOutput(OutputStatusCode.Success, null, null);
        }

        public static IOutput<TValue> Succeed<TValue>(this IOutputFactory factory, TValue value, OutputSpecificityCode specificityCode = OutputSpecificityCode.Success)
        {
            return factory.CreateOutput(value, OutputStatusCode.Success, null, null);
        }

        [Obsolete("Use succeed response method")]
        public static IOutputResponse<TValue> Success<TValue>(this IOutputFactory factory, TValue value, OutputSpecificityCode specificityCode = OutputSpecificityCode.Success)
        {
            return factory.SucceedResponse(value, specificityCode);
        }

        public static IOutputResponse SucceedResponse(this IOutputFactory factory, OutputSpecificityCode specificityCode = OutputSpecificityCode.Success)
        {
            return factory.BuildResponse()
                .AddSuccess(specificityCode)
                .BuildResponse();
        }

        public static IOutputResponse<TValue> SucceedResponse<TValue>(this IOutputFactory factory, TValue value, OutputSpecificityCode specificityCode = OutputSpecificityCode.Success)
        {
            return factory.BuildResponse<TValue>()
                .AddSuccess(value, specificityCode)
                .BuildResponse();
        }

        #endregion

        #region Fail

        public static IOutput Fail(this IOutputFactory factory, string error, OutputSpecificityCode specificityCode = OutputSpecificityCode.InvalidParameter,
            string originationSource = OutputStatusCode.DefaultSource)
        {
            return factory.CreateOutput(new OutputStatusCode(specificityCode, originationSource), 
                new ErrorInformation(new Error(error)), 
                null);
        }

        public static IOutput<TValue> Fail<TValue>(this IOutputFactory factory, string error, OutputSpecificityCode specificityCode = OutputSpecificityCode.InvalidParameter,
            string originationSource = OutputStatusCode.DefaultSource)
        {
            return factory.CreateOutput(default(TValue),
                new OutputStatusCode(specificityCode, originationSource),
                new ErrorInformation(new Error(error)),
                null);
        }

        [Obsolete("Use fail response method")]
        public static IOutputResponse Error(this IOutputFactory factory, string error, OutputSpecificityCode specificityCode = OutputSpecificityCode.InvalidParameter,
            string originationSource = OutputStatusCode.DefaultSource)
        {
            return factory.FailResponse(error, specificityCode, originationSource);
        }

        [Obsolete("Use fail response method")]
        public static IOutputResponse<TValue> Error<TValue>(this IOutputFactory factory, string error, OutputSpecificityCode specificityCode = OutputSpecificityCode.InvalidParameter,
            string originationSource = OutputStatusCode.DefaultSource)
        {
            return factory.FailResponse<TValue>(error, specificityCode, originationSource);
        }

        public static IOutputResponse FailResponse(this IOutputFactory factory, string error, OutputSpecificityCode specificityCode = OutputSpecificityCode.InvalidParameter,
            string originationSource = OutputStatusCode.DefaultSource)
        {
            return factory.BuildResponse()
                .WithOrigination(originationSource)
                .AddError(error, specificityCode)
                .BuildResponse();
        }

        public static IOutputResponse<TValue> FailResponse<TValue>(this IOutputFactory factory, string error, OutputSpecificityCode specificityCode = OutputSpecificityCode.InvalidParameter,
            string originationSource = OutputStatusCode.DefaultSource)
        {
            return factory.BuildResponse<TValue>()
                .WithOrigination(originationSource)
                .AddError(error, specificityCode)
                .BuildResponse();
        }

        #endregion

        #region Exception

        public static IOutput Exception(this IOutputFactory factory, Exception ex, string originationSource = OutputStatusCode.DefaultSource)
        {
            return factory.CreateOutput(new OutputStatusCode(OutputSpecificityCode.UnknownError, originationSource),
                new ErrorInformation(ex),
                null);
        }

        public static IOutput<TValue> Exception<TValue>(this IOutputFactory factory, Exception ex, string originationSource = OutputStatusCode.DefaultSource)
        {
            return factory.CreateOutput(default(TValue), new OutputStatusCode(OutputSpecificityCode.UnknownError, originationSource),
                new ErrorInformation(ex),
                null);
        }

        [Obsolete("Use exception response method")]
        public static IOutputResponse Fault(this IOutputFactory factory,
            Exception ex, string originationSource = OutputStatusCode.DefaultSource)
        {
            return factory.ExceptionResponse(ex, originationSource);
        }

        [Obsolete("Use exception method")]
        public static IOutputResponse<TValue> Fault<TValue>(this IOutputFactory factory,
            Exception ex, string originationSource = OutputStatusCode.DefaultSource)
        {
            return factory.ExceptionResponse<TValue>(ex, originationSource);
        }

        public static IOutputResponse ExceptionResponse(this IOutputFactory factory,
            Exception ex, string originationSource = OutputStatusCode.DefaultSource)
        {
            return factory.BuildResponse()
                .WithOrigination(originationSource)
                .AddException(ex)
                .BuildResponse();
        }

        public static IOutputResponse<TValue> ExceptionResponse<TValue>(this IOutputFactory factory,
            Exception ex, string originationSource = OutputStatusCode.DefaultSource)
        {
            return factory.BuildResponse<TValue>()
                .WithOrigination(originationSource)
                .AddException(ex)
                .BuildResponse();
        }

        #endregion
    }
}
