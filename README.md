# OSK.Functions.Outputs
The library is meant to faciliate easy to use output responses from various APIs, functions, and other types of calls that can occur in an application. These
outputs can provide quick and meaningful information to a caller beyond a simple exception or other error data. Error information can include specific error
messages, exceptions, http status codes, and more. 

The main data points on an output are:
* `OutputDetails`
* `ErrorInformation`

`OutputDetails` describe non error information about the result of the function call and there is a `ResultSpecifictyCode` that is available that provides a mechanism
for returning more meaningful specific details to a functions response beyond simple success and failure.

`ErrorInformation` will only be avialable on outputs that are considered to be in a non-successful state and can provide either exception or other error message data if 
a library provides it.

An output's code can be easily represented by the following template
```
{function result}.{specificity code}, {optional origination source}
```
The string form of the codes should hopefully make checking/debugging error information easier by allowing more complex error scenarios to be viewable in a concise form.
For example, a function that has successfully completed a task and has queued an asynchronous job can be shown as
`20.22`

A `20` for the function result represents a successful function call completion, with a specificity of `22` representing an accepted request to a function signifying that a job has been queued for
later processing.

For future codes that may be added, the following represents the different groupings of codes:
* Ranges of [`20-29`, `200-299`, ... ] represent `successful function result` codes as well as `informational specificity` related codes
* Ranges of [`30-39`, `300-399`, ... ] represent `network specificity` related codes
* Ranges of [`40-49`, `400-499`, ... ] represent`error function result` codes as well as `validation specificity` related codes 
* Ranges of [`50-59`, `500-599`, ... ] represent `fault function result` codes as well as `operation specificity` related codes
  * For example, an a specific issue with the system encountering deadlocks could be represented via this range

# Abstractions
The abstraction layer should allow libraries that only need access to the interfaces to avoid adding a dependency on the core output logic, thus reducing
unnecessary dependency requirements. An application should add the core logic and any extra libraries being added can simply add the dependency for the abstractions
project.

# Usage: Consumers
 The central focal point to this library is the `IOutputFactory` and `IOutputFactory<T>` objects. By adding a dependency to the `Outputs.Logging` or base `Outputs`,
 an application will gain access to the functionality through dependency injection. `IOutputFactory` is a basic implementation that avoids a dependency on 
 Microsoft's Logging mechanism, while `IOutputFactory<T>` will use an ILogger to record error responses. Some useful shortcuts to creating outputs can be found in the
 `OutputFactoryExtensions`. Callers can add the dependency by using `AddFunctionOutputs` or `AddLoggingFunctionOutputs` functions on a service collection.
 
Notes:
 * The internal logic will prevent creating an error output that has no error information attached (i.e. exception data, error strings, etc.)
 * `OriginationSource` on the IOutput status code is meant to convey the base application where the error orignated from. This does not need to be used, but can help provide extra debug information should issues/errors occur in an application's lifetime. Being a string can help to ensure broad usage of identification to most projects in the wild.
 * `PaginatedOutput` can help to make list style calls return extra data relating to total items in the data set, skip/take, etc. and can facilitate pagination for front-end usage
 * `ResultSpecificity` codes do have some similar codes/values to `HttpStatusCode`s, but this is only coincidential and should not be expected to be upheld in all cases.

# Contributions and Issues
Any and all contributions are appreciated! Please be sure to follow the branch naming convention OSK-{issue number}-{deliminated}-{branch}-{name} as current workflows rely on it for automatic issue closure. Please submit issues for discussion and tracking using the github issue tracker. Feel free to create issues to discuss adding more nuanced specificity or other result codes.