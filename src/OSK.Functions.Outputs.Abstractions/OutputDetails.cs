using System;
using System.Net;
using System.Reflection;

namespace OSK.Functions.Outputs.Abstractions
{
    public struct OutputDetails
    {
        #region Static

        public static readonly OutputDetails Success = new OutputDetails(FunctionResult.Success, ResultSpecificityCode.None, DefaultSource);

        public static OutputDetails Parse(string statusCode)
        {
            var statusCodeParts = statusCode.Split(',');
            if (statusCodeParts.Length < 1 || statusCodeParts.Length > 2)
            {
                throw new InvalidOperationException("The expected status code parts were not found.");
            }

            var statusParts = statusCodeParts[0].Split(".");
            if (statusParts.Length != 2)
            {
                throw new InvalidOperationException("The expected status parts were not found.");
            }

            if (!int.TryParse(statusParts[0], out var functionResult)
                && !Enum.IsDefined(typeof(FunctionResult), functionResult))
            {
                throw new InvalidOperationException("The expected http status code was not valid.");
            }
            if (!int.TryParse(statusParts[1], out var specificityCode)
                && !Enum.IsDefined(typeof(ResultSpecificityCode), specificityCode))
            {
                throw new InvalidOperationException("The expected detail code was not valid.");
            }

            var originationSource = statusCodeParts.Length == 1
                ? DefaultSource
                : statusCodeParts[1];

            return new OutputDetails((FunctionResult)functionResult, (ResultSpecificityCode)specificityCode, originationSource);
        }

        #endregion

        #region Variables

        public const string DefaultSource = "None";

        public FunctionResult Result { get; }

        public ResultSpecificityCode SpecificityCode { get; }

        public string OriginationSource { get; }

        private readonly string _originationSuffix;

        #endregion

        #region Constructors

        public OutputDetails(FunctionResult functionResult, ResultSpecificityCode specificityCode, string originationSource = DefaultSource)
        {
            Result = functionResult;
            SpecificityCode = specificityCode;
            OriginationSource = originationSource;

            _originationSuffix = string.IsNullOrWhiteSpace(originationSource) || originationSource == DefaultSource
                ? string.Empty
                : $",{originationSource}";
        }

        #endregion
        
        #region Helpers

        public readonly bool IsSuccessful => Result is FunctionResult.Success
            || Result is FunctionResult.MultipleResults;

        public override readonly string ToString() => $"{(int)Result}.{(int)SpecificityCode}";

        public readonly string ToString(bool includeOrigination) => includeOrigination
            ? $"{(int)Result}.{(int)SpecificityCode}{_originationSuffix}"
            : ToString();

        #endregion
    }
}
