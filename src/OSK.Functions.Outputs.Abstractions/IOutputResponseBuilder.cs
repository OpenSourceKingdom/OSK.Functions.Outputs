using System;
using System.Collections.Generic;
using System.Text;

namespace OSK.Functions.Outputs.Abstractions
{
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
