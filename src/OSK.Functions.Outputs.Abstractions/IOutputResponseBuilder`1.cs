using System;
using OSK.Hexagonal.MetaData;

namespace OSK.Functions.Outputs.Abstractions
{
    [HexagonalIntegration(HexagonalIntegrationType.LibraryProvided)]
    public interface IOutputResponseBuilder<TValue>
    {
        IOutputResponseBuilder<TValue> WithRunTimeMetric();

        IOutputResponseBuilder<TValue> WithTimeStamp();

        IOutputResponseBuilder<TValue> WithOrigination(string originationSource);

        IOutputResponseBuilder<TValue> AddException(Exception exception);

        IOutputResponseBuilder<TValue> AddError(string error, OutputSpecificityCode specificityCode);

        IOutputResponseBuilder<TValue> AddSuccess(TValue value, OutputSpecificityCode specificityCode = OutputSpecificityCode.Success);

        IOutputResponse<TValue> BuildResponse();
    }
}
