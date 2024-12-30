namespace OSK.Functions.Outputs.Abstractions
{
    public enum ResultSpecificityCode
    {
        None = 0,

        #region Informational

        Created = 200,
        Updated = 201,
        Accepted = 202,
        Deleted = 203,

        #endregion

        #region Validation Errors

        /// <summary>
        /// The data that an output is associated to has failed due to some part of it being invalid
        /// </summary>
        InvalidData = 400,
        NotAuthenticated = 401,
        InsufficientPermissions = 403,
        DataNotFound = 404,
        /// <summary>
        /// The data that an output is associated to has failed due to some part of it being duplicated
        /// </summary>
        DuplicateData = 409,

        #endregion

        #region Network Communication Errors

        /// <summary>
        /// This is meant to signify an error further down stream that is being propogated back up.
        /// For example, if service A calls service B, which calls service C, as the response comes back to
        /// the previous callers, this detail code can be used to help differentiate failures from a call
        /// in a specific service from another to help determine if circuit breakers or other logic should be
        /// triggered
        /// </summary>
        DownStreamError = 500,
        NetworkCommunicationError = 501,
        BadGateway = 502,
        ServerError = 503,

        #endregion

        Exception = 999
    }
}
