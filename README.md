# OSK.Functions.Outputs
The library is meant to faciliate easy to use output responses from various APIs, functions, and other types of calls that can occur in an application. These
outputs can provide quick and meaningful information to a caller beyond a simple exception or other error data. Response information can include specific error
messages, exceptions or non-error information such as specificty codes, run timer metrics, response dates, etc.

The main data points on an output response are:
* `OuputStatusCode`: Provides quick reference to the response `OutputSpecificityCode`. These specificity code shares a lot of status codes with HttpStatusCode, but does differ in some parts that are more nuanced for function responses
* `ErrorInformation`: Provides error related information, if the specificity code for a response is an error code. This can either be an error message or an exception


An output's code can be easily represented by the following template
```
{specificity code}, {optional origination source}
```
The string form of the codes should hopefully make checking/debugging error information easier by allowing more complex error scenarios to be viewable in a concise form.
For example, a function that has successfully completed a task and has queued an asynchronous job can be shown as
`202, OSK.Job.Service`

For future codes that may be added, the following represents the different groupings of codes:
* The range of `200-299` represents `success`  codes
* The range of `400-499` represents `error` codes
* The range of `500-599` represents `fault` codes

# Abstractions
The abstraction layer should allow libraries that only need access to the interfaces to avoid adding a dependency on the core output logic, thus reducing
unnecessary dependency requirements. An application should add the core logic and any extra libraries being added can simply add the dependency for the abstractions
project.

# Mocks
The mocks project provides a quick set of factory objects that should help with streamlining unit tests with the library. They are set up to avoid running core output logic and
to skip any ILogger dependency requirement. By using the needed `MockOutputFactory` or `MockOutputFactory<T>` objects, users should be able to skip needing to mock the necessary ports for DI

# Usage: Consumers
 The central focal point to this library is the `IOutputFactory` and `IOutputFactory<T>` objects. By adding a dependency to the `Outputs.Logging` or base `Outputs`,
 an application will gain access to the functionality through dependency injection. `IOutputFactory` is a basic implementation that avoids a dependency on 
 Microsoft's Logging mechanism, while `IOutputFactory<T>` will use an ILogger to record error responses. Some useful shortcuts to creating outputs can be found in the
 `OutputFactoryExtensions`. Callers can add the dependency by using `AddFunctionOutputs` or `AddLoggingFunctionOutputs` functions on a service collection.
 
Notes:
 * The internal logic will prevent creating an error output that has no error information attached (i.e. exception data, error strings, etc.)
 * `OriginationSource` on the IOutput status code is meant to convey the base application where the error orignated from. This does not need to be used, but can help provide extra debug information should issues/errors occur in an application's lifetime. Being a string can help to ensure broad usage of identification to most projects in the wild.
 * `IPaginatedOutput` can help to make list style calls return extra data relating to total items in the data set, skip/take, etc. and can facilitate pagination for front-end usage. This can be generated using the `CreatePage` from the `IOutputFactory`
 * `ResultSpecificity` codes do have some similar codes/values to `HttpStatusCode`s, but this is only coincidential and should not be expected to be upheld in all cases.

# Contributions and Issues
Any and all contributions are appreciated! Please.3 be sure to follow the branch naming convention OSK-{issue number}-{deliminated}-{branch}-{name} as current workflows rely on it for automatic issue closure. Please submit issues for discussion and tracking using the github issue tracker. Feel free to create issues to discuss adding more nuanced specificity or other result codes.