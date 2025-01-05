using System.Net;

namespace OSK.Functions.Outputs.Abstractions
{
    /// <summary>
    /// A specificity code represents specific information related to a function's result. This could be merely informational, with successful responses,
    /// or for error responses these can represent specific reasons for the functions error or other failure
    /// </summary>
    public enum OutputSpecificityCode
    {
        #region Special

        /// <summary>
        /// In the case where a consumer may send the code to a project without the latest updates and latest codes,
        /// this should represent to the application that their version of Outputs is out of date and that they need
        /// to be updated if they want to understand the meaning of the value being transmitted
        /// </summary>
        SpecificityNotRecognized = 1,

        #endregion

        #region Successful

        /// <summary>
        /// The function had a successful execution
        /// </summary>
        Success = 200,

        /// <summary>
        /// The function created a resource
        /// </summary>
        Created = 201,

        /// <summary>
        /// The function has completed initial processing of the call and has returned successfuly. Depending on the implementation of the function,
        /// this may imply a background task or job of some sort being ran asynchronously
        /// </summary>
        Accepted = 202,
        
        /// <summary>
        /// The function deleted a resource
        /// </summary>
        Deleted = 204,
        
        /// <summary>
        /// The function partially completed as expected. Specific errors and other information should be checked on the output's
        /// result value
        /// </summary>
        MultipleOutputs = 207,

        #endregion

        #region Data Errors

        /// <summary>
        /// The data that an output is associated to has failed due to some part of it being invalid
        /// </summary>
        InvalidParameter = 400,

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
        DataNotFound = 404,

        /// <summary>
        /// The data that an output is associated to has failed due to some part of it being duplicated
        /// </summary>
        DuplicateData = 409,

        /// <summary>
        /// The function received a data object that was larger than allowed
        /// </summary>
        DataTooLarge = 413,

        /// <summary>
        /// A URI passed to the function was longer than allowed
        /// </summary>
        UriTooLong = 414,

        /// <summary>
        /// The data media type used is not supported by the function
        /// </summary>
        MediaTypeNotSupported = 415,

        /// <summary>
        /// Some part of the data was outside the expected range of valid input
        /// </summary>
        InvalidParameterInputRange = 416,

        /// <summary>
        /// The function encountered an issue with a locking mechanism that prevented its operation
        /// </summary>
        Locked = 423,

        /// <summary>
        /// Some part of the function has been rate limited
        /// </summary>
        RateLimited = 429,

        #endregion

        #region Operation Errors

        /// <summary>
        /// The function encountered an unknown error when attempting to perform an action
        /// </summary>
        UnknownError = 500,

        /// <summary>
        /// A function being utilized was not implemented as expected
        /// </summary>
        NotImplemented = 501,

        /// <summary>
        /// The function encountered a <see cref="HttpStatusCode.BadGateway"/> when attempting to perform an action
        /// </summary>
        BadGateway = 502,

        /// <summary>
        /// The service encountered a <see cref="HttpStatusCode.ServiceUnavailable"/> when attempting to perform an action
        /// </summary>
        ServiceUnavailable = 503,

        /// <summary>
        /// Some process in the function timed out and didn't complete in the expected time period
        /// </summary>
        Timeout = 504,

        /// <summary>
        /// The function attempted an operation on the machine that lacked the resources to store the data
        /// </summary>
        InsufficientStorage = 507,

        /// <summary>
        /// The function determined it was in an endless loop and could not complete as expected
        /// </summary>
        EndlessLoop = 508,

        /// <summary>
        /// This is meant to signify an error further down stream that is being propogated back up.
        /// For example, if service A calls service B, which calls service C, as the response comes back to
        /// the previous callers, this detail code can be used to help differentiate failures from a call
        /// in a specific service from another to help determine if circuit breakers or other logic should be
        /// triggered. In the example of service A calling service B, service B could return this value to indicate
        /// that a service it dependend on had issues that prevented completion, but that service B itself was not 
        /// having issues
        /// </summary>
        ThirdPartyServiceFailure = 509,

        #endregion
    }
}
