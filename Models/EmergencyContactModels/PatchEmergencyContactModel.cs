using System.ComponentModel.DataAnnotations;

namespace RRMS.Models.EmergencyContactModels
{
    public class PatchEmergencyContactModel
    {
        [Required]
        public int EmergencyContactId { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        public required string Relation { get; set; }

        [Required]
        public required string PhoneNumber { get; set; }

        [Required]
        public required string Address { get; set; }
    }
}
