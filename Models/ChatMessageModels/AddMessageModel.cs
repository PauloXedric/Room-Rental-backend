using System.ComponentModel.DataAnnotations;

namespace RRMS.Models.ChatMessageModels
{
    public class AddMessageModel
    {
        public  string? SenderId { get; set; }

        [Required]
        public  required string Message { get; set; }
    }
}
