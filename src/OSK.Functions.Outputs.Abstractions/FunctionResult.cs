namespace OSK.Functions.Outputs.Abstractions
{
    /// <summary>
    /// Represents result of a function's operation.
    /// </summary>
    public enum FunctionResult
    {
        /// <summary>
        /// The function completed successfully, with no issues encountered.
        /// 
        /// This should represent that all calls and operations that the function dependend on also completed successfully
        /// </summary>
        Success = 200,

        /// <summary>
        /// The function partially completed as expected. Specific errors and other information should be checked on the output's
        /// result value
        /// </summary>
        MultipleResults = 299,

        /// <summary>
        /// The function encountered an issue during its operation that prevented successful completion. This should indicate that 
        /// something directly related to the function, i.e. validation, parameters, etc., were not properly configured for the function
        /// to run.
        /// </summary>
        Error = 400,

        /// <summary>
        /// The function encountered an issue after it had successfully started its operation. This should indicate that while the function
        /// itself was able to perform its work, some other operation it dependend on failed, i.e. a service call to an API or similar.
        /// </summary>
        Failed = 500
    }
}
