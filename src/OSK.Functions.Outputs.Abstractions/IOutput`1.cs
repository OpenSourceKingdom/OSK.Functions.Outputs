﻿using System;
using System.Collections.Generic;
using System.Text;

namespace OSK.Functions.Outputs.Abstractions
{
    public interface IOutput<TValue>: IOutput
    {
        TValue Value { get; }
    }
}
