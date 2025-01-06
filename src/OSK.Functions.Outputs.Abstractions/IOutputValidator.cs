using System;
using System.Collections.Generic;
using System.Text;

namespace OSK.Functions.Outputs.Abstractions
{
    public interface IOutputValidator
    {
        void Validate(IOutput output);

        void Validate<TValue>(IOutput<TValue> output);
    }
}
