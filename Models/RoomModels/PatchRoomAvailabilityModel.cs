using System.ComponentModel.DataAnnotations;

namespace RRMS.Models.RoomModels
{
    public class PatchRoomAvailabilityModel
    {
        [Required]
        public int RoomId { get; set; }

        public DateTime? InquireFromDate { get; set; } = null;

        public DateTime? InquireToDate { get; set; } = null;

        public DateTime? AvailableStartDate { get; set; } = null;
    }
}
