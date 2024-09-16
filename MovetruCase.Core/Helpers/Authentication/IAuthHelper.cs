using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovetruCase.Core.Helpers.Authentication
{
    public interface IAuthHelper
    {
        Task<string> GetUserId(string authorizationHeader);
    }
}
