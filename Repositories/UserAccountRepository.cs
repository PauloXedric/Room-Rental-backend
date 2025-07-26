using Microsoft.AspNetCore.Identity;
using RRMS.Data;
using RRMS.Enums;
using RRMS.Helpers;
using RRMS.Models.Identity;
using RRMS.Models.UserAccountModels;

namespace RRMS.Repositories
{
    public interface IUserAccountRepository 
    {
        Task<bool> CheckExistsAsync(string username);
        Task<bool> AddUserAsync(RegisterUserModel userRegister);
        Task<ApplicationUser?> GetUserLoginAsync(LoginUserModel userLogin);

    }

    public class UserAccountRepository : IUserAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _userRole;

        public UserAccountRepository(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> userRole)
        {
            _userManager = userManager;
            _userRole = userRole;
        }

        public async Task<bool> CheckExistsAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            return user != null;
        }


        public async Task<bool> AddUserAsync(RegisterUserModel userRegister)
        {
            var identityUser = new ApplicationUser
            {
                UserName = userRegister.Email,
                Email = userRegister.Email,
                FirstName = userRegister.FirstName,
                LastName = userRegister.LastName,
                Age = userRegister.Age,
                Gender = userRegister.Gender,
                Occupation = userRegister.Occupation,           
            };

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


     //   public async Task<>


    }
}
