using System;
using System.Collections.Generic;
using System.Text;

namespace OSK.Functions.Outputs.Abstractions
{
    public struct OutputDetails(double? runTimeInMilliseconds, DateTime? completionTime)
    {
        #region Variables

        public readonly double? RunTimeInMilliseconds => runTimeInMilliseconds;

        public readonly DateTime? CompletionTime => completionTime;
        
        #endregion
    }
}
