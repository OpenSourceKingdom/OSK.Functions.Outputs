using System;
using System.Collections.Generic;
using System.Linq;

namespace OSK.Functions.Outputs.Abstractions
{
    public static class OutputFactoryExtensions
    {
        #region Success

        public static IOutput Success(this IOutputFactory factory, ResultSpecificityCode specificityCode = ResultSpecificityCode.None)
        {
            return factory.Create(new OutputInformation(FunctionResult.Success, resultSpecificityCode: specificityCode,
                errorInformation: null, originationSource: OutputDetails.DefaultSource));
        }

        public static IOutput<TValue> Success<TValue>(this IOutputFactory factory, TValue value, ResultSpecificityCode specificityCode = ResultSpecificityCode.None)
        {
            return factory.Create(value, new OutputInformation(FunctionResult.Success, resultSpecificityCode: specificityCode,
                errorInformation: null, originationSource: OutputDetails.DefaultSource));
        }

        #endregion

        #region Error

        public static IOutput Error(this IOutputFactory factory, string error, ResultSpecificityCode specificityCode = ResultSpecificityCode.InvalidData,
            string originationSource = OutputDetails.DefaultSource)
        {
            return factory.Create(new OutputInformation(FunctionResult.Error,
                resultSpecificityCode: specificityCode,
                errorInformation: new ErrorInformation([new Error(error)]),
                originationSource: originationSource));
        }

        public static IOutput Error(this IOutputFactory factory, IEnumerable<Error> errors, ResultSpecificityCode specificityCode = ResultSpecificityCode.InvalidData,
            string originationSource = OutputDetails.DefaultSource)
        {
            return factory.Create(new OutputInformation(FunctionResult.Error,
                resultSpecificityCode: specificityCode,
                errorInformation: new ErrorInformation(errors),
                originationSource: originationSource));
        }

        public static IOutput<TValue?> Error<TValue>(this IOutputFactory factory, string error, ResultSpecificityCode specificityCode = ResultSpecificityCode.InvalidData,
            string originationSource = OutputDetails.DefaultSource)
        {
            return factory.Create(default(TValue), new OutputInformation(FunctionResult.Error,
                resultSpecificityCode: specificityCode,
                errorInformation: new ErrorInformation([new Error(error)]),
                originationSource: originationSource));
        }

        public static IOutput<TValue?> Error<TValue>(this IOutputFactory factory, IEnumerable<Error> errors, ResultSpecificityCode specificityCode = ResultSpecificityCode.InvalidData,
            string originationSource = OutputDetails.DefaultSource)
        {
            return factory.Create(default(TValue), new OutputInformation(FunctionResult.Error,
                resultSpecificityCode: specificityCode,
                errorInformation: new ErrorInformation(errors),
                originationSource: originationSource));
        }

        #endregion

        #region Fault

        public static IOutput Fault(this IOutputFactory factory,
            Exception ex, string originationSource = OutputDetails.DefaultSource)
        {
            return factory.Create(FunctionResult.Fault, new OutputInformation(FunctionResult.Failed,
                resultSpecificityCode: ResultSpecificityCode.UnspecifiedException,
                errorInformation: new ErrorInformation(ex),
                originationSource: originationSource
                ));
        }

        public static IOutput Fault(this IOutputFactory factory,
            Exception ex, ResultSpecificityCode specificityCode, string originationSource = OutputDetails.DefaultSource)
        {
            return factory.Create(FunctionResult.Fault, new OutputInformation(FunctionResult.Failed,
                resultSpecificityCode: specificityCode,
                errorInformation: new ErrorInformation(ex),
                originationSource: originationSource
                ));
        }

        public static IOutput<TValue?> Fault<TValue>(this IOutputFactory factory,
            Exception ex, string originationSource = OutputDetails.DefaultSource)
        {
            return factory.Create(default(TValue), new OutputInformation(FunctionResult.Fault,
                resultSpecificityCode: ResultSpecificityCode.UnspecifiedException,
                errorInformation: new ErrorInformation(ex),
                originationSource: originationSource
                ));
        }

        public static IOutput<TValue?> Fault<TValue>(this IOutputFactory factory,
            Exception ex, ResultSpecificityCode specificityCode, string originationSource = OutputDetails.DefaultSource)
        {
            return factory.Create(default(TValue), new OutputInformation(FunctionResult.Fault,
                resultSpecificityCode: specificityCode,
                errorInformation: new ErrorInformation(ex),
                originationSource: originationSource
                ));
        }

        #endregion

        #region MultipleResults

        #endregion

        #region Paginated

        public static IOutput<PaginatedOutput<TValue>> Paginated<TValue>(this IOutputFactory factory,
            IEnumerable<TValue> items, long skip, long take, long? total)
        {
            return factory.Create(
                new PaginatedOutput<TValue>()
                {
                    Items = items.ToList(),
                    Skip = skip,
                    Take = take,
                    Total = total
                },
                new OutputInformation(FunctionResult.Success, resultSpecificityCode: ResultSpecificityCode.None,
                    errorInformation: null, originationSource: OutputDetails.DefaultSource));
        }

        #endregion
    }
}
