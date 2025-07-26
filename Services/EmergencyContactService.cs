using AutoMapper;
using RRMS.Entities;
using RRMS.Enums;
using RRMS.Helpers;
using RRMS.Models.EmergencyContactModels;
using RRMS.Repositories;

namespace RRMS.Services
{
    public interface IEmergencyContactService 
    {
        Task<ReadEmergencyContactModel> GetContactInfoByUserIdAsync(string userId);
        Task<Result> AddEmergencyContactAsync(CreateEmergencyContactModel createContact);
        Task<Result> PatchEmergencyContactAsync(PatchEmergencyContactModel updateContact);

    }
    

    public class EmergencyContactService : IEmergencyContactService
    {
        private readonly IMapper _mapper;
        private readonly IEmergencyContactRepository _emergencyContactRepository;
        private readonly ILogger<IEmergencyContactService> _logger;

        public EmergencyContactService(IMapper mapper, IEmergencyContactRepository emergencyContactRepository,
                                       ILogger<IEmergencyContactService> logger)
        {
            _mapper = mapper;
            _emergencyContactRepository = emergencyContactRepository;
            _logger = logger;
        }


        public async Task<ReadEmergencyContactModel> GetContactInfoByUserIdAsync(string userId)
        {
            try 
            {
                var contactInfo = await _emergencyContactRepository.GetEmergencyContactByUserIdAsync(userId);

                return _mapper.Map<ReadEmergencyContactModel>(contactInfo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting emergency info of user with Id: {UserId}", userId);
                throw;
            }
        }



        public async Task<Result> AddEmergencyContactAsync(CreateEmergencyContactModel createContact)
        {
            try
            {
                var contactEntity = _mapper.Map<EmergencyContactEntity>(createContact);

                AuditHelper.SetCreatedAndModifiedOn(contactEntity);

                var result = await _emergencyContactRepository.AddAsync(contactEntity);

                return result > 0 ? Result.Success : Result.Failed; 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding emergency contact for user with ID: {Contact}", createContact.UserId);
                throw;
            }
        }


        public async Task<Result> PatchEmergencyContactAsync(PatchEmergencyContactModel updateContact)
        {
            try
            {
                var contactExist = await _emergencyContactRepository.GetByIdAsync(updateContact.EmergencyContactId);

                if (contactExist == null)
                {
                    return Result.DoesNotExist;
                }

                AuditHelper.SetModifiedOn(contactExist); 

                _mapper.Map(updateContact, contactExist);

                await _emergencyContactRepository.UpdateAsync(contactExist);

                return Result.Success;
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Error occurred while updating emergency contact with Id: {Id}", updateContact.EmergencyContactId);
                throw;
            }
        }


         
    }
}
