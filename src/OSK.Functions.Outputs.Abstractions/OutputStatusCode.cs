using System;

namespace OSK.Functions.Outputs.Abstractions
{
    public struct OutputStatusCode
    {
        #region Static

        public static readonly OutputStatusCode Success = new(OutputSpecificityCode.Success, DefaultSource);

        public static OutputStatusCode Parse(string statusCode)
        {
            var statusCodeParts = statusCode.Split(',');
            if (statusCodeParts.Length < 1 || statusCodeParts.Length > 2)
            {
                throw new InvalidOperationException("The expected status code parts were not found.");
            }

            if (!int.TryParse(statusCodeParts[0], out var specificityCode)
                || !Enum.IsDefined(typeof(OutputSpecificityCode), specificityCode))
            {
                specificityCode = (int)OutputSpecificityCode.SpecificityNotRecognized;
            }

            var originationSource = statusCodeParts.Length == 1
                ? DefaultSource
                : statusCodeParts[1];

            return new OutputStatusCode((OutputSpecificityCode)specificityCode, originationSource);
        }

        #endregion

        #region Variables

        public const string DefaultSource = "None";

        public OutputSpecificityCode SpecificityCode { get; }

        public string OriginationSource { get; }

        private readonly string _originationSuffix;

        #endregion

        #region Constructors

        public OutputStatusCode(OutputSpecificityCode specificityCode,
            string originationSource = DefaultSource)
        {
            SpecificityCode = specificityCode;
            OriginationSource = originationSource;

            _originationSuffix = string.IsNullOrWhiteSpace(originationSource) || string.Equals(originationSource, DefaultSource, StringComparison.OrdinalIgnoreCase)
                ? string.Empty
                : $",{OriginationSource}";
        }

        #endregion

        #region Helpers

        public readonly bool IsSuccessful => SpecificityCode >= OutputSpecificityCode.Success 
            && SpecificityCode < OutputSpecificityCode.InvalidParameter;

        public override readonly string ToString() => OriginationSource is null
            ? null
            : $"{(int)SpecificityCode}";

        public readonly string ToString(bool includeOrigination) => includeOrigination
            ? $"{(int)SpecificityCode}{_originationSuffix}"
            : ToString();

        #endregion
    }
}
