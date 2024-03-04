using System.ComponentModel.DataAnnotations.Schema;

namespace ChatSystem.Models
{
    public class Message
    {
        public int MessageId { get; set; }
        public int? SenderId { get; set; }
        public int? ReceiverId { get; set; }
        public string MessageText { get; set; }
        public DateTime CreatedDate { get; set; }

        // Navigation properties
        [ForeignKey("SenderId")]
        public Member Sender { get; set; }
        [ForeignKey("ReceiverId")]
        public Member Receiver { get; set; }

    }
}
