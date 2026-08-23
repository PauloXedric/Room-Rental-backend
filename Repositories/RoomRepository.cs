using RRMS.Data;
using RRMS.Entities;

namespace RRMS.Repositories
{
    public interface IRoomRepository : IBaseRepository<RoomEntity>
    {
        IQueryable<RoomEntity> GetRoomsByName(string? roomNameFilter);

    }


    public class RoomRepository : BaseRepository<RoomEntity>, IRoomRepository
    {
        private readonly AppDbContext _dbContext;


        public RoomRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }


        public IQueryable<RoomEntity> GetRoomsByName(string? roomNameFilter)
        {
            var query = _dbContext.Room.AsQueryable();

            if (!string.IsNullOrEmpty(roomNameFilter))
            {
                query = query.Where(r => r.RoomName.Contains(roomNameFilter));
            }

            return query.OrderBy(r => r.RoomName);
        }



    }
}
