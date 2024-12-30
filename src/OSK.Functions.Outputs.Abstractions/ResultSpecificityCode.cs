using System.Net;

namespace OSK.Functions.Outputs.Abstractions
{
    /// <summary>
    /// A specificity code represents specific information related to a function's <see cref="FunctionResult"/>. This could be merely informational, with successful responses,
    /// or for error responses these can represent specific reasons for the functions error or other failure
    /// </summary>
    public enum ResultSpecificityCode
    {
        /// <summary>
        /// There is no specific information needed for the output. This is the default value for success outputs, 
        /// if nothing is set
        /// </summary>
        None = 0,

        #region Informational

        /// <summary>
        /// The function created a resource
        /// </summary>
        Created = 200,

        /// <summary>
        /// The function updated a resource
        /// </summary>
        Updated = 201,

        /// <summary>
        /// The function has completed initial processing of the call and has returned successfuly. Depending on the implementation of the function,
        /// this may imply a background task or job of some sort being ran asynchronously
        /// </summary>
        Accepted = 202,
        
        /// <summary>
        /// The function deleted a resource
        /// </summary>
        Deleted = 203,

        #endregion

        #region Validation Errors

        /// <summary>
        /// The data that an output is associated to has failed due to some part of it being invalid
        /// </summary>
        InvalidData = 400,

        /// <summary>
        /// The function lacked an identification required to run the operation
        /// </summary>
        NotAuthenticated = 401,

        /// <summary>
        /// THe function was authenticated to an identity, but the identity did not have the required permissions to run the operation
        /// </summary>
        InsufficientPermissions = 403,

        /// <summary>
        /// A resource being referenced by the function could not be found
        /// </summary>
        ResourceNotFound = 404,
        
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
        /// triggered. In the example of service A calling service B, service B could return this value to indicate
        /// that a service it dependend on had issues that prevented completion, but that service B itself was not 
        /// having issues
        /// </summary>
        DownStreamError = 500,

        /// <summary>
        /// The service encountered a <see cref="HttpStatusCode.ServiceUnavailable"/> when attempting to perform an action
        /// </summary>
        ServiceUnavailable = 501,

        /// <summary>
        /// The function encountered a <see cref="HttpStatusCode.BadGateway"/> when attempting to perform an action
        /// </summary>
        BadGateway = 502,
        
        /// <summary>
        /// The function encountered a <see cref="HttpStatusCode.InternalServerError"/> when attempting to perform an action
        /// </summary>
        ServerError = 503,

        #endregion

        #region Misc

        Exception = 999

        #endregion
    }
}
