﻿using MovetruCase.Core.Utilities.Results.DataResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovetruCase.Core.Utilities.Results.SucessDataResult
{
    public class SuccessDataResult<T> : DataResult<T>
    {
        public SuccessDataResult(T data, bool success, string message) : base(data, success, message)
        {
        }
    }
}
