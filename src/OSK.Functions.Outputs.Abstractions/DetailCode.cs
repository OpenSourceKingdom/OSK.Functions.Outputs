namespace OSK.Functions.Outputs.Abstractions
{
    public enum DetailCode
    {
        None = 0,

        /// <summary>
        /// This is meant to signify an error further down stream that is being propogated back up.
        /// For example, if service A calls service B, which calls service C, as the response comes back to
        /// the previous callers, this detail code can be used to help differentiate failures from a call
        /// in a specific service from another to help determine if circuit breakers or other logic should be
        /// triggered
        /// </summary>
        DownStreamError = 1,
        NetworkCommunicationError = 2,

        /// <summary>
        /// The data that an output is associated to has failed due to some part of it being invalid
        /// </summary>
        InvalidData = 400,

        /// <summary>
        /// The data that an outpt is associated to has failed due to some part of it being duplicated
        /// </summary>
        DuplicateData = 420,

        Exception = 500
    }
}
