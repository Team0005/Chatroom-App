using System;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Auth.Jwt;
using Auth.Dto.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistence.Interfaces;

namespace Auth.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public AccountController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            try
            {
                var user = await _unitOfWork.Users.FindByUsername(model.Username);
                if (user == null)
                    throw new Exception("User doesn't exist");

                var loginLog = new LoginLog(user.Id, model.IpAddress, user.Password == model.Password);
                await _unitOfWork.LoginLogs.Add(loginLog);
                await _unitOfWork.Save();
                
                if (user.Password != model.Password)
                    throw new Exception("Username or Password doesn't match");
                    
                var token = JwtHelper.GetJwtToken(user);
                var response = new LoginResponseDto()
                {
                    Jwt = token,
                    UserRole = user.UserRole.ToString(),
                    Username = user.Username,
                    UserId = user.Id,
                    ImageUrl = user.ImageUrl
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [Authorize(Roles = "Administrator")]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.Username))
                    throw new Exception("Username should not be empty");
                if (string.IsNullOrWhiteSpace(model.Password))
                    throw new Exception("Password should not be empty");
                if (string.IsNullOrWhiteSpace(model.ImageUrl))
                    throw new Exception("Image Url should not be empty");
                if (string.IsNullOrWhiteSpace(model.Nationality))
                    throw new Exception("Nationality should not be empty");
                if (string.IsNullOrWhiteSpace(model.Phone))
                    throw new Exception("Phone should not be empty");
                
                var exitingUser = await _unitOfWork.Users.FindByUsername(model.Username);
                if (exitingUser != null)
                    throw new Exception("User with username already exits, use another username");

                var user = new User(model.Username, model.Password, model.UserRole, model.ImageUrl);
                var userInformation = new UserInformation(model.Nationality, model.Phone, model.BirthDate, user.Id);
                
                await _unitOfWork.Users.Add(user);
                await _unitOfWork.UserInformations.Add(userInformation);
                await _unitOfWork.Save();
                
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [Authorize(Roles = "User")]
        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _unitOfWork.Users.GetAll();
                 
                var userModels = users.Select(u => new UserDto()
                {
                    UserId = u.Id,
                    Username = u.Username,
                    UserRole = u.UserRole,
                    ImageUrl = u.ImageUrl,
                    UserRoleString = u.UserRole.ToString(),
                    Phone = u.UserInformation.Phone,
                    Nationality = u.UserInformation.Nationality
                }).ToList();
                
                return Ok(userModels);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [Authorize(Roles = "User")]
        [HttpGet("details")]
        public async Task<IActionResult> GetUserDetails()
        {
            try
            {
                var currentUser = HttpContext.User;
                var userId = Guid.Parse(currentUser.Claims.FirstOrDefault(claim => claim.Type == "userId").Value);
                
                var user = await _unitOfWork.Users.Get(userId);
                 
                var userModel = new UserDto()
                {
                    Password = user.Password,
                    ImageUrl = user.ImageUrl,
                    Phone = user.UserInformation.Phone,
                    Nationality = user.UserInformation.Nationality
                };
                
                return Ok(userModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [Authorize(Roles = "Administrator")]
        [HttpDelete("delete/{userId}")]
        public async Task<IActionResult> Delete(Guid userId)
        {
            try
            {
                var userToDelete = await _unitOfWork.Users.Get(userId);
                if (userToDelete == null)
                    throw new Exception("User doesn't exist");

                await _unitOfWork.Users.Delete(userToDelete);
                await _unitOfWork.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [Authorize(Roles = "User")]
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateUserDto model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.Nationality))
                    throw new Exception("Nationality should not be empty");
                if (string.IsNullOrWhiteSpace(model.Phone))
                    throw new Exception("Phone should not be empty");
                if (string.IsNullOrWhiteSpace(model.ImageUrl))
                    throw new Exception("Image Url should not be empty");
                if (string.IsNullOrWhiteSpace(model.Password))
                    throw new Exception("Password should not be empty");
                
                var currentUser = HttpContext.User;
                var userId = Guid.Parse(currentUser.Claims.FirstOrDefault(claim => claim.Type == "userId").Value);
                
                var userToUpdate = await _unitOfWork.Users.Get(userId);
                if(userToUpdate == null)
                    throw new Exception("User doesn't exist");

                var userInformationToUpdate = await _unitOfWork.UserInformations.FindByUserId(userToUpdate.Id);
                if (userInformationToUpdate == null)
                    throw new Exception("User's Information doesn't exist");

                userToUpdate.ImageUrl = model.ImageUrl;
                userToUpdate.Password = model.Password;
                userInformationToUpdate.Nationality = model.Nationality;
                userInformationToUpdate.Phone = model.Phone;

                await _unitOfWork.Users.Update(userToUpdate);
                await _unitOfWork.UserInformations.Update(userInformationToUpdate);
                await _unitOfWork.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}