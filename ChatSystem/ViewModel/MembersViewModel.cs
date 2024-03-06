using ChatSystem.Models;

namespace ChatSystem.ViewModel
{
    public class MembersViewModel
    {
        public int MemberId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int? Status { get; set; }
        public InvitationStatus  InvitationStatus { get; set; }
    }
}
