using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovetruCase.Core.Utilities.Results.BaseResult;

namespace MovetruCase.Core.Utilities.Results.DataResult
{
    public interface IDataResult<out T> : IResult
    {
        T Data { get; }
    }
}
