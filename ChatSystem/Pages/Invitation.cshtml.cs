using ChatSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ChatSystem.Pages
{
    public class InvitationModel : PageModel
    {
        private readonly AppDbContext _context;

        public InvitationModel(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Invitation> Invitations { get; set; }

        [BindProperty(SupportsGet = true)]
        public Member Member { get; set; }

        public void OnGet()
        {
            string email = HttpContext.Session.GetString(Sessions.Member);

            Member = _context.Members.SingleOrDefault(m => m.Email == email);
            Invitations = _context.Invitations
                .Where(m => m.ReceiverId == Member.MemberId)
                .Include(s => s.Sender);
        }
    }
}
