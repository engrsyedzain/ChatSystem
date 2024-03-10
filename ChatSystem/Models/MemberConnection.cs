using System.ComponentModel.DataAnnotations.Schema;

namespace ChatSystem.Models
{
    public class MemberConnection
    {
        public int MemberConnectionId { get; set; }
        public int MemberId { get; set; }
        public string? ConnectionId { get; set; }

        [ForeignKey("MemberId")]
        public Member Member { get; set; }
    }
}
