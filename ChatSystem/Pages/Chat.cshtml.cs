using ChatSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ChatSystem.Pages
{
    public class ChatModel : PageModel
    {
        private readonly AppDbContext _context;

        public ChatModel(AppDbContext context)
        {
            _context = context;
        }

        public Member Sender { get; set; }
        public Member Receiver { get; set; }
        public IEnumerable<Message> Messages { get; set; }

        public void OnGet()
        {
            var recieverMemberId = Convert.ToInt32(Request.Query["id"]);
            string email = HttpContext.Session.GetString(Sessions.Member);
            Sender = _context.Members.SingleOrDefault(m => m.Email == email);
            Receiver = _context.Members.SingleOrDefault(m => m.MemberId == recieverMemberId);

            Messages = _context.Messages.Include(s => s.Sender).Include(r => r.Receiver)
                .Where(s => 
                s.SenderId == Sender.MemberId && s.ReceiverId == Receiver.MemberId || 
                s.SenderId == Receiver.MemberId && s.ReceiverId == Sender.MemberId).ToList();


            //var invitation = _context.Invitations
            //    .SingleOrDefault(m => m.SenderId == Sender.MemberId
            //    && m.ReceiverId == recieverMemberId);
        }
    }
}
