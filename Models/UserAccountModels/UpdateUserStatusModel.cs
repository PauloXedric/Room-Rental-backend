using RRMS.Enums;

namespace RRMS.Models.UserAccountModels
{
    public class UpdateUserStatusModel
    {
        public string UserId { get; set; } = null!;

        public UserStatusEnum Status { get; set; }
    }
}
