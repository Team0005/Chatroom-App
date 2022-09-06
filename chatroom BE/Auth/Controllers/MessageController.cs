using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Auth.Dto.Message;
using Domain;
using Auth.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence;
using Persistence.Interfaces;

namespace Auth.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public MessageController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Authorize(Roles = "User")]
        [HttpPost("send")]
        public async Task<IActionResult> SendMessage(CreateMessageDto model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.Content))
                    throw new Exception("Message content should not be empty");
                
                var currentUser = HttpContext.User;
                var userId = Guid.Parse(currentUser.Claims.FirstOrDefault(claim => claim.Type == "userId").Value);

                var sender = await _unitOfWork.Users.Get(userId);
                if (sender == null)
                    throw new Exception("Sender doesn't exist");
                
                var receiver = await _unitOfWork.Users.Get(model.ReceiverId);
                if (receiver == null)
                    throw new Exception("Receiver doesn't exist");

                var message = new Message(sender.Id, receiver.Id, model.Content);
                await _unitOfWork.Messages.Add(message);
                await _unitOfWork.Save();
                
                Console.WriteLine(userId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [Authorize(Roles = "User")]
        [HttpGet("messages/{receiverId}")]
        public async Task<IActionResult> GetMessages(Guid receiverId)
        {
            try
            {
                var currentUser = HttpContext.User;
                var userId = Guid.Parse(currentUser.Claims.FirstOrDefault(claim => claim.Type == "userId").Value);
     
                var sender = await _unitOfWork.Users.Get(userId);
                if (sender == null)
                    throw new Exception("Sender doesn't exist");
                
                var receiver = await _unitOfWork.Users.Get(receiverId);
                if (receiver == null)
                    throw new Exception("Receiver doesn't exist");

                var messages = await _unitOfWork.Messages.GetBySenderIdAndReceiverId(sender.Id, receiver.Id);

                var model = messages
                    .OrderBy(message => message.TimeStamp)
                    .Select(message => new MessageDto()
                {
                    Id = message.Id,
                    Content = message.Content,
                    TimeStamp = message.TimeStamp,
                    UserImageUrl = message.Sender.ImageUrl,
                    Username = message.Sender.Username
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