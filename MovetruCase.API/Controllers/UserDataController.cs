using FirebaseAdmin.Messaging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovetruCase.Business.Abstract;
using MovetruCase.Business.DTO;
using MovetruCase.Core.Helpers.Authentication;
using MovetruCase.Entities.Entities;

namespace MovetruCase.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserDataController : ControllerBase
    {
        private readonly IUserDataService userDataService;
        private readonly IAuthHelper authHelper;

        public UserDataController(IUserDataService userDataService, IAuthHelper authHelper)
        {
            this.userDataService = userDataService;
            this.authHelper = authHelper;
        }

        [HttpPost("Login")]
        [Authorize]
        public async Task<ActionResult> Login()
        {
            if (!HttpContext.Request.Headers.ContainsKey("Authorization"))
            {
                return BadRequest("Authorization Error");
            }

            string bearerToken = HttpContext.Request.Headers["Authorization"];

            if (String.IsNullOrEmpty(bearerToken) || !bearerToken.StartsWith("Bearer"))
            {
                return BadRequest("Authorization Error");
            }

            string userId = await authHelper.GetUserId(bearerToken);
            if (String.IsNullOrEmpty(userId))
            {
                return BadRequest("Authorization Error");
            }

            var result = await userDataService.GetUserData(userId);
            if (result.Success)
                return Ok(result);
            return NotFound(result);

        }

        [HttpPost("AddUserData")]
        [Authorize]
        public async Task<ActionResult> AddUserData(AddUserDataRequest request)
        {
            if (!HttpContext.Request.Headers.ContainsKey("Authorization"))
            {
                return BadRequest("Authorization Error");
            }

            string bearerToken = HttpContext.Request.Headers["Authorization"];

            if (String.IsNullOrEmpty(bearerToken) || !bearerToken.StartsWith("Bearer"))
            {
                return BadRequest("Authorization Error");
            }

            string userId = await authHelper.GetUserId(bearerToken);
            if (String.IsNullOrEmpty(userId))
            {
                return BadRequest("Authorization Error");
            }

            var result = await userDataService.GetUserData(userId);
            if (result.Success && !result.Data.IsUserNewlyRegistered)
                return BadRequest("User Data Already Added");

            var addResult = await userDataService.UpdateUserData(new UserData()
            {
                CreationDate = DateTimeOffset.UtcNow,
                Age = request.Age,
                HeightAsCentimeter = request.HeightAsCentimeter,
                Name = request.Name,
                WeightAsKilogram = request.WeightAsKilogram,
                IsUserNewlyRegistered = false,
                UserID = userId,
                UpdateDate = DateTimeOffset.UtcNow
            });

            if (addResult.Success)
            {
                return Ok(addResult);
            }

            return BadRequest(addResult);

        }
    }
}
