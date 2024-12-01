# OSK.Functions.Outputs
The library is meant to faciliate easy to use output responses from various APIs, functions, and other types of calls that can occur in an application. These
outputs can provide quick and meaningful information to a caller beyond a simple exception or other error data. Error information can include specific error
messages, exceptions, http status codes, and more. 

# Abstractions
The abstraction layer should allow libraries that only need access to the interfaces to avoid adding a dependency on the core output logic, thus reducing
unnecessary dependency requirements. An application should add the core logic and any extra libraries being added can simply add the dependency for the abstractions
project.

# Usage: Consumers
 The central focal point to this library is the `IOutputFactory` and `IOutputFactory<T>` objects. By adding a dependency to the `Outputs.Logging` or base `Outputs`,
 an application will gain access to the functionality through dependency injection. `IOutputFactory` is a basic implementation that avoids a dependency on 
 Microsoft's Logging mechanism, while `IOutputFactory<T>` will use an ILogger to record error responses. Some useful shortcuts to creating outputs can be found in the
 `OutputFactoryExtensions`. 
 
Notes:
 ** The internal logic will prevent creating an error output that has error information attached (i.e. exception data, error strings, etc.)
 ** `OriginationSource` on the IOutput status code is meant to convey the base application where the error orignated from. This does not need to be used, but can help provide extra debug information should issues/errors occur in an application's lifetime. Being a string can help to ensure broad usage of identification to most projects in the wild.
 ** `DetailCode` is meant to help convey a bit more information to callers at a glance. For example, `DetailCode.DownStreamError` would represent an error that originated outside of the current application (i.e. service A calls service B and B returns an error to A, A can return to the call of service A or handle this specific use case for retrying on transient errors)
 ** `PaginatedOutput` can help to make list style calls return extra data relating to total items in the data set, skip/take, etc. and can facilitate pagination for front-end usage
 ** `ErrorInformation` will only be available on an `IOutput` that was unsuccessful. 

# Mocks
The mocks project is meant to be a simple implementation to the library that simply returns basic responses for test purposes. It may be similar to the base Output project, but they can differ as iterations are made. For main application use outside of test purposes, users should prefer the base Output or Logging projects.

# Contributions and Issues
Any and all contributions are appreciated! Please be sure to follow the branch naming convention OSK-{issue number}-{deliminated}-{branch}-{name} as current workflows rely on it for automatic issue closure. Please submit issues for discussion and tracking using the github issue tracker.