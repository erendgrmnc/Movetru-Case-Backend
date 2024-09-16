using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovetruCase.Core.Utilities.Results.BaseResult
{
    public abstract class Result : IResult
    {
        public Result(bool success, string Message)
        {
            this.Success = success;
            this.Message = Message;
        }

        public bool Success { get; }
        public string Message { get; set; }
    }
}
