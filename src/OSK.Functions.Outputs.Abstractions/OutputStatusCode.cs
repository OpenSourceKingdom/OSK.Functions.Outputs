using System;
using System.Net;

namespace OSK.Functions.Outputs.Abstractions
{
    public struct OutputStatusCode
    {
        #region Static

        public static readonly OutputStatusCode Success = new OutputStatusCode(HttpStatusCode.OK, DetailCode.None, DefaultSource);

        #endregion

        #region Variables

        public const string DefaultSource = "None";

        public HttpStatusCode StatusCode { get; }

        public DetailCode DetailCode { get; }

        public string OriginationSource { get; }

        private readonly string _codeSuffix; 

        #endregion

        #region Constructors

        public OutputStatusCode(HttpStatusCode statusCode, DetailCode detailCode, 
            string originationSource = DefaultSource)
        {
            StatusCode = statusCode;
            DetailCode = detailCode;
            OriginationSource = originationSource;
            _codeSuffix = string.IsNullOrWhiteSpace(originationSource) || originationSource == DefaultSource
                ? string.Empty
                : $",{originationSource}";
        }

        public static OutputStatusCode Parse(string statusCode)
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

            if (!int.TryParse(statusParts[0], out var httpStatusCode) 
                && !Enum.IsDefined(typeof(HttpStatusCode), httpStatusCode))
            {
                throw new InvalidOperationException("The expected http status code was not valid.");
            }
            if (!int.TryParse(statusParts[1], out var detailCode) 
                && !Enum.IsDefined(typeof(DetailCode), detailCode))
            {
                throw new InvalidOperationException("The expected detail code was not valid.");
            }

            var originationSource = statusCodeParts.Length == 1
                ? DefaultSource
                : statusCodeParts[1];

            return new OutputStatusCode((HttpStatusCode)httpStatusCode, (DetailCode)detailCode, originationSource);
        }

        #endregion

        #region Helpers

        public bool IsSuccessCode => StatusCode >= HttpStatusCode.OK && StatusCode < HttpStatusCode.MultipleChoices;

        public override string ToString() => $"{(int)StatusCode}.{(int)DetailCode}";

        public string ToString(bool includeOrigination) => includeOrigination
            ? $"{(int)StatusCode}.{(int)DetailCode},{OriginationSource}"
            : ToString();

        #endregion
    }
}
