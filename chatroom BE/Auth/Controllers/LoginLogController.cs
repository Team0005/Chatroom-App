using System;
using System.Linq;
using System.Threading.Tasks;
using Auth.Dto.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistence.Interfaces;

namespace Auth.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginLogController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public LoginLogController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Authorize(Roles = "User")]
        [HttpGet("getAll")]
        public async Task<IActionResult> GetUserLoginLogs()
        {
            try
            {
                var currentUser = HttpContext.User;
                var userId = currentUser.Claims.FirstOrDefault(claim => claim.Type == "userId").Value;
                var loginLogs = await _unitOfWork.LoginLogs.FindAllByUserId((Guid.Parse(userId)));

                var model = loginLogs
                    .OrderByDescending(loginLog => loginLog.TimeStamp)
                    .Select(loginLog => new LoginLogDto
                    {
                        Id = loginLog.Id,
                        IpAddress = loginLog.IpAddress,
                        Succeeded = loginLog.Succeeded,
                        TimeStamp = loginLog.TimeStamp.ToString()
                    });
                
                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}