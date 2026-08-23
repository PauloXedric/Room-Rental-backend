using RRMS.Abstractions;

namespace RRMS.Entities
{
    public class ChatMessageEntity 
    {
        public int Id { get; set; }
        public string SenderId { get; set; }  
        public string ReceiverId { get; set; } 
        public string Message { get; set; }
        public DateTime SentAt { get; set; }
    }
}
