using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovetruCase.Core.Helpers.Authentication;
using Newtonsoft.Json.Linq;

namespace MovetruCase.Core.Helpers
{
    public class FirebaseAuthenticationHelper : IAuthHelper
    {
        FirebaseAuth firebaseAuth;
        public FirebaseAuthenticationHelper()
        {
            firebaseAuth = FirebaseAuth.DefaultInstance;
        }

        public async Task<string> GetUserId(string authorizationHeader)
        {
            var accessToken = authorizationHeader.Substring("Bearer ".Length);

            try
            {
                FirebaseToken decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(accessToken);

                return decodedToken.Uid;
            }
            catch (FirebaseAuthException ex)
            {
                return string.Empty;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
    }
}
