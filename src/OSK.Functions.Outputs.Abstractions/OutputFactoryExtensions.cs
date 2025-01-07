using System;

namespace OSK.Functions.Outputs.Abstractions
{
    public static class OutputFactoryExtensions
    {
        #region Success

        public static IOutputResponse Success(this IOutputFactory factory, OutputSpecificityCode specificityCode = OutputSpecificityCode.Success)
        {
            return factory.BuildResponse()
                .AddSuccess(specificityCode)
                .BuildResponse();
        }

        public static IOutputResponse<TValue> Success<TValue>(this IOutputFactory factory, TValue value, OutputSpecificityCode specificityCode = OutputSpecificityCode.Success)
        {
            return factory.BuildResponse<TValue>()
                .AddSuccess(value, specificityCode)
                .BuildResponse();
        }        

        #endregion

        #region Error

        public static IOutputResponse Error(this IOutputFactory factory, string error, OutputSpecificityCode specificityCode = OutputSpecificityCode.InvalidParameter,
            string originationSource = OutputStatusCode.DefaultSource)
        {
            return factory.BuildResponse()
                .WithOrigination(originationSource)
                .AddError(error, specificityCode)
                .BuildResponse();
        }

        public static IOutputResponse<TValue> Error<TValue>(this IOutputFactory factory, string error, OutputSpecificityCode specificityCode = OutputSpecificityCode.InvalidParameter,
            string originationSource = OutputStatusCode.DefaultSource)
        {
            return factory.BuildResponse<TValue>()
                .WithOrigination(originationSource)
                .AddError(error, specificityCode)
                .BuildResponse();
        }

        #endregion

        #region Fault

        public static IOutputResponse Fault(this IOutputFactory factory,
            Exception ex, string originationSource = OutputStatusCode.DefaultSource)
        {
            return factory.BuildResponse()
                .WithOrigination(originationSource)
                .AddException(ex)
                .BuildResponse();
        }

        public static IOutputResponse<TValue> Fault<TValue>(this IOutputFactory factory,
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
