using RRMS.Enums;

namespace RRMS.Models.RoomModels
{
    public class ReadRoomModel
    {
        public int RoomId { get; set; }

        public  string RoomName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public RoomTypeEnum? RoomType { get; set; } = null;

        public int NumberOfBeds { get; set; } = 0;

        public int MaxOccupants { get; set; } = 0;

        public int CurrentOccupantsCount { get; set; } = 0;

        public bool IsAvailable { get; set; } = true;

        public GenderRestrictionEnum? GenderRestriction { get; set; } = null;

        public string RoomAddress { get; set; } = string.Empty;

        public decimal MonthlyPrice { get; set; } = 0;

        public decimal DepositAmount { get; set; } = 0;
    }
}
