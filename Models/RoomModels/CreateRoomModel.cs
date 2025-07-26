using RRMS.Enums;
using System.ComponentModel.DataAnnotations;

namespace RRMS.Models.RoomModels
{
    public class CreateRoomModel
    {

        [Required]
        public required string RoomName { get; set; }

        public string Description { get; set; } = string.Empty;

        public RoomTypeEnum RoomType { get; set; } = RoomTypeEnum.None;

        [Required]
        public int NumberOfBeds { get; set; }

        [Required]
        public int MaxOccupants { get; set; }

        public GenderRestrictionEnum GenderRestriction { get; set; } = GenderRestrictionEnum.None;

        public string RoomAddress { get; set; } = string.Empty;

        public decimal MonthlyPrice { get; set; } = 0;

        public decimal DepositAmount { get; set; } = 0;

        public DateTime? InquireFromDate { get; set; } = null; 

        public DateTime? InquireToDate { get; set; } = null;

        public DateTime? AvailableStartDate { get; set; } = null;
    }
}
