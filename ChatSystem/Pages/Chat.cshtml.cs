using ChatSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChatSystem.Pages
{
    public class ChatModel : PageModel
    {
        private readonly AppDbContext _context;

        public ChatModel(AppDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            var recieverMemberId = Convert.ToInt32(Request.Query["id"]);
            string email = HttpContext.Session.GetString(Sessions.Member);
            var currentMember = _context.Members.SingleOrDefault(m => m.Email == email);

            var invitation = _context.Invitations
                .SingleOrDefault(m => m.SenderId == currentMember.MemberId
                && m.ReceiverId == recieverMemberId);
        }
    }
}
