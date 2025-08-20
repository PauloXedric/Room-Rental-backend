using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RRMS.Models.Pagination;
using RRMS.Services;
using RRMS.Views;

namespace RRMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViewController : ControllerBase
    {
        private readonly IViewService _viewService;

        public ViewController(IViewService viewService)
        {
            _viewService = viewService;
        }


        [HttpGet("user-emergency-contact")]
        public async Task<ActionResult<PagedResult<UserEmergencyContactView>>> UserEmergencyContactList([FromQuery] PaginationParams pagination, [FromQuery] string? firstNameFilter)
        {
            var contactList = await _viewService.UserEmergencyContactListAsync(pagination, firstNameFilter);

            return Ok(contactList);
        }

    }
}
