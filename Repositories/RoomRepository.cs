using RRMS.Data;
using RRMS.Entities;

namespace RRMS.Repositories
{
    public interface IRoomRepository : IBaseRepository<RoomEntity>
    {
        IQueryable<RoomEntity> GetAllRoomByName(string? filter);
    }


    public class RoomRepository : BaseRepository<RoomEntity>, IRoomRepository
    {
        private readonly AppDbContext _dbContext;


        public RoomRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }


        public IQueryable<RoomEntity> GetAllRoomByName(string? filter)
        {
            var query = _dbContext.Room.AsQueryable();

            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(r => r.RoomName.Contains(filter));
            }

            return query.OrderBy(r => r.RoomName);
        }



    }
}
