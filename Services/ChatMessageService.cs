using AutoMapper;
using RRMS.Entities;
using RRMS.Helpers;
using RRMS.Models.ChatMessageModels;
using RRMS.Repositories;

namespace RRMS.Services
{
    public interface IChatMessageService
    {
        Task<ReadMessageModel> SaveMessage(AddMessageModel addMessage);
        Task<IEnumerable<ReadMessageModel>> GetChatHistoryAsync(string userId);
    }


    public class ChatMessageService : IChatMessageService
    {
        private readonly IChatMessageRepository _chatMessageRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ChatMessageService> _logger;

        public ChatMessageService(IChatMessageRepository chatMessageRepository, IMapper mapper, ILogger<ChatMessageService> logger)
        {
            _chatMessageRepository = chatMessageRepository;
            _mapper = mapper;
            _logger = logger;
        }


        public async Task<IEnumerable<ReadMessageModel>> GetChatHistoryAsync(string userId)
        {
            try
            {
                var messages = await _chatMessageRepository.GetChatHistoryAsync(userId);

                return _mapper.Map<IEnumerable<ReadMessageModel>>(messages);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while getting a message history for user with Id:{Sender}", userId);
                throw;
            }
        }


        public async Task<ReadMessageModel> SaveMessage(AddMessageModel addMessage)
        {
            try
            {
                var messageEntity = _mapper.Map<ChatMessageEntity>(addMessage);

                messageEntity.ReceiverId = await _chatMessageRepository.GetAdminIdAsync();

                messageEntity.SentAt = LocalTimeHelper.GetPhilippineTimeNow();

                await _chatMessageRepository.AddAsync(messageEntity);

                return _mapper.Map<ReadMessageModel>(messageEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while saving a message for user with Id:{Sender}", addMessage.SenderId);
                throw;
            }
        }


       


    }
}
