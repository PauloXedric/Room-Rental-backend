using RRMS.Data;
using RRMS.Views;

namespace RRMS.Repositories
{
    public interface IViewRepository
    {
        IQueryable<UserEmergencyContactView> GetUserEmergencyContactAsync(string? firstNameFilter);
    }

    public class ViewRepository : IViewRepository
    {
        private readonly AppDbContext _dbContext;

        public ViewRepository(AppDbContext dbContext) 
        { 
            _dbContext = dbContext;
        }


        public IQueryable<UserEmergencyContactView> GetUserEmergencyContactAsync(string? firstNameFilter) 
        {
            var query = _dbContext.UserEmergencyContact.AsQueryable();

            if (!string.IsNullOrEmpty(firstNameFilter))
            {
                query = query.Where(x => x.FirstName.Contains(firstNameFilter));
            }

            return query;
        }
    }
}
