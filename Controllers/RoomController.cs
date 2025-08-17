using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RRMS.Enums;
using RRMS.Helpers;
using RRMS.Models.Pagination;
using RRMS.Models.RoomModels;
using RRMS.Services;

namespace RRMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }


        [HttpGet]
        public async Task<ActionResult<PagedResult<ReadRoomModel>>> GetAllRoomDetails([FromQuery]PaginationParams pagination, [FromQuery] string? filter)
        {         
            var requestResult = await _roomService.GetRoomDetailsAsync(pagination, filter);
            return Ok(requestResult);
        }



        [HttpPost]
        public async Task<IActionResult> AddNewRoom([FromBody] CreateRoomModel addRoom)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _roomService.AddRoomAsync(addRoom);

            return result switch
            {
                Result.Success => Ok(ApiResponse.SuccessMessage("Added room successfully.")),
                Result.Failed => BadRequest(ApiResponse.FailMessage("Adding room failed.")),
                _ => StatusCode(500, ApiResponse.FailMessage("An unexpected result occurred during adding emergency contact."))
            };
        }


        [HttpPatch("update-information")]
        public async Task<IActionResult> UpdateRoomInfo([FromBody] PatchRoomInfoModel roominfo) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _roomService.UpdateRoomInformationAsync(roominfo);

            return result switch
            {
                Result.Success => Ok(ApiResponse.SuccessMessage("Room information updated successfully.")),
                Result.DoesNotExist => NotFound(ApiResponse.FailMessage("Room not found.")),
                Result.Failed => StatusCode(500, ApiResponse.FailMessage("Failed to update room information due to a system error.")),
                _ => StatusCode(500, ApiResponse.FailMessage("Unexpected result."))
            };
        }



        [HttpPatch("update-price")]
        public async Task<IActionResult> UpdateRoomPrice([FromBody]PatchRoomPricingModel roomPrice)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _roomService.UpdateRoomPricingAsync(roomPrice);

            return result switch 
            {
                Result.Success => Ok(ApiResponse.SuccessMessage("Room price updated successfully.")),
                Result.DoesNotExist => NotFound(ApiResponse.FailMessage("Room not found.")),
                Result.Failed => StatusCode(500, ApiResponse.FailMessage("Failed to update room price due to a system error.")),
                _ => StatusCode(500, ApiResponse.FailMessage("Unexpected result."))
            };
        }


        [HttpPatch("update-availability")]
        public async Task<IActionResult> UpdateRoomAvailability([FromBody] PatchRoomAvailabilityModel roomAvail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _roomService.UpdateRoomAvailability(roomAvail);

            return result
                ? Ok(ApiResponse.SuccessMessage("Room availability updated successfully."))
                : NotFound(ApiResponse.FailMessage("Room not found."));
        }


         
        [HttpDelete("{roomId}")]
        public async Task<IActionResult> DeleteRoom([FromRoute] int roomId)
        {
            if (roomId <= 0)
            {
                return BadRequest("Invalid Room Id");
            }

            var result =  await _roomService.DeleteRoomAsync(roomId);

            return result switch
            {
                Result.Success => Ok(ApiResponse.SuccessMessage("Room has been deleted successfully.")),
                Result.Failed => StatusCode(500, ApiResponse.FailMessage("Failed to delete room due to a system error.")),
                _ => StatusCode(500, ApiResponse.FailMessage("Unexpected result."))
            };
        }

    }
}
