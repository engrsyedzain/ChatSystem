using System.ComponentModel.DataAnnotations.Schema;

namespace ChatSystem.Models
{
    public class Invitation
    {
        public int InvitationId { get; set; }

        public int? SenderId { get; set; }
        public int? ReceiverId { get; set; }
        public int? StatusId { get; set; }
        public string SenderConnectionId { get; set; }
        public string ReceiverConnectionId { get; set; }

        // Navigation properties
        [ForeignKey("SenderId")]
        public Member Sender { get; set; }
        [ForeignKey("ReceiverId")]
        public Member Receiver { get; set; }

        [ForeignKey("StatusId")]
        public InvitationStatus Status { get; set; }

    }
}
