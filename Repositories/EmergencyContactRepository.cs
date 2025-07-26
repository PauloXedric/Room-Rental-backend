using Microsoft.EntityFrameworkCore;
using RRMS.Data;
using RRMS.Entities;
using RRMS.Models.EmergencyContactModels;

namespace RRMS.Repositories
{

    public interface IEmergencyContactRepository : IBaseRepository<EmergencyContactEntity>
    {
        Task<EmergencyContactEntity?> GetEmergencyContactByUserIdAsync(string userId);
    }


    public class EmergencyContactRepository : BaseRepository<EmergencyContactEntity>, IEmergencyContactRepository
    {
        private readonly AppDbContext _dbContext;

        public EmergencyContactRepository(AppDbContext dbContext) : base(dbContext)
        {
              _dbContext = dbContext;    
        }


        public async Task<EmergencyContactEntity?> GetEmergencyContactByUserIdAsync(string userId)
        {
            return await _dbContext.EmergencyContacts
                .FirstOrDefaultAsync(ec => ec.UserId == userId);
        }



    }
}
