using RRMS.Enums;
using RRMS.Models.Identity;

namespace RRMS.Models.UserAccountModels
{
    public class ReadAccountStatusModel
    {
        public string Id { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public UserStatusEnum Status { get; set; } = UserStatusEnum.None;


        public static ReadAccountStatusModel UserStatus(ApplicationUser user)
        {
            return new ReadAccountStatusModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email ?? string.Empty,
                Status = user.Status        
            };
        }
    }
}
