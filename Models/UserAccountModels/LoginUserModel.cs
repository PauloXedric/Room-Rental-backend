using System.ComponentModel.DataAnnotations;

namespace RRMS.Models.UserAccountModels
{
    public class LoginUserModel
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        public required string Password { get; set; }
    }
}
