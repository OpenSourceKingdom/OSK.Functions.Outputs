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

        #endregion

        #region Constructors

        public OutputStatusCode(HttpStatusCode statusCode, DetailCode detailCode, string originationSource = DefaultSource)
        {
            StatusCode = statusCode;
            DetailCode = detailCode;
            OriginationSource = originationSource;
        }

        #endregion

        #region Helpers

        public bool IsSuccessCode => StatusCode >= HttpStatusCode.OK && StatusCode < HttpStatusCode.MultipleChoices;

        public override string ToString() => $"{(int)StatusCode}.{DetailCode}";

        #endregion
    }
}
