using System.ComponentModel.DataAnnotations;

namespace RRMS.Models.EmergencyContactModels
{
    public class CreateEmergencyContactModel
    {     

        [Required]
        public required string Name { get; set; }

        [Required]
        public required string Relation { get; set; }

        [Required]
        public required string PhoneNumber { get; set; }

        [Required]
        public required string Address { get; set; }

        public  string UserId { get; set; } = string.Empty;
    }


}
