using System;
using System.Collections.Generic;
using System.Text;

namespace OSK.Functions.Outputs.Abstractions
{
    public class OutputInformation
    {
        #region Variables

        public string OriginationSource { get; }

        public FunctionResult FunctionResult { get; }

        public ResultSpecificityCode ResultSpecificityCode { get; }

        public ErrorInformation? ErrorInformation { get; }

        #endregion

        #region Constructors

        public OutputInformation(FunctionResult functionResult, ResultSpecificityCode resultSpecificityCode,
            ErrorInformation? errorInformation, string originationSource)
        {
            FunctionResult = functionResult;
            ResultSpecificityCode = resultSpecificityCode;
            ErrorInformation = errorInformation;
            OriginationSource = originationSource;
        }

        #endregion
    }
}
