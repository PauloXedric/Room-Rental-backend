using RRMS.Enums;
using RRMS.Models.Identity;
using RRMS.Models.UserAccountModels;
using RRMS.Repositories;

namespace RRMS.Services
{
    public interface IUserAccountService 
    {
        Task<Result> RegisterNewUserAsync(RegisterUserModel userRegister);
        Task<(Result, ApplicationUser?)> LoginUserAsync(LoginUserModel userLogin);
    }


    public class UserAccountService : IUserAccountService
    {
        private readonly IUserAccountRepository _userAccountRepository;
        private readonly ILogger<UserAccountService> _logger;

        public UserAccountService(IUserAccountRepository userAccountRepository, ILogger<UserAccountService> logger)
        {
            _userAccountRepository = userAccountRepository;
            _logger = logger;
        }

        public async Task<Result> RegisterNewUserAsync(RegisterUserModel userRegister)
        {
            try
            {           
                bool userExist = await _userAccountRepository.CheckExistsAsync(userRegister.Email);

                if(userExist)
                {
                    return Result.AlreadyExist;
                }

                await _userAccountRepository.AddUserAsync(userRegister);

                return Result.Success;         
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while registering new user with email: {Email}", userRegister.Email);
                throw;
            }
        }


        public async Task<(Result, ApplicationUser?)> LoginUserAsync(LoginUserModel userLogin)
        {
            try
            {
                var user = await _userAccountRepository.GetUserLoginAsync(userLogin);
                
                if (user == null)
                {
                    return (Result.DoesNotExist, null); 
                }

                return (Result.Success, user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while logging in user with email: {Email}", userLogin.Email);
                throw;
            }
        }


    }
}
