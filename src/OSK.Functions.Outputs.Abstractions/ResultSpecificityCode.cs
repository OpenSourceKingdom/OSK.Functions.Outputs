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

        #region Informational [Codes between the 20-29, 200-299, etc.]

        /// <summary>
        /// The function created a resource
        /// </summary>
        Created = 20,

        /// <summary>
        /// The function updated a resource
        /// </summary>
        Updated = 21,

        /// <summary>
        /// The function has completed initial processing of the call and has returned successfuly. Depending on the implementation of the function,
        /// this may imply a background task or job of some sort being ran asynchronously
        /// </summary>
        Accepted = 22,
        
        /// <summary>
        /// The function deleted a resource
        /// </summary>
        Deleted = 23,

        /// <summary>
        /// The function partially completed as expected. Specific errors and other information should be checked on the output's
        /// result value
        /// </summary>
        MultipleResults = 29,

        #endregion

        #region Network Communication Errors [Codes between 30-39, 300-399, etc.]

        /// <summary>
        /// This is meant to signify an error further down stream that is being propogated back up.
        /// For example, if service A calls service B, which calls service C, as the response comes back to
        /// the previous callers, this detail code can be used to help differentiate failures from a call
        /// in a specific service from another to help determine if circuit breakers or other logic should be
        /// triggered. In the example of service A calling service B, service B could return this value to indicate
        /// that a service it dependend on had issues that prevented completion, but that service B itself was not 
        /// having issues
        /// </summary>
        DownStreamError = 30,

        /// <summary>
        /// The service encountered a <see cref="HttpStatusCode.ServiceUnavailable"/> when attempting to perform an action
        /// </summary>
        ServiceUnavailable = 31,

        /// <summary>
        /// The function encountered a <see cref="HttpStatusCode.BadGateway"/> when attempting to perform an action
        /// </summary>
        BadGateway = 32,

        /// <summary>
        /// The function encountered a <see cref="HttpStatusCode.InternalServerError"/> when attempting to perform an action
        /// </summary>
        ServerError = 33,

        #endregion

        #region Validation Errors [Codes between 40-49, 400-499, etc.]

        /// <summary>
        /// The data that an output is associated to has failed due to some part of it being invalid
        /// </summary>
        InvalidData = 40,

        /// <summary>
        /// The function lacked an identification required to run the operation
        /// </summary>
        NotAuthenticated = 41,

        /// <summary>
        /// THe function was authenticated to an identity, but the identity did not have the required permissions to run the operation
        /// </summary>
        InsufficientPermissions = 43,

        /// <summary>
        /// A resource being referenced by the function could not be found
        /// </summary>
        ResourceNotFound = 44,
        
        /// <summary>
        /// Some part of the data was outside the expected range of valid input
        /// </summary>
        InvalidInputRange = 45,

        /// <summary>
        /// The data that an output is associated to has failed due to some part of it being duplicated
        /// </summary>
        DuplicateData = 49,

        #endregion

        #region Operation Errors [Codes between 50-59, 500-599, etc.]

        /// <summary>
        /// The function encountered an issue with a locking mechanism that prevented its operation
        /// </summary>
        Locked = 51,

        /// <summary>
        /// An expected method was not implemented for the function to use
        /// </summary>
        MethodNotImplemented = 52,

        /// <summary>
        /// A catch all exception that signifies some unknown exception was encountered
        /// </summary>
        UnspecifiedException

        #endregion
    }
}
