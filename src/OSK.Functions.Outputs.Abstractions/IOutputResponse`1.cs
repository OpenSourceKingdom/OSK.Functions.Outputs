using System;
using System.Collections.Generic;
using System.Text;

namespace OSK.Functions.Outputs.Abstractions
{
    public interface IOutputResponse<TValue>: IOutputResponse
    {
        new IOutput<TValue>[] Outputs { get; }
    }
}
