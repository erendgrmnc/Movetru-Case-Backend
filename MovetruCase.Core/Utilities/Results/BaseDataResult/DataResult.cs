using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovetruCase.Core.Utilities.Results.BaseResult;

namespace MovetruCase.Core.Utilities.Results.DataResult
{
    public class DataResult<T> : Result, IDataResult<T>
    {
        protected DataResult(T data, bool success, string message) : base(success, message)
        {
            Data = data;
        }


        public T Data { get; }
    }
}
