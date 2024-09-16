using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovetruCase.Core.Helpers.Authentication
{
    public class JwtToken
    {
        public string Token { get; set; }
        public DateTime ExpirationDateTime { get; set; }
    }
}
