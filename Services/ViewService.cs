using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RRMS.Models.Pagination;
using RRMS.Repositories;
using RRMS.Views;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace RRMS.Services
{
    public interface IViewService
    {
        Task<PagedResult<UserEmergencyContactView>> UserEmergencyContactListAsync(PaginationParams pagination, string? firstNameFilter);
    }

    public class ViewService : IViewService
    {
        private readonly IViewRepository _viewRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ViewService> _logger;

        public ViewService(IViewRepository viewRepository, IMapper mapper, ILogger<ViewService> logger) 
        {
            _viewRepository = viewRepository;
            _mapper = mapper;
            _logger = logger;
        }


        public async Task<PagedResult<UserEmergencyContactView>> UserEmergencyContactListAsync(PaginationParams pagination, string? firstNameFilter)
        {
            try
            {
                var contactList =  _viewRepository.GetUserEmergencyContactAsync(firstNameFilter);

                var totalCount = await contactList.CountAsync();

                var pagedData = await contactList
                 .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                 .Take(pagination.PageSize)
                 .ToListAsync();

                return new PagedResult<UserEmergencyContactView>(pagedData, totalCount, pagination.PageNumber, pagination.PageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching a paginated list of user emergency contact.");
                throw;
            }
        }
    }
}
