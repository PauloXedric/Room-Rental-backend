using Microsoft.AspNetCore.Identity;
using RRMS.Abstractions;
using RRMS.Enums;

namespace RRMS.Models.Identity
{
    public class ApplicationUser : IdentityUser, IAuditable
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public int Age { get; set; }

        public string Gender { get; set; } = string.Empty;

        public string Occupation { get; set; } = string.Empty;

        public UserStatusEnum Status { get; set; } = UserStatusEnum.Inactive;

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
