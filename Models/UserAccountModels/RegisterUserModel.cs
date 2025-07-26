using RRMS.Enums;
using System.ComponentModel.DataAnnotations;

namespace RRMS.Models.UserAccountModels
{
    public class RegisterUserModel
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        public required string Password { get; set; }

        [Required]
        public required string FirstName { get; set; }

        [Required]
        public required string LastName { get; set; }

        [Required]
        [Range(18, 120, ErrorMessage = "Only users who are 18 years old and above can register.")]
        public int Age { get; set; }

        [Required]
        public required string Gender { get; set; }

        [Required]
        public  required string Occupation { get; set; } 

        public RoleEnum Role { get; set; } = RoleEnum.Tenant;

     
    }
}
