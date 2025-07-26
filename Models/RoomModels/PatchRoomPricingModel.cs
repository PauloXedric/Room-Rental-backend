using System.ComponentModel.DataAnnotations;

namespace RRMS.Models.RoomModels
{
    public class PatchRoomPricingModel
    {
        [Required]
        public int RoomId { get; set; }

        public decimal MonthlyPrice { get; set; } = 0;

        public decimal DepositAmount { get; set; } = 0;
    }
}
