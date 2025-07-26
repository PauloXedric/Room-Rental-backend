using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using RRMS.Entities;
using RRMS.Enums;
using RRMS.Models.Pagination;
using RRMS.Models.RoomModels;
using RRMS.Repositories;

namespace RRMS.Services
{
    public interface IRoomService 
    {
        Task<PagedResult<ReadRoomModel>> GetRoomDetailsAsync(PaginationParams pagination, string? filter);
        Task<Result> AddRoomAsync(CreateRoomModel addRoom);
        Task<Result> UpdateRoomInformationAsync(PatchRoomInfoModel roomInfo);
        Task<Result> UpdateRoomPricingAsync(PatchRoomPricingModel roomPrice);
        Task<Result> DeleteRoomAsync(int roomId);
    }


    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<RoomService> _logger;   

        public RoomService(IRoomRepository roomRepository, IMapper mapper, ILogger<RoomService> logger)
        {
            _roomRepository = roomRepository;
            _mapper = mapper;
            _logger = logger;
        }


        public async Task<PagedResult<ReadRoomModel>> GetRoomDetailsAsync(PaginationParams pagination, string? filter)
        {
            try
            {
                var query = _roomRepository.GetAllRoomByName(filter);

                var totalCount = await query.CountAsync();

                var pagedData = await query
                    .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                    .Take(pagination.PageSize)
                    .ProjectTo<ReadRoomModel>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                return new PagedResult<ReadRoomModel>(pagedData, totalCount, pagination.PageNumber, pagination.PageSize);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Error occurred while fetching paginated data of room");
                throw;
            }
        }


        public async Task<Result> AddRoomAsync(CreateRoomModel addRoom)
        {
            try
            {
                var roomEntity = _mapper.Map<RoomEntity>(addRoom);

                var result = await _roomRepository.AddAsync(roomEntity);

                return result > 0 ? Result.Success : Result.Failed;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding new room with name: {RoomName}", addRoom.RoomName);
                throw;
            }   
        }


        public async Task<Result> UpdateRoomInformationAsync(PatchRoomInfoModel roomInfo)
        {
            try
            {
                var roomEntity = await _roomRepository.GetByIdAsync(roomInfo.RoomId);

                if (roomEntity == null)
                {
                    return Result.DoesNotExist;
                }

                _mapper.Map(roomInfo, roomEntity);

                var updated = await _roomRepository.UpdateAsync(roomEntity);

                return updated ? Result.Success : Result.Failed;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating information of room with Id: {RoomId}", roomInfo.RoomId);
                throw;
            }
        }


        public async Task<Result> UpdateRoomPricingAsync(PatchRoomPricingModel roomPrice)
        {
            try
            {
                var roomEntity = await _roomRepository.GetByIdAsync(roomPrice.RoomId);

                if (roomEntity == null)
                {
                    return Result.DoesNotExist;
                }

                _mapper.Map(roomPrice, roomEntity);

                var updated = await _roomRepository.UpdateAsync(roomEntity);

                return updated ? Result.Success : Result.Failed;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while updating room pricing of room with Id: {RoomId}", roomPrice.RoomId);
                throw;
            }   
        }


        public async Task<bool> UpdateRoomAvailability(PatchRoomAvailabilityModel roomAvail)
        {
            try
            {
                var roomEntity = await _roomRepository.GetByIdAsync(roomAvail.RoomId);

                if (roomEntity == null)
                { 
                    return false;
                }

                _mapper.Map(roomAvail, roomEntity);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating room availability of room with Id: {RoomId}", roomAvail.RoomId);
                throw;
            }
        }


        public async Task<Result> DeleteRoomAsync(int roomId)
        {
            try
            {
                var result = await _roomRepository.DeleteAsync(roomId);

                if (result == false)
                {
                    return Result.Failed;                 
                }

                return Result.Success;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while deleting a room with Id: {RoomId}", roomId);
                throw;
            }
        }





    }
}
