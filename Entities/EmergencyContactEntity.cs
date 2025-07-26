using RRMS.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace RRMS.Entities
{
    public class EmergencyContactEntity : IAuditable
    {
        [Key]
        public int  EmergencyContactId { get; set; }

        public string Name { get; set; }

        public string Relation { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public string UserId { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
