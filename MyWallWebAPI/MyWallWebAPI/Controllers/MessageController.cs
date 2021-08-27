using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWallWebAPI.Domain.Models;
using MyWallWebAPI.Domain.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyWallWebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly MessageService _messageService;

        public MessageController(MessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet("list-user-send-messages")]
        public async Task<ActionResult> ListUserSendMessages()
        {
            try
            {
                List<Message> list = await _messageService.ListUserSendMessages();

                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("list-user-received-messages")]
        public async Task<ActionResult> ListUserReceivedMessages()
        {
            try
            {
                List<Message> list = await _messageService.ListUserReceivedMessages();

                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("new-message")]
        public async Task<ActionResult> NewMessage([FromQuery] string userName, [FromBody] Message message)
        {
            try
            {
                message = await _messageService.NewMessage(userName, message);

                return Ok(message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-message")]
        public async Task<ActionResult> GetMessage([FromQuery] int messageId)
        {
            try
            {
                Message message = await _messageService.GetMessage(messageId);

                return Ok(message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete-message")]
        public async Task<ActionResult> DeleteMessage([FromQuery] int messageId)
        {
            try
            {
                return Ok(await _messageService.DeleteMessage(messageId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
