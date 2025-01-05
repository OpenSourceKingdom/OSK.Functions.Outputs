using System;
using System.Collections.Generic;
using System.Text;

namespace OSK.Functions.Outputs.Abstractions
{
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
