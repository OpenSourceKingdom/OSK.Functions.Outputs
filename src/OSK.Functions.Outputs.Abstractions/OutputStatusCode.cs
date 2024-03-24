using System.Net;

namespace OSK.Functions.Outputs.Abstractions
{
    public struct OutputStatusCode
    {
        #region Static

        public static readonly OutputStatusCode Success = new OutputStatusCode(HttpStatusCode.OK, DetailCode.None, 0);

        #endregion

        #region Variables

        public HttpStatusCode StatusCode { get; }

        public DetailCode DetailCode { get; }

        public int OriginationSourceId { get; }

        #endregion

        #region Constructors

        public OutputStatusCode(HttpStatusCode statusCode, DetailCode detailCode, int originationSourceId)
        {
            StatusCode = statusCode;
            DetailCode = detailCode;
            OriginationSourceId = originationSourceId;
        }

        #endregion

        #region Helpers

        public bool IsSuccessCode => StatusCode >= HttpStatusCode.OK && StatusCode < HttpStatusCode.MultipleChoices;

        public override string ToString() => $"{(int)StatusCode}.{DetailCode}.{OriginationSourceId}";

        #endregion
    }
}
