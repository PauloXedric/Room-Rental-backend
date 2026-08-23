using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RRMS.Data;
using RRMS.Enums;
using RRMS.Helpers;
using RRMS.Models.Identity;
using RRMS.Models.UserAccountModels;

namespace RRMS.Repositories
{
    public interface IUserAccountRepository 
    {
        Task<List<ReadAccountStatusModel>> GetAllAccountStatusAsync(string? firstNameFilter);
        Task<List<ReadUserIdModel>> GetAllTenantsIdAsync();
        Task<bool> CheckExistsAsync(string username);
        Task<bool> AddUserAsync(RegisterUserModel userRegister);
        Task<ApplicationUser?> GetUserLoginAsync(LoginUserModel userLogin);
        Task<bool> UpdateUserStatusAsync(UpdateUserStatusModel updateStatus);

    }

    public class UserAccountRepository : IUserAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _userRole;
        private readonly IMapper _mapper;

        public UserAccountRepository(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> userRole, IMapper mapper)
        {
            _userManager = userManager;
            _userRole = userRole;
            _mapper = mapper;
        }

       

        public async Task<List<ReadAccountStatusModel>> GetAllAccountStatusAsync(string? firstNameFilter)
        {
            var users = _userManager.Users.ToList();

            if (!string.IsNullOrEmpty(firstNameFilter))
            {
                users = users
                    .Where(u => u.FirstName.Contains(firstNameFilter, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            var result = new List<ReadAccountStatusModel>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);

                if (roles.Contains(RoleEnum.Admin.ToString()))
                    continue;

                result.Add(ReadAccountStatusModel.UserStatus(user));
            }

            return result;
        }


        public async Task<List<ReadUserIdModel>> GetAllTenantsIdAsync()
        {
            var tenantUsers = await _userManager.GetUsersInRoleAsync(RoleEnum.Tenant.ToString());

            return _mapper.Map<List<ReadUserIdModel>>(tenantUsers);
        }


        public async Task<bool> CheckExistsAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            return user != null;
        }


        public async Task<bool> AddUserAsync(RegisterUserModel userRegister)
        {

            var identityUser = _mapper.Map<ApplicationUser>(userRegister);        

            AuditHelper.SetCreatedAndModifiedOn(identityUser);          

            var newUser = await _userManager.CreateAsync(identityUser, userRegister.Password);

            if (!newUser.Succeeded)
                return false;

            var role = userRegister.Role.ToString();
            var roleExists = await _userRole.RoleExistsAsync(role);

            if (!roleExists)
            {
                await _userRole.CreateAsync(new IdentityRole(role));
            }

            await _userManager.AddToRoleAsync(identityUser, role);

            return true;
        }


        public async Task<ApplicationUser?> GetUserLoginAsync(LoginUserModel userLogin)
        {
            var identifyUser = await _userManager.FindByNameAsync(userLogin.Email);

            if (identifyUser is null)
            {
                return null;
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(identifyUser, userLogin.Password);

            if (!isPasswordValid)
            { 
                return null;
            }

            return identifyUser;
        }


        public async Task<bool> UpdateUserStatusAsync(UpdateUserStatusModel updateStatus)
        {
            var user = await _userManager.FindByIdAsync(updateStatus.UserId);
            if (user == null)
            {
                return false;
            }

            user.Status = updateStatus.Status;
            var result = await _userManager.UpdateAsync(user);

            return result.Succeeded;
        }


       


    }
}
