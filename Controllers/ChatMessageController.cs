using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RRMS.Enums;
using RRMS.Models.ChatMessageModels;
using RRMS.Services;
using System.Security.Claims;

namespace RRMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatMessageController : ControllerBase
    {
        private readonly IChatMessageService _chatMessageService;

        public ChatMessageController(IChatMessageService chatMessageService)
        {
            _chatMessageService = chatMessageService;
        }


        [HttpGet("message-history")]
        public async Task<IActionResult> GetChatHistory([FromQuery] string? tenantId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            if (User.IsInRole(RoleEnum.Tenant.ToString()))
            {
                var history = await _chatMessageService.GetChatHistoryAsync(userId);
                return Ok(history);
            }

            var targetUserId = tenantId ?? userId;
            var historyForLandlord = await _chatMessageService.GetChatHistoryAsync(targetUserId);
            return Ok(historyForLandlord);
        }



        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] AddMessageModel addMessage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var senderId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (senderId == null)
                return Unauthorized();

            addMessage.SenderId = senderId;

            var savedMessage = await _chatMessageService.SaveMessage(addMessage);
            return Ok(savedMessage);
        }
    }
}
