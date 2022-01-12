using System;
using System.Collections.Generic;
using System.Text;

namespace ContactGuide.Shared.Utilities.Results
{
    public interface IDataResult<out T> : IResult
    {
        T Data { get; }
    }
}
