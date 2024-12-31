namespace OSK.Functions.Outputs.Abstractions
{
    public class OutputInformation(FunctionResult functionResult, ResultSpecificityCode resultSpecificityCode,
            ErrorInformation? errorInformation, string originationSource)
    {
        #region Variables

        public string OriginationSource => originationSource;

        public FunctionResult FunctionResult => functionResult;

        public ResultSpecificityCode ResultSpecificityCode => resultSpecificityCode;

        public ErrorInformation? ErrorInformation => errorInformation;

        #endregion
    }
}
