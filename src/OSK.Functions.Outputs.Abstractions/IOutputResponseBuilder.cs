using System;
using OSK.Hexagonal.MetaData;

namespace OSK.Functions.Outputs.Abstractions
{
    [HexagonalIntegration(HexagonalIntegrationType.LibraryProvided)]
    public interface IOutputResponseBuilder
    {
        IOutputResponseBuilder WithRunTimeMetric();

        IOutputResponseBuilder WithTimeStamp();

        IOutputResponseBuilder WithOrigination(string originationSource);

        IOutputResponseBuilder AddException(Exception exception);

        IOutputResponseBuilder AddError(string error, OutputSpecificityCode specificityCode);

        IOutputResponseBuilder AddSuccess(OutputSpecificityCode specificityCode = OutputSpecificityCode.Success);

        IOutputResponse BuildResponse();
    }
}
