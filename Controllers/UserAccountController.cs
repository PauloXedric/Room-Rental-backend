using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RRMS.Enums;
using RRMS.Helpers;
using RRMS.Models.Pagination;
using RRMS.Models.UserAccountModels;
using RRMS.Services;

namespace RRMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccountController : ControllerBase
    {
        private readonly IUserAccountService _userAccountService;
        private readonly IWebTokenService _webTokenService;

        public UserAccountController(IUserAccountService userAccountService, IWebTokenService webTokenService) 
        {
            _userAccountService = userAccountService;
            _webTokenService = webTokenService;
        }


        [HttpGet("user-status")]
        public async Task<ActionResult<PagedResult<ReadAccountStatusModel>>> UserStatusList([FromQuery] PaginationParams pagination, [FromQuery] string? firstNameFilter)
        {
            var userList = await _userAccountService.UserStatusListAsync(pagination, firstNameFilter);

            return Ok(userList);
        }


        [HttpGet("users-id")]
        public async Task<ActionResult<List<ReadUserIdModel>>> GetAllTenantId()
        {
            var tenants = await _userAccountService.GetAllTenantsIdAsync();

            return tenants;
        }


        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserModel userRegister)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userAccountService.RegisterNewUserAsync(userRegister);

            return result switch
            {
                Result.Success => Ok(ApiResponse.SuccessMessage("Successfully registered.")),
                Result.AlreadyExist => Conflict(ApiResponse.FailMessage("The email you entered is already registered.")),
                _ => StatusCode(500, ApiResponse.FailMessage("An unexpected result occurred during registration."))
            };
        }


        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginUserModel userLogin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var (result, user) = await _userAccountService.LoginUserAsync(userLogin);

            return result switch
            {
                Result.Success => Ok(new
                {
                    tokenString = await _webTokenService.GenerateUserTokenAsync(user!)
                }),
                Result.DoesNotExist => NotFound(ApiResponse.FailMessage("Account does not exist.")),
                _ => StatusCode(500, ApiResponse.FailMessage("Unexpected login error."))
            };        
        }


        [HttpPatch("update-status")]
        public async Task<IActionResult> UpdateUserStatus([FromBody] UpdateUserStatusModel updateStatus)
        {
            var result = await _userAccountService.UpdateUserStatusAsync(updateStatus);

            if (!result)
            {
                return NotFound(ApiResponse.FailMessage("Updating user status failed."));
            }

            return Ok(ApiResponse.SuccessMessage("User status updated successfully."));
        }


    }
}
