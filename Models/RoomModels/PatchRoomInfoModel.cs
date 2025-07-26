using RRMS.Enums;
using System.ComponentModel.DataAnnotations;

namespace RRMS.Models.RoomModels
{
    public class PatchRoomInfoModel
    {
        [Required]
        public int RoomId { get; set; }

        [Required]
        public required string RoomName { get; set; }

        public string Description { get; set; } = string.Empty;

        public RoomTypeEnum RoomType { get; set; }

        [Required]
        public int NumberOfBeds { get; set; }

        [Required]
        public int MaxOccupants { get; set; }

        public GenderRestrictionEnum GenderRestriction { get; set; }

        public string RoomAddress { get; set; } = string.Empty;

    }
}
