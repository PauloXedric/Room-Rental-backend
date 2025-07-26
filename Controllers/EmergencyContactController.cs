using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RRMS.Enums;
using RRMS.Helpers;
using RRMS.Models.EmergencyContactModels;
using RRMS.Services;
using System.Security.Claims;

namespace RRMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmergencyContactController : ControllerBase
    {
        private readonly IEmergencyContactService _emergencyContactService;

        public EmergencyContactController(IEmergencyContactService emergencyContactService) 
        {
            _emergencyContactService = emergencyContactService;
        }


        [HttpGet]
        public async Task<ActionResult<ReadEmergencyContactModel>> GetUserEmergencyInfo()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User ID not found.");
            }

            return await _emergencyContactService.GetContactInfoByUserIdAsync(userId);
        }



        [HttpPost]
        public async Task<IActionResult> AddEmergencyContact([FromBody] CreateEmergencyContactModel addContact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User ID not found.");
            }

            addContact.UserId = userId;

            var result = await _emergencyContactService.AddEmergencyContactAsync(addContact);

            return result switch
            {
                Result.Success => Ok(ApiResponse.SuccessMessage("Added emergency contact successfully.")),
                Result.Failed => BadRequest(ApiResponse.FailMessage("Adding emergency contact failed.")),
                _ => StatusCode(500, ApiResponse.FailMessage("An unexpected result occurred during adding emergency contact."))
            };
        }


        [HttpPatch]
        public async Task<IActionResult> PatchEmergencyContact([FromBody] PatchEmergencyContactModel patchContact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _emergencyContactService.PatchEmergencyContactAsync(patchContact);

            return result switch
            {
                Result.Success => Ok(ApiResponse.SuccessMessage("Updated emergency contact successfully.")),
                Result.DoesNotExist => NotFound(ApiResponse.FailMessage("User with that emergency contact doesnot exist.")),
                _ => StatusCode(500, ApiResponse.FailMessage("An unexpected result occurred during patching emergency contact."))
            };
        }

    }
}
