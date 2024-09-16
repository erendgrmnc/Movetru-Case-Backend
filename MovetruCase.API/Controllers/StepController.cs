using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovetruCase.Business.Abstract;
using MovetruCase.Business.DTO;
using MovetruCase.Core.Helpers.Authentication;
using MovetruCaseEntities.Entities;

namespace MovetruCase.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StepController : ControllerBase
    {
        private readonly IStepService stepService;
        private readonly IAuthHelper authHelper;

        public StepController(IStepService stepService, IAuthHelper authHelper)
        {
            this.stepService = stepService;
            this.authHelper = authHelper;
        }


        [HttpGet("GetDailyStepData")]
        [Authorize]
        public async Task<ActionResult> GetUserDailyStepData()
        {
            string authorizationHeader = HttpContext.Request.Headers.Authorization;
            var userId = await authHelper.GetUserId(authorizationHeader);

            var result = await stepService.GetDailyStepLog(userId, DateTimeOffset.UtcNow);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("SendDailyStepData")]
        [Authorize]

        public async Task<ActionResult> SendDailyStepData(SendDailyStepDataRequest request)
        {
            string authorizationHeader = HttpContext.Request.Headers.Authorization;
            var userId = await authHelper.GetUserId(authorizationHeader);

            var result = await stepService.UpdateDailyStepLog(new DailyStepLog
            {
                UserId = userId,
                TotalSteps = request.TotalSteps,
                CreationDate = DateTimeOffset.UtcNow

            });

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("GetWeeklyStepData")]
        [Authorize]

        public async Task<ActionResult> GetWeeklyStepData()
        {
            string authorizationHeader = HttpContext.Request.Headers.Authorization;
            var userId = await authHelper.GetUserId(authorizationHeader);

            var result = await stepService.GetDailyStepLogsBetweenDates(userId, DateTimeOffset.UtcNow.AddDays(-7), DateTimeOffset.UtcNow
              );

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
