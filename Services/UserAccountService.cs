using Microsoft.AspNetCore.Identity;
using RRMS.Enums;
using RRMS.Models.Identity;
using RRMS.Models.Pagination;
using RRMS.Models.UserAccountModels;
using RRMS.Repositories;

namespace RRMS.Services
{
    public interface IUserAccountService 
    {
        Task<PagedResult<ReadAccountStatusModel>> UserStatusListAsync(PaginationParams pagination, string? firstNameFilter);
        Task<List<ReadUserIdModel>> GetAllTenantsIdAsync();
        Task<Result> RegisterNewUserAsync(RegisterUserModel userRegister);
        Task<(Result, ApplicationUser?)> LoginUserAsync(LoginUserModel userLogin);
        Task<bool> UpdateUserStatusAsync(UpdateUserStatusModel updateStatus);
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


        public async Task<PagedResult<ReadAccountStatusModel>> UserStatusListAsync(PaginationParams pagination, string? firstNameFilter)
        {
            try
            {
                var userList = await _userAccountRepository.GetAllAccountStatusAsync(firstNameFilter);

                var totalCount = userList.Count;

                var pagedData = userList
                   .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                   .Take(pagination.PageSize)              
                   .ToList();

                return new PagedResult<ReadAccountStatusModel>(pagedData, totalCount, pagination.PageNumber, pagination.PageSize);


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting a list of user account status.");
                throw;
            }
        }


        public async Task<List<ReadUserIdModel>> GetAllTenantsIdAsync()
        {
            try
            {
                var tenantUsers = await _userAccountRepository.GetAllTenantsIdAsync();

                return tenantUsers;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occorred while getting a list of tenant's Name and Id");
                throw;
            }
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


        public async Task<bool> UpdateUserStatusAsync(UpdateUserStatusModel updateStatus)
        {
            try
            {
                return await _userAccountRepository.UpdateUserStatusAsync(updateStatus);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating account status of user with id: {Id}", updateStatus.UserId);
                throw;
            }
        }
    }
}
