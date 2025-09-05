using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RRMS.Data;
using RRMS.Entities;
using RRMS.Enums;
using RRMS.Models.Identity;

namespace RRMS.Repositories
{
    public interface IChatMessageRepository : IBaseRepository<ChatMessageEntity>
    {
        Task<IEnumerable<ChatMessageEntity>> GetChatHistoryAsync(string userId);
        Task<string> GetAdminIdAsync();
    }

    public class ChatMessageRepository : BaseRepository<ChatMessageEntity>, IChatMessageRepository
    {
        public readonly AppDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public ChatMessageRepository(AppDbContext dbContext, UserManager<ApplicationUser> userManager) : base(dbContext)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<IEnumerable<ChatMessageEntity>> GetChatHistoryAsync(string tenantId)
        {
            var usersInRole = await _userManager.GetUsersInRoleAsync(RoleEnum.Admin.ToString());
            var landlord = usersInRole.FirstOrDefault();

            if (landlord == null)
            {
                return Enumerable.Empty<ChatMessageEntity>();
            }
                
            var landlordId = landlord.Id;

            return await _dbContext.ChatMessages
                .Where(m =>
                    (m.SenderId == tenantId && m.ReceiverId == landlordId) ||
                    (m.SenderId == landlordId && m.ReceiverId == tenantId))
                .OrderBy(m => m.SentAt)
                .ToListAsync();
        }


        public async Task<string> GetAdminIdAsync()
        {
            var users = _userManager.Users.ToList();

            foreach (var user in users)
            {
                if (await _userManager.IsInRoleAsync(user, RoleEnum.Admin.ToString()))
                {
                    return user.Id; 
                }
            }

            throw new InvalidOperationException("No admin user exists in the system.");
        }



    }
}
