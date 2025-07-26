using RRMS.Abstractions;
using RRMS.Enums;
using System.ComponentModel.DataAnnotations;

namespace RRMS.Entities
{
    public class RoomEntity 
    {
        [Key]
        public int RoomId { get; set; }      

        public required string RoomName { get; set; }

        public string? Description { get; set; }

        public RoomTypeEnum RoomType { get; set; } 

        public int NumberOfBeds { get; set; }

        public int MaxOccupants { get; set; }

        public int CurrentOccupantsCount { get; set; }

        public bool IsAvailable { get; set; } = true;

        public GenderRestrictionEnum GenderRestriction { get; set; }

        public string? RoomAddress { get; set; }

        public decimal? MonthlyPrice { get; set; }

        public decimal? DepositAmount { get; set; }

        public DateTime? InquireFromDate { get; set; }

        public DateTime? InquireToDate { get; set; }

        public DateTime? AvailableStartDate { get; set; }
    }
}
